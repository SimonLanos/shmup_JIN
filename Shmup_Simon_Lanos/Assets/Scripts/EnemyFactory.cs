using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

	[SerializeField]
	private GameObject ennemy;

	private Stack<EnemyAvatar> stackEnnemies = new Stack<EnemyAvatar>();

	//Singleton
	private static EnemyFactory instance = null;

	public static EnemyFactory Instance {
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

	public EnemyAvatar GetEnemy(){
		EnemyAvatar enemy;
		if (stackEnnemies.Count <= 0) {
			enemy = (Instantiate (ennemy, Vector3.zero, new Quaternion ())).GetComponent<EnemyAvatar> ();
			} else {
			enemy = stackEnnemies.Pop ();
			}

		enemy.ResetPosition ();

		enemy.gameObject.SetActive (true);
		return enemy;
	}

	public void Relase(EnemyAvatar enemy){
		stackEnnemies.Push (enemy);
		enemy.gameObject.SetActive (false);
	}
}
