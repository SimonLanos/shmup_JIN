using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour {

	public float maxSpeed;

	private float health;

	public float Health {
		get{
			return this.health;
		}
		private set {
			this.health = value;
		}
	}

	[SerializeField]
	private float maxHealth;
	public float MaxHealth {
		get{
			return this.maxHealth;
		}
		private set {
			this.maxHealth = value;
		}
	}

	void Start(){
		Health = MaxHealth;
	}

	//Si le vaisseau est en train de dasher, il ne prend pas de dégat
	public virtual void TakeDamage(float damage){
		Engines engine = GetComponent<Engines> ();
		if (engine && !engine.Dashing) {
			Health -= damage;
			if (Health <= 0) {
				Die ();
			}
		}
	}

	protected virtual void Die(){
		Destroy (gameObject);
	}

	public void ResetPosition(){
		GetComponent<Engines>().position = Vector2.zero;
		transform.position = Vector3.zero;
	}

	public void SetPosition(Vector3 newPosition){
		GetComponent<Engines>().position = new Vector2 (newPosition.x, newPosition.y);
		transform.position = newPosition;
	}



}
