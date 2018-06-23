using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurseTower : MonoBehaviour {

	List<GameObject> enemies;
	Enemy cursedEnemy;

	[SerializeField]
	float DPS, attackDelay;

	void Awake()
	{
		InvokeRepeating("ScanForEnemies", 0f, attackDelay);
	}

	void ScanForEnemies()
	{
		enemies = new List<GameObject>();
		enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		if(cursedEnemy != null)
			if(enemies.Contains(cursedEnemy.gameObject))
				enemies.Remove(enemies.Find(gameObject => gameObject == cursedEnemy.gameObject));

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
