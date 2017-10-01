using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

	public float Damage;

	public Vector2 direction;
	private Vector2 speed;
	public float MaxSpeed;

	private Vector2 position;

	public enum BulletType{Player, EnnemyBullet};
	public BulletType bulletType;

	protected virtual void Init(){
		//On récupère les informations de position de l'élément en ignorant l'axe z
		position = new Vector2 (transform.position.x, transform.position.y);
		//On initialise la vitesse
		speed = direction*MaxSpeed;
	}

	protected virtual void UpdatePosition(){
		//On met à jour la vélocité de la balle
		speed = direction*MaxSpeed;
		//On calcule sa position
		position += speed * Time.deltaTime;
		//On injecte cete information dans sa transform
		transform.position = new Vector3 (position.x, position.y, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		//Selon la nature de la balle, on cible le joueur on l'ennemi
		BaseAvatar target = null ;
		if (bulletType == BulletType.Player) {
			target = other.GetComponent<EnemyAvatar> ();
		}
		else if (bulletType == BulletType.EnnemyBullet) {
			target = other.GetComponent<PlayerAvatar> ();
		}
		//On blesse la cible acquise et on détruit la balle
		if (target) {
			target.TakeDamage (Damage);
			BulletFactory.Instance.Relase (gameObject.GetComponent<Bullet>());
		}
	}

	public void ResetPosition(){
		position = Vector2.zero;
		transform.position = Vector3.zero;
	}

	public void SetPosition(Vector3 newPosition){
		position = new Vector2 (newPosition.x, newPosition.y);
		transform.position = newPosition;
	}

}
