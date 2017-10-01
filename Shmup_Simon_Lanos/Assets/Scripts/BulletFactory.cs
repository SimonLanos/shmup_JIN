using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour {


	[SerializeField]
	private GameObject playerBullet;
	[SerializeField]
	private GameObject ennemyBullet;

	private Stack<Bullet> stackPlayerBullets = new Stack<Bullet>();
	private Stack<Bullet> stackEnnemyBullets = new Stack<Bullet>();

	//Singleton
	private static BulletFactory instance = null;

	public static BulletFactory Instance {
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

	//Fin singleton

	public Bullet GetBullet(Bullet.BulletType bulletType){
		Bullet shot;
		if (bulletType == Bullet.BulletType.Player) {
			if (stackPlayerBullets.Count <= 0) {
				shot = (Instantiate (playerBullet, Vector3.zero, new Quaternion ())).GetComponent<Bullet> ();
			} else {
				shot = stackPlayerBullets.Pop ();
			}
		}
		else {
			if (stackEnnemyBullets.Count <= 0) {
				shot = (Instantiate (ennemyBullet, Vector3.zero, new Quaternion ())).GetComponent<Bullet> ();
			} else {
				shot = stackEnnemyBullets.Pop ();
			}
		}

		shot.ResetPosition ();

		shot.gameObject.SetActive (true);
		return shot;
	}

	public void Relase(Bullet bullet){
		if (bullet.bulletType == Bullet.BulletType.Player) {
			stackPlayerBullets.Push (bullet);
		}
		else {
			stackEnnemyBullets.Push (bullet);
		}
		bullet.gameObject.SetActive (false);
	}
}
