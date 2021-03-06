using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private TextAsset levelTextAsset;
	//On déclare les variables pour stocker les prefabs du joueur et des ennemis
	[SerializeField]
	private PlayerAvatar player;
	[SerializeField]
	private EnemyAvatar enemy;

	//Variables liées à l'apparition des ennemis
	[SerializeField]
	private float enemyCooldown;
	private float timeOfNextEnemyInstantiation;

	//On déclare les options d'apparition des collectables et leur prefab
	[SerializeField]
	private float collectibleApparitionCooldown;
	private float timeOfNextCollectibleInstantiation;

	[SerializeField]
	private Collectibles weaponCollectible;
	[SerializeField]
	private int weaponCollectibleApparitionRatio;

	[SerializeField]
	private Collectibles energyCollectible;
	[SerializeField]
	private int energyCollectibleApparitionRatio;


	[SerializeField]
	private Collectibles healthCollectible;
	[SerializeField]
	private int healthCollectibleApparitionRatio;


	//On déclare le prefabs du décor
	[SerializeField]
	private BackgroundScroller background;
	//Ces variables stockeront les dimensions de la fenetre de jeu
	private float height;
	private float width;

	//Ici on défini les variables qui définiront la nature du niveau
	[SerializeField]
	private bool gameModeWithHealth;
	public bool GameModeWithHealth {
		get {
			return gameModeWithHealth;
		}
		private set {
			gameModeWithHealth = value;
		}
	}

	[SerializeField]
	private int numberOfEnemyInLevel;

	[SerializeField]
	private float[] timeBetweenThisAndPreviousEnemy;
	[SerializeField]
	private float[] enemiesVerticalSpawnPositions;

	private int numberOfEnemyInstantiated;
	private int numberOfDefeatedEnemy;

	private int score;
	public int Score {
		get {
			return score;
		}
		private set {
			score = value;
		}
	}
	private int combo;
	public int Combo {
		get {
			return combo;
		}
		private set {
			combo = value;
		}
	}

	//Ici on s'assure qu'il s'agit d'un singleton
	private static GameManager instance = null;

	public static GameManager Instance {
		get {
			return instance;
		}
	}

	private void Awake(){
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		}

		instance = this;
	}

	// Use this for initialization
	void Start () {
		//On instantie le joueur
		PlayerAvatar temp = Instantiate (player);
		//On prépare l'événemment de fin de jeu
		temp.OnPlayerDeath += OnGameOver;
		//On calcule les dimensions de la fenetre de jeu
		height = Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
		//On crée le décor
		for (int i = 0; (i-2) * 20.48 < width; i++) {
			Instantiate (background, new Vector3 (i * 20.48f + 10.24f - width, 0, 1), transform.rotation);
		}
		//On modifie ces valeurs pour assurer que les ennemis sont créés hors de la fenetre
		height -= enemy.GetComponent<Renderer> ().bounds.size.x/2f;
		width += enemy.GetComponent<Renderer> ().bounds.size.y/2f;
		//On initialise le temps pour créer les ennemis
		timeOfNextEnemyInstantiation = Time.time;
		//WriteDataInXML ("test_data2.xml"); //à ignorer, j'avais juste la flemme d'écrire un XML à la main
		if (levelTextAsset) {
			LoadFromXML (levelTextAsset);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Instantiation des collectables
		if (Time.time > timeOfNextCollectibleInstantiation) {
			//On réinitialise le temps d'attente
			timeOfNextCollectibleInstantiation += collectibleApparitionCooldown;
			//On prépare le collectables qu'on va instantier en le choisissant
			Collectibles toInstantiate;
			float index = Random.Range (0, weaponCollectibleApparitionRatio + healthCollectibleApparitionRatio + energyCollectibleApparitionRatio);
			//Selon le nombre tiré, on initialise tel ou tel collectable
			if (index < weaponCollectibleApparitionRatio) {
				toInstantiate = weaponCollectible;
			} else if (index < weaponCollectibleApparitionRatio + healthCollectibleApparitionRatio) {
				toInstantiate = healthCollectible;
			} else {
				toInstantiate = energyCollectible;
			}
			//On l'instantie
			Instantiate (toInstantiate, new Vector3 (width, Random.Range (-height, height), 0), transform.rotation);
		}


		//Selon le mode de jeu on emploi telle ou telle méthode pour instantier un ennemi
		if (GameModeWithHealth) {
			if (levelTextAsset == null) {
				//Si il n'y a pas de niveau à chaerger, utiliser les valeurs de l'éditeur Unity 
				if (Time.time > timeOfNextEnemyInstantiation && numberOfEnemyInstantiated < numberOfEnemyInLevel) {
					timeOfNextEnemyInstantiation += enemyCooldown;
					EnemyAvatar temp = EnemyFactory.Instance.GetEnemy();
					temp.SetPosition ( new Vector2(width, Random.Range (-height, height)));
					//On décide de la nature du mouvement de l'ennemi
					temp.GetComponent<AIEnemyBasicEngine> ().zigzag = (1 == Random.Range (0, 2));
					//On prépare l'événement de sa mort
					temp.OnEnemyDeath += OnEnemyDeath;
					temp.OnEnemyDeath += UIManager.OnEnemyDeath;
					//On met à jour le nombre d'ennemis déjà instanciés
					numberOfEnemyInstantiated++;
				}
			} else { //Sinon utiliser les données du XML donné
				Debug.Log("prout");
				if (Time.time > timeOfNextEnemyInstantiation && numberOfEnemyInstantiated < numberOfEnemyInLevel) {
					timeOfNextEnemyInstantiation += timeBetweenThisAndPreviousEnemy[numberOfEnemyInstantiated];
					EnemyAvatar temp = EnemyFactory.Instance.GetEnemy();
					temp.SetPosition ( new Vector2(width,enemiesVerticalSpawnPositions[numberOfEnemyInstantiated]));
					numberOfEnemyInstantiated++;
					temp.GetComponent<AIEnemyBasicEngine> ().zigzag = (1 == Random.Range (0, 2));
					temp.OnEnemyDeath += OnEnemyDeath;
					temp.OnEnemyDeath += UIManager.OnEnemyDeath;
				}
			}
		} else if (!GameModeWithHealth) {
			if (Time.time > timeOfNextEnemyInstantiation) {
				timeOfNextEnemyInstantiation += enemyCooldown;
				//EnemyAvatar temp = Instantiate (enemy, new Vector3 (width, Random.Range (-height, height), 0), transform.rotation);
				EnemyAvatar temp = EnemyFactory.Instance.GetEnemy();
				temp.SetPosition ( new Vector2(width, Random.Range (-height, height)));
				temp.GetComponent<AIEnemyBasicEngine>().zigzag = (1 == Random.Range (0, 2));
				temp.OnEnemyDeath += OnEnemyDeath;
				temp.OnEnemyDeath += UIManager.OnEnemyDeath;
			}
		}
	}

	void OnGameOver()
	{
		//En cas de mort du joueur on charge le menu principal
		SceneManager.LoadSceneAsync ("menu", LoadSceneMode.Single);
	}

	void OnEnemyDeath()
	{
		//Selon le mode de jeu, en cas de mort d'un ennemi ...
		if (GameModeWithHealth) {
			//...On l'ajoute au nombre d'ennmis vaincus
			numberOfDefeatedEnemy++;
			if (numberOfDefeatedEnemy == numberOfEnemyInLevel) {
				SceneManager.LoadSceneAsync ("menu", LoadSceneMode.Single);
			}
		} else if (!GameModeWithHealth) {
			//Ou on l'ajoute au score
			AddScore( 100 * (combo+1));
			//Et on incrémente le combo
			combo++;
		}
	}

	public void ResetCombo(){
		combo = 0;
	}

	public void BoostCombo(int boost){
		combo += boost;
	}

	public void AddScore(int _score){
		score += _score;
		if (score < 0) {
			score = 0;
		}
	}

	void WriteDataInXML(string path){
		LevelDescription levelDescription = new LevelDescription (
			                                    gameModeWithHealth,
			                                    enemyCooldown,
			                                    collectibleApparitionCooldown,
			                                    weaponCollectibleApparitionRatio,
			                                    energyCollectibleApparitionRatio,
			                                    healthCollectibleApparitionRatio,
			                                    numberOfEnemyInLevel,
			                                    timeBetweenThisAndPreviousEnemy,
			                                    enemiesVerticalSpawnPositions);
		XmlHelpers.SerializeToXML<LevelDescription> (path, levelDescription);
		
	}

	void LoadFromXML(TextAsset textAsset){
		LevelDescription levelDescription = XmlHelpers.DeserializeFromXML<LevelDescription>(textAsset);

		gameModeWithHealth = levelDescription.gameModeWithHealth;

		enemyCooldown = levelDescription.enemyCooldown;
		collectibleApparitionCooldown = levelDescription.collectibleApparitionCooldown;

		weaponCollectibleApparitionRatio = levelDescription.weaponCollectibleApparitionRatio;
		energyCollectibleApparitionRatio = levelDescription.energyCollectibleApparitionRatio;
		healthCollectibleApparitionRatio = levelDescription.energyCollectibleApparitionRatio;

		numberOfEnemyInLevel = levelDescription.numberOfEnemyInLevel;

		timeBetweenThisAndPreviousEnemy = levelDescription.timeBetweenThisAndPreviousEnemy;
		enemiesVerticalSpawnPositions = levelDescription.enemiesVerticalSpawnPositions;

	}
}
