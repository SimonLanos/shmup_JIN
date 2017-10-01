using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameManager gameManager;
	private Slider health;
	private Slider energy;
	private Slider weaponUnlock;
	private Slider weaponUnlockCollectable;
	private PlayerAvatar player;
	[SerializeField]
	public Text scoreText;
	[SerializeField]
	public Text comboText;


	// Use this for initialization
	void Start () {
		//On initialise nos variables
		gameManager = GameObject.FindObjectOfType<GameManager> ();
		GameObject temp = GameObject.Find ("Health Bar");
		if (temp != null){
			health = temp.GetComponent<Slider>();
		}
		temp = GameObject.Find ("Energy Bar");
		if (temp != null){
			energy = temp.GetComponent<Slider>();
		}
		temp = GameObject.Find ("WeaponBar");
		if (temp != null){
			weaponUnlock = temp.GetComponent<Slider>();
		}
		temp = GameObject.Find ("WeaponCollectableBar");
		if (temp != null){
			weaponUnlockCollectable = temp.GetComponent<Slider>();
		}
		PlayerAvatar temp1 = GameObject.FindObjectOfType<PlayerAvatar> ();
		if (temp1 != null){
			player = temp1;
			player.OnPlayerDeath += OnGameOver;
			//On règle la valeur maximale de chacun des sliders
			health.maxValue = player.MaxHealth;
			energy.maxValue = player.gameObject.GetComponent<BulletGun> ().MaxEnergy;
		}
		GameManager temp2 = GameObject.FindObjectOfType<GameManager> ();
		if (temp2) {
			if (!temp2.GameModeWithHealth) {
				//SI le mode de jeu est sans vie, on désactive la barre de vie
				health.gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Ce passage est rajouté pour s'assurer que la barre de vie se cache bien comme il faut, même si le bool qui indique le mode de jeu change pendant l'initialisation du GameManager (suite à la lecture d'un XML, par exemple) (C'est le cas ici)
		GameManager temp2 = GameObject.FindObjectOfType<GameManager> ();
		if (temp2) {
			if (!temp2.GameModeWithHealth) {
				//SI le mode de jeu est sans vie, on désactive la barre de vie
				health.gameObject.SetActive(false);
			}
		}
		//fin du passage rajouté

		if (player == null) {
			//Si le joueur n'a pas été détecté car pas instantié à temps, on le récupère ici
			PlayerAvatar temp = GameObject.FindObjectOfType<PlayerAvatar> ();
			if (temp != null) {
				player = temp;
				player.OnPlayerDeath += OnGameOver;
				health.maxValue = player.MaxHealth;
				energy.maxValue = player.gameObject.GetComponent<BulletGun> ().MaxEnergy;
			}
		} else {
			//On met à jour la valeur courante des sliders
			health.value = player.Health;
			energy.value = player.gameObject.GetComponent<BulletGun> ().Energy;
			weaponUnlock.value = player.gameObject.GetComponent<BulletGun> ().LastUnlockWeapon + 1;
			weaponUnlockCollectable.value = player.gameObject.GetComponent<BulletGun> ().NumberOfUnlockShootCollectiblesHarvested;
		}

		if (!gameManager.GameModeWithHealth) {
			scoreText.text = "Score : " + gameManager.Score;
			comboText.text = "Combo : " + gameManager.Combo;
		} else {
			scoreText.text = "";
			comboText.text = "";
		}
	}

	//Ces méthodes sont encore vide faute de temps pour trouver comment les remplir mais elles sont bien appelées à la mort du joueur et d'un ennemi, respectivement

	void OnGameOver()
	{
		
	}

	public static void OnEnemyDeath()
	{

	}

}
