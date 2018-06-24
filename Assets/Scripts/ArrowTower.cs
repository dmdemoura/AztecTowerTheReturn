using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float fireRate;
    [SerializeField] private float arrowSpawnDistance;
    [SerializeField] private float arrowVelocity;
    private Quaternion rotation;
    private enum State{broken, active};
    private State state = State.active;

    private void Start()
    {
        InvokeRepeating("Fire", 0, fireRate);
    }
    private void Update()
    {
        //GameObject enemy = GetClosestGameObjectWithTag("Enemy", transform.position);
        GameCharacter enemy = FindClosestGameChar("Enemy");
        if (enemy != null)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
            rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
        if(!IsBroken){
            GameObject newArrow = Instantiate(arrow, transform.position + rotation * new Vector3(arrowSpawnDistance,0), rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = rotation * new Vector3(arrowVelocity,0);
        }
    }
}