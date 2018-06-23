using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float fireRate;
    [SerializeField] private float arrowSpawnDistance;
    [SerializeField] private float arrowVelocity;
    [SerializeField] private float reconstructTime;
    [SerializeField] private int health;
    private enum State{broken, active};
    private State state = State.active;

    private void Start()
    {
        InvokeRepeating("Fire", 0, fireRate);
    }
    private void Update()
    {
        GameObject enemy = GetClosestGameObjectWithTag("Enemy", transform.position);
        if (enemy != null)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    private GameObject GetClosestGameObjectWithTag(string tag, Vector3 position)
    {
        GameObject closest = null;
        float distance = float.PositiveInfinity;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(tag))
        {
            float newDistance = (gameObject.transform.position - position).magnitude;
            if (closest == null)
            {
                closest = gameObject;
                distance = newDistance;
            }
            else
            {
                if (newDistance < distance)
                {
                    distance = newDistance;
                    closest = gameObject;
                }
            }
        }
        return closest;
    }
    private void Fire()
    {   
        if(state==State.active){
            GameObject newArrow = Instantiate(arrow, transform.position + transform.rotation * new Vector3(arrowSpawnDistance,0), transform.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector3(arrowVelocity,0);
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
        Debug.Log("ArrowTower: GetHit called");
		if(health>=damage)
			health -= damage;
		else
			health=0;

		if(health==0)
            StartCoroutine(reconstructing());
    }
}