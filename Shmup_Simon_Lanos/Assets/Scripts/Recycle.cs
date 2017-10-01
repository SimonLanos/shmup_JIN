using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycle : MonoBehaviour {

	void OnBecameInvisible(){
		BulletFactory.Instance.Relase (gameObject.GetComponent<Bullet> ());
	}
}
