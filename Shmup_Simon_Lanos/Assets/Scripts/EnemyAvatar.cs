using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : BaseAvatar {

	public event System.Action OnEnemyDeath;

	protected override void Die ()
	{
		EnemyFactory.Instance.Relase(this);
		OnEnemyDeath ();
	}
}
