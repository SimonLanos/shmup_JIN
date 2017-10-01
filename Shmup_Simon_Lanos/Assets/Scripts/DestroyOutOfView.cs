using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour {

	void OnBecameInvisible(){
		//On détruit l'objet quand il devient invisible
		Destroy (gameObject);
	}
}
