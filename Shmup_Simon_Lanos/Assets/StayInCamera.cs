using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInCamera : MonoBehaviour {

	private float height;
	private float width;

	// Use this for initialization
	void Start () {
		//On calcule la taille de la fenetre de jeu
		height = Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		//On s'assure que le joueur est dans les limites de la vue de la caméra
		if (transform.position.y > height) {
			GetComponent<Engines>().position = new Vector2(transform.position.x, height);
		}
		if (transform.position.y < -height) {
			GetComponent<Engines>().position = new Vector2(transform.position.x, -height);
		}
		if (transform.position.x > width) {
			GetComponent<Engines>().position = new Vector2(width, transform.position.y);
		}
		if (transform.position.x < -width) {
			GetComponent<Engines>().position = new Vector2(-width, transform.position.y);
		}
	}
}
