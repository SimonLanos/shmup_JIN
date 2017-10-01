using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour {
	
	private Vector2 direction = new Vector2(-1,0);
	private Vector2 speed;
	[SerializeField]
	private float MaxSpeed;

	private Vector2 position;


	//On défini ici quels effets notre collectables va avoir au contact du joueur
	//Rendre de la santé
	[SerializeField]
	private int healthRecovery;
	//Augmenter la jauge de combo
	[SerializeField]
	private int comboBoost;
	//Débloquer un tiers de l'arme suivante
	[SerializeField]
	private bool unlockNextShoot;

	//Recharger pleinement l'énergie
	[SerializeField]
	private bool energyRecovery;



	protected virtual void Start(){
		position = new Vector2 (transform.position.x, transform.position.y);
		speed = direction*MaxSpeed;
	}

	protected virtual void UpdatePosition(){
		speed = direction*MaxSpeed;
		position += speed * Time.deltaTime;
		transform.position = new Vector3 (position.x, position.y, 0);
	}

	void Update(){
		UpdatePosition ();
	}


	void OnTriggerEnter2D(Collider2D other){
		//Au contact du personnage, on applique les effets du collectable 
		PlayerAvatar target = other.GetComponent<PlayerAvatar> ();
		if (target) {
			GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
			if (gameManager) {
				if (gameManager.GameModeWithHealth) {
					target.TakeDamage (-healthRecovery);
					Destroy (gameObject);
				}
				if (!gameManager.GameModeWithHealth) {
					gameManager.BoostCombo (comboBoost);
					Destroy (gameObject);
				}
			}
			BulletGun bulletGun = other.GetComponent<BulletGun> ();
			if (bulletGun && unlockNextShoot) {
				bulletGun.TryUnlockNextShoot();
			}
			if (bulletGun && energyRecovery) {
				bulletGun.RefillEnergy ();
			}
		}
	}
}
