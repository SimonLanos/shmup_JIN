using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Bullet {

	void Start(){
		Init ();
	}

	// Update is called once per frame
	void Update () {
		UpdatePosition ();
	}
}
