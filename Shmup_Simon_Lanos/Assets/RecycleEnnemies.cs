using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleEnnemies : MonoBehaviour {

	void OnBecameInvisible(){
		EnemyFactory.Instance.Relase (gameObject.GetComponent<EnemyAvatar> ());
	}
}
