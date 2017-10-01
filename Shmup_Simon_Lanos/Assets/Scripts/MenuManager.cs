using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	//On s'assure qu'il s'agit d'un singleton
	private static MenuManager instance = null;

	public static MenuManager Instance {
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

	//On charge la scène de jeu
	public void LoadGame(){
		SceneManager.LoadSceneAsync ("_main", LoadSceneMode.Single);
	}

	//On ferme le jeu
	public void QuitGame(){
		Application.Quit ();
	}
}
