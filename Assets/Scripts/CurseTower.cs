using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurseTower : MonoBehaviour {

	List<GameObject> enemies = new List<GameObject>();
	Enemy cursedEnemy;

	[SerializeField]
	float DPS, attackDelay;

	void Awake()
	{
		InvokeRepeating("ScanForEnemies", 0f, attackDelay);
	}

	void ScanForEnemies()
	{
		enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		if(enemies.Count > 0)
		{
		List<int> hp = new List<int>();

		foreach(var enemy in enemies)
			hp.Add(enemy.GetComponent<Enemy>().health);

		cursedEnemy = enemies[hp.IndexOf(hp.Max())].GetComponent<Enemy>();
		InvokeRepeating("Curse", 0f, 1f);
		}
	}

	void Curse()
	{
		if(cursedEnemy != null)
			cursedEnemy.SendMessage("GetHit", DPS);
		else
			CancelInvoke("Curse");
	}
	
}
