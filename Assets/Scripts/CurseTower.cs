using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurseTower : MonoBehaviour {

	List<GameObject> enemies;
	Enemy cursedEnemy;

	[SerializeField]
	float DPS, attackDelay;
	[SerializeField] float reconstructTime;
	[SerializeField] int health;


	private enum State{active, broken}
	private State state = State.active;

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
		if(state==State.active){
			if(cursedEnemy != null)
				cursedEnemy.SendMessage("GetHit", DPS);
			else
				CancelInvoke("Curse");
		}
	}

    private IEnumerator reconstructing()
    {
        state = State.broken;
        yield return new WaitForSeconds(reconstructTime);
        state = State.active;
    }

    public void GetHit(int damage)
	{
		if(health>=damage)
			health -= damage;
		else
			health=0;

		if(health==0)
            StartCoroutine(reconstructing());
    }
}