using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : MonoBehaviour {

	//On déclare les fonctions qui vont déterminer le comportement du canon
	[SerializeField]
	private float damage;

	public float Damage {
		get{
			return this.damage;
		}
		private set {
			this.damage = value;
		}
	}

	private float coolDown;
	[SerializeField]
	private float mincoolDown;
	[SerializeField]
	private float maxcoolDown;

	public float CoolDown {
		get{
			return this.coolDown;
		}
		private set {
			this.coolDown = value;
		}
	}

	private float timeOfNextShot;

	//On déclare la munition
	[SerializeField]
	private Bullet bullet;

	void Start(){
		timeOfNextShot = Time.time + coolDown;
		//On choisi au hasard le cooldown de cet ennemi spécifique
		coolDown = Random.Range (mincoolDown, maxcoolDown);
	}

	public void Fire(){
		if (Time.time > timeOfNextShot) {
			Bullet shot = BulletFactory.Instance.GetBullet (Bullet.BulletType.EnnemyBullet);
			shot.SetPosition (transform.position);
			shot.transform.rotation = transform.rotation;
			shot.Damage = Damage;
			shot.direction = new Vector2(-1,0);
			//shot.bulletType = Bullet.BulletType.EnnemyBullet;
			timeOfNextShot = Time.time + CoolDown;
		}
	}

	void Update(){
		Fire();
	}
}
