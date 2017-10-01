using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

	[SerializeField]
	private float scrollSpeed;
	//Cette information est hard-codé pour simplifier la chose, mais devra être changé si on veux modifier la taille des tiles de background
	private float tileSize = 20.48f;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		//On récupère l'information de la position de départ de la tuile
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		//On fait défiler la tuile vers la gauche puis on réinitialise sa position à son point initiale pour faire un fond défilant et ininterrompu
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
		transform.position = startPosition + Vector3.left * newPosition;
	}
}
