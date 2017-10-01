using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : MonoBehaviour {

	public Vector2 speed;
	public Vector2 position;

	[SerializeField]
	private bool dashing = false;
	public bool Dashing {
		get {
			return dashing;
		}
		private set {
			dashing = value;
		}
	}
	[SerializeField]
	private float durationOfDash;
	private float timeStartOfDash;
	[SerializeField]
	private float distanceOfDash;

	[SerializeField]
	private Vector2 directionOfDash;

	void Start(){
		speed = Vector2.zero;
		position = new Vector2 ();
		position.x = transform.position.x;
		position.y = transform.position.y;
	}

	void Update(){
		//Selon si le joueur est en cours de dash ou non, on met à jour sa position différement
		if (dashing) {
			position = position + Time.deltaTime * distanceOfDash/durationOfDash * directionOfDash;
		}
		if (!dashing) {
			position = position + speed * GetComponent<BaseAvatar> ().maxSpeed * Time.deltaTime;
		}
		transform.position = new Vector3 (position.x, position.y, transform.position.z);
		//On vérifie si le dash est fini ou non
		if (dashing && Time.time > timeStartOfDash + durationOfDash) {
			dashing = false;
		}
	}

	public void Dash(){
		//SI le dash est lancé, on enregistre la direction au départ du dash pour la verrouiller
		if (!dashing && !GetComponent<BulletGun>().overHeat) {
			dashing = true;
			directionOfDash = speed;
			timeStartOfDash = Time.time;
		}
	}

}
