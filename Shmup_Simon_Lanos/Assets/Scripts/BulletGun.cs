using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour {


	[SerializeField]
	private Bullet bullet;

	[SerializeField]
	private float damage;

	public float Damage {
		get{
			return this.damage;
		}
		private set {
			this.damage = value;
		}
	}

	private Vector2 speed;
	[SerializeField]
	private float maxSpeed;

	public float MaxSpeed {
		get{
			return this.maxSpeed;
		}
		private set {
			this.maxSpeed = value;
		}
	}


	[SerializeField]
	private float[] coolDown;
	public float[] CoolDown{
		get{
			return coolDown;
		}
		private set{
			coolDown = value;
		}
	}


	//Options particulières à la dernière arme
	private float spiralAngle;
	[SerializeField]
	private float spiralRotationSpeed;



	private float timeOfNextShot;

	[SerializeField]
	private float[] energyNeededForOneShot;
	public float[] EnergyNeededForOneShot{
		get{
			return energyNeededForOneShot;
		}
		private set{
			energyNeededForOneShot = value;
		}
	}


	[SerializeField]
	private float dashEnergyCost;

	[SerializeField]
	private float energyRegeneration;

	[SerializeField]
	private float maxEnergy;
	public float MaxEnergy {
		get{
			return this.maxEnergy;
		}
		private set {
			this.maxEnergy = value;
		}
	}

	private float energy;
	public float Energy {
		get{
			return this.energy;
		}
		private set {
			this.energy = value;
		}
	}

	//On est en surchauffe quand l'énergie tombe à zéro
	public bool overHeat;


	private int numberOfUnlockShootCollectiblesHarvested;
	public int NumberOfUnlockShootCollectiblesHarvested{
		get{ return numberOfUnlockShootCollectiblesHarvested; }
		private set {numberOfUnlockShootCollectiblesHarvested = value;}
	}

	private int lastUnlockedWeapon;
	public int LastUnlockWeapon{
		get{ return lastUnlockedWeapon; }
		private set {lastUnlockedWeapon = value;}
	}

	private int weaponIndex;

	void Start(){
		Energy = MaxEnergy;
	}

	public void RegenerateEnergy(){
		//Si le vaisseau est en surchauffe, la récupération d'énergie est ralentie de 25%
		if (overHeat) {
			Energy += energyRegeneration * Time.deltaTime * 0.75f;
			if (Energy > MaxEnergy) {
				Energy = MaxEnergy;
				overHeat = false;
			}
		}
		if (!overHeat) {
			Energy += energyRegeneration * Time.deltaTime;
			if (Energy > MaxEnergy) {
				Energy = MaxEnergy;
			}
		}
	}

	public void SwapWeapon(){
		//On incrémente un index et on le fait boucler pour choisir quel type d'arme est actif
		weaponIndex++;
		if (weaponIndex > lastUnlockedWeapon) {
			weaponIndex = 0;
			spiralAngle = 0;
		}
	}

	public void Fire(){
		//Si le joueur n'est pas en surchauffe et que le cooldown de l'arme est révolu ...
		if (Time.time > timeOfNextShot && !overHeat) {
			//... Si il a l'énergie suffisante pour un tir en réserve ...
			if (Energy >= energyNeededForOneShot[weaponIndex]) {
				Energy -= energyNeededForOneShot[weaponIndex];
				Bullet shot;
				//... il tire la balle qui correspond à son type d'arme active
				switch (weaponIndex) {
				case 0: //ligne droite
					//shot = Instantiate (bullet, transform.position + new Vector3(5,-3,0)*0.2f, transform.rotation);
					shot = BulletFactory.Instance.GetBullet (Bullet.BulletType.Player);
					shot.SetPosition(transform.position + new Vector3 (5, -3, 0) * 0.2f); 
					//ci-dessus : cette information de position peut sembler étrange mais il s'agit du décalage nécessaire pour que les bullets semblent jaillir du canon dessiné sur le vaisseau
					shot.transform.rotation = transform.rotation;
					shot.direction = (new Vector2 (1, 0)).normalized;
					shot.Damage = Damage;
					//shot.gameObject.SetActive (true);
					break;
				case 1: //45°
					//shot = Instantiate (bullet, transform.position + new Vector3(5,-3,0)*0.2f, Quaternion.Euler (0, 0, 45));
					shot = BulletFactory.Instance.GetBullet (Bullet.BulletType.Player);
					shot.SetPosition(transform.position + new Vector3 (5, -3, 0) * 0.2f);
					shot.transform.rotation = Quaternion.Euler (0, 0, 45);
					shot.direction = (new Vector2 (1, 1)).normalized;
					shot.Damage = Damage;
					//shot = Instantiate (bullet, transform.position + new Vector3(5,-3,0)*0.2f, Quaternion.Euler (0, 0, -45));
					shot = BulletFactory.Instance.GetBullet (Bullet.BulletType.Player);
					shot.SetPosition(transform.position + new Vector3 (5, -3, 0) * 0.2f);
					shot.transform.rotation = Quaternion.Euler (0, 0, -45);
					shot.direction = (new Vector2 (1, -1)).normalized;
					shot.Damage = Damage;
					break;
				case 2: //spirale
					//shot = Instantiate (bullet, transform.position + new Vector3(5,-3,0)*0.2f, Quaternion.Euler (0, 0, spiralAngle*360f/2f/Mathf.PI));
					shot = BulletFactory.Instance.GetBullet (Bullet.BulletType.Player);
					shot.SetPosition(transform.position + new Vector3 (5, -3, 0) * 0.2f);
					shot.transform.rotation = Quaternion.Euler (0, 0, spiralAngle*360f/2f/Mathf.PI);
					shot.direction = (new Vector2 (Mathf.Cos (spiralAngle), Mathf.Sin (spiralAngle))).normalized;
					spiralAngle += spiralRotationSpeed * Time.deltaTime;
					shot.Damage = Damage;
					break;
				default: // cas par défaut en ligne droite pour ne pas causer d'erreur
					//shot = Instantiate (bullet, transform.position + new Vector3(5,-3,0)*0.2f, transform.rotation);
					shot = BulletFactory.Instance.GetBullet (Bullet.BulletType.Player);
					shot.SetPosition(transform.position + new Vector3 (5, -3, 0) * 0.2f);
					shot.transform.rotation = transform.rotation;
					shot.direction = (new Vector2 (1, 0)).normalized;
					shot.Damage = Damage;
					break;
				}
				shot.MaxSpeed = MaxSpeed;
				//shot.bulletType = Bullet.BulletType.Player;
				timeOfNextShot = Time.time + CoolDown[weaponIndex];
			}
			// Si il ne reste pas assez d'énergie en réserve, il ne tire pas et rentre en surchauffe
			if (Energy < energyNeededForOneShot[weaponIndex]) {
				Energy = 0;
				overHeat = true;
			}
		}
	}

	public void DashEnergyDiminution(){
		//On retire l'énergie necessaire au dash si il y en a assez en réserve, sinon on rentre en surchauffe
		if (Energy >= dashEnergyCost) {
			Energy -= dashEnergyCost;
		} else {
			Energy = 0;
			overHeat = true;
		}
	}

	public void TryUnlockNextShoot(){
		//Au bout de trois collectables de déblocage d'arme, on incrémente l'index qui régit à combien d'amre on a actuellement accès 
		numberOfUnlockShootCollectiblesHarvested++;
		if (numberOfUnlockShootCollectiblesHarvested >=3 ) {
			numberOfUnlockShootCollectiblesHarvested = 0;
			lastUnlockedWeapon++;
			if (lastUnlockedWeapon > coolDown.Length - 1) {
				lastUnlockedWeapon = coolDown.Length - 1;
			}
		}
	}

	public void RefillEnergy(){
		//On recharge intégralement l'énergie
		energy = MaxEnergy;
	}
	
}
