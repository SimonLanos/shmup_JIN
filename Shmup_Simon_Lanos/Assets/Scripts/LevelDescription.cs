using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelDescription{

	//AVANT TOUTE CHOSE, il est possible que je n'ai pas le temps de tout implémenter et que des variables soient mises en commentaire.
	//Idéalement, il faudrait implémenter au moins tout ce qui est ici pour le GameDesigner

	//choix mode de jeu
	public bool gameModeWithHealth;

	//Partie avec aléatoire
	public float enemyCooldown;
	public float collectibleApparitionCooldown;

	public int weaponCollectibleApparitionRatio;
	public int energyCollectibleApparitionRatio;
	public int healthCollectibleApparitionRatio;

	//public float minRandomEnemySpeed;
	//public float maxRandomEnemySpeed;

	//public float minRandomEnemyCooldown;
	//public float maxRandomEnemyCooldown;



	//Partie sans aléatoire

	public int numberOfEnemyInLevel;

	//Dans chaque tableau, un index correspond à un ennemi

	public float[] timeBetweenThisAndPreviousEnemy;
	public float[] enemiesVerticalSpawnPositions;
	//public float[] enemiesSpeeds;
	//public bool[] enemiesZigzag;
	//public float[] enemiesShootCooldowns;
	//public float[] enemyBulletsSpeeds;
	//public float[] enemiesDamage;
	//public float[] enemiesHealth;




	public LevelDescription(

		bool _gameModeWithHealth,
		float _enemyCooldown,
		float _collectibleApparitionCooldown,
		int _weaponCollectibleApparitionRatio,
		int _energyCollectibleApparitionRatio,
		int _healthCollectibleApparitionRatio,
		int _numberOfEnemyInLevel,
		float[] _timeBetweenThisAndPreviousEnemy,
		float[] _enemiesVerticalSpawnPositions/*,
		float[] _enemiesSpeeds,
		bool[] _enemiesZigzag,
		float[] _enemiesShootCooldowns,
		float[] _enemyBulletsSpeeds,
		float[] _enemiesHealth,
		float _minRandomEnemySpeed,
		float _maxRandomEnemySpeed,
		float _minRandomEnemyCooldown,
		float _maxRandomEnemyCooldown
*/
		)
	{
		enemyCooldown = _enemyCooldown;
		collectibleApparitionCooldown = _collectibleApparitionCooldown;
		weaponCollectibleApparitionRatio =  _weaponCollectibleApparitionRatio;
		energyCollectibleApparitionRatio = _energyCollectibleApparitionRatio;
		healthCollectibleApparitionRatio = _healthCollectibleApparitionRatio;
		gameModeWithHealth = _gameModeWithHealth;
		numberOfEnemyInLevel = _numberOfEnemyInLevel;
		timeBetweenThisAndPreviousEnemy = _timeBetweenThisAndPreviousEnemy;
		enemiesVerticalSpawnPositions = _enemiesVerticalSpawnPositions;
		/*
		enemiesSpeeds = _enemiesSpeeds;
		enemiesZigzag = _enemiesZigzag;
		enemiesShootCooldowns = _enemiesShootCooldowns;
		enemyBulletsSpeeds = _enemyBulletsSpeeds;
		enemiesHealth = _enemiesHealth;
		minRandomEnemyCooldown = _minRandomEnemyCooldown;
		maxRandomEnemyCooldown = _maxRandomEnemyCooldown;
		minRandomEnemySpeed = _minRandomEnemyCooldown;
		maxRandomEnemySpeed = _maxRandomEnemyCooldown;
		*/
	}

}
