using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject panel;
	
	[SerializeField] int damage;
	[SerializeField] int heal;
	List<GameObject> allEnemies;
		
	public int FireTower = 0;
	public int ArrowTower = 0;
	public int CurseTower = 0;
	int currentWave = 1;
	int numberOfGroups = 0;
	int currentGroups = 0;

	[SerializeField]
	GameObject[] enemies;
	GameObject player;
	Player plyr;
	[SerializeField]	int special;
	[SerializeField]
	Transform spawnAreaReference;

	[SerializeField]
	[Range(0.0f, 1.0f)] float initialSpawnRate;

	[SerializeField]
	int initialSpawnAmount, initialSpawnInterval;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.C) && plyr.hearts>special)
		{	
			hitAllEnemiesAndHeal();
			plyr.hearts-=special;
		}
	}

	void Awake()
	{
		StartGame();
		plyr = GetComponent<Player>();
	}

	void StartGame()
	{
		InvokeRepeating("SpawnerLoop",0f,initialSpawnInterval);
	}

	public void NextWave()
	{
		currentWave++;
		InvokeRepeating("SpawnerLoop",0f,initialSpawnInterval);
	}

	void SpawnerLoop()
	{
		Vector3 spawnPos;
		spawnPos.x = Random.Range(transform.position.x, spawnAreaReference.position.x);
		spawnPos.y = Random.Range(transform.position.y, spawnAreaReference.position.y);
		spawnPos.z = spawnPos.y;

		if(currentWave >= 1)
		{
			Debug.Log("WtfYo");
			Instantiate(enemies[0], spawnPos, Quaternion.identity);
			
		}


		if(currentWave >= 2)
		{	
			if(Random.Range(1f,0f) <= (initialSpawnRate * 0.5f * currentWave))
				Instantiate(enemies[1], spawnPos, Quaternion.identity);
		}

		if(currentWave >= 3)
		{
			if(Random.Range(1f,0f) <= (initialSpawnRate * 0.4f * currentWave))
				Instantiate(enemies[2], spawnPos, Quaternion.identity);
		}

		if(currentWave >= 4)
		{
			if(Random.Range(1f,0f) <= (initialSpawnRate * 0.3f * currentWave))
				Instantiate(enemies[3], spawnPos, Quaternion.identity);
		}

		if(currentWave >= 5)
		{
			if(Random.Range(1f,0f) <= (initialSpawnRate * 0.2f * currentWave))
				Instantiate(enemies[4], spawnPos, Quaternion.identity);
		}

		numberOfGroups++;
		if(numberOfGroups > (initialSpawnAmount * currentWave))
		{
			CancelInvoke("SpawnerLoop");
			panel.SetActive(true);
//			NextWave();
		}
	}

	void hitAllEnemiesAndHeal()
	{
		allEnemies = new List<GameObject>();
		allEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		foreach(var enemy in enemies)
			enemy.GetComponent<Enemy>().health -= damage;

		player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<Player>().health += heal;
		
	}
}
