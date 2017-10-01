using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : BaseAvatar {

	public event System.Action OnPlayerDeath;

	//On s'assure qu'il s'agit d'un singleton
	private static PlayerAvatar instance = null;

	public static PlayerAvatar Instance {
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



	protected override void Die ()
	{
		base.Die ();
		OnPlayerDeath ();
	}

	public override void TakeDamage (float damage)
	{
		//Selon le mode de jeu, on reçoit des dégats ou on subit une perte en score
		GameManager temp2 = GameObject.FindObjectOfType<GameManager> ();
		if (temp2) {
			if (temp2.GameModeWithHealth) {
				base.TakeDamage (damage);
			} else {
				temp2.ResetCombo ();
				temp2.AddScore (-100);
			}
		}
	}
		
}
