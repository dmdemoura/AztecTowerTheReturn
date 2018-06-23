using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	int currentWave = 1;
	int numberOfGroups = 0;
	int currentGroups = 0;

	[SerializeField]
	GameObject[] enemies;

	[SerializeField]
	Transform spawnAreaReference;

	[SerializeField]
	[Range(0.0f, 1.0f)] float initialSpawnRate;

	[SerializeField]
	int initialSpawnAmount, initialSpawnInterval;

	void Awake()
	{
		StartGame();
	}

	void StartGame()
	{
		InvokeRepeating("SpawnerLoop",0f,initialSpawnInterval);
	}

	void NextWave()
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
			NextWave();
		}
	}
}
