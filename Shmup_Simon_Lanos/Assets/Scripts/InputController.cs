using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	[SerializeField]
	private float maximumTimeBetweenInputsToDash;

	private float timeOfLastInput;
	private int lastInput; // 0 = rien, 1 = gauche, 2 = droite, 3 = haut, 4 = bas
	private bool startDashing;

	void Update () {
		//On récupère l'input du joueur pour déterminer la direction de déplacement du vaisseau
		GetComponent<Engines> ().speed = (new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"))).normalized;
		if (Input.GetKeyDown (KeyCode.Tab)) {
			GetComponent<BulletGun> ().SwapWeapon ();
		}
		//Le joueur tire avec espace
		if ((!GetComponent<BulletGun> ().overHeat) && (Input.GetKey ("space"))) {
			GetComponent<BulletGun> ().Fire ();
		}
		//si le joueur ne tire pas il récupère de l'énergie
		if ((GetComponent<BulletGun> ().overHeat) || !(Input.GetKey ("space"))) {
			GetComponent<BulletGun> ().RegenerateEnergy ();
		}


		//Si le joueur rentre deux fois de suite la meme touche dans un temps donné, on dit au moteur de dasher
		if (Time.time > timeOfLastInput + maximumTimeBetweenInputsToDash) {
			lastInput = 0;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
			if (lastInput == 1) {
				startDashing = true;
			}
			lastInput = 1;
			timeOfLastInput = Time.time;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
			if (lastInput == 2) {
				startDashing = true;
			}
			lastInput = 2;
			timeOfLastInput = Time.time;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {
			if (lastInput == 3) {
				startDashing = true;
			}
			lastInput = 3;
			timeOfLastInput = Time.time;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) {
			if (lastInput == 4) {
				startDashing = true;
			}
			lastInput =4;
			timeOfLastInput = Time.time;
		}

		if (startDashing) {
			if (!GetComponent<BulletGun> ().overHeat) {
				GetComponent<Engines> ().Dash ();
				GetComponent<BulletGun> ().DashEnergyDiminution ();
			}
			startDashing = false;
			lastInput = 0;
		}


	}
}
