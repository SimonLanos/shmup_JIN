using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBasicEngine : MonoBehaviour {

	public bool zigzag;
	[SerializeField]
	private float minSpeed;
	[SerializeField]
	private float maxSpeed;

	private float effectiveSpeed;


	// Use this for initialization
	void Start () {
		//On choisi la vitesse de ce personnage spécifiquement
		effectiveSpeed = Random.Range (minSpeed, maxSpeed);
		GetComponent<EnemyAvatar> ().maxSpeed = effectiveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//selon le tpe du mouvement du personnage on update sa position différement
		if (!zigzag) {
			GetComponent<Engines> ().speed = new Vector2 (-1, 0);
		} else if (zigzag) {
			GetComponent<Engines> ().speed = (new Vector2 (-1, Mathf.Sin(Time.time))).normalized;
		}
	}
}
