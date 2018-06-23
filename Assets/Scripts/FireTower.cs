using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : MonoBehaviour {
    [SerializeField] private GameObject flame;
    [SerializeField] private float flameSpeed;
    [SerializeField] private float downtime;
    [SerializeField] private float brokentime;
    [SerializeField] private float uptime;
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    
    private enum State {broken, active, inactive};
    private State state = State.inactive;

    private float lastTime = 0;

    private void FixedUpdate() {
        float deltaTime = Time.realtimeSinceStartup - lastTime;
        if(state==State.broken && deltaTime>=brokentime)
        {
            health = healthMax;
            state = State.active;
            lastTime = Time.realtimeSinceStartup;
        }
        else if (state==State.inactive && deltaTime >= downtime)
        {
            lastTime = Time.realtimeSinceStartup;
            flame.SetActive(true);
            state = State.active;
        }
        else if (state==State.active && deltaTime >= uptime)
        {
            lastTime = Time.realtimeSinceStartup;
            flame.SetActive(false);
            state = State.inactive;
        }
        else if (state==State.active)
        {
            flame.transform.RotateAround(transform.position, new Vector3(0, 0, 1), flameSpeed);
        }
	}

    public void GetHit(int damage)
	{
		if(health>=damage)
			health -= damage;
		else
			health=0;

		if(health==0)
        {
            state = State.broken;
            lastTime = Time.realtimeSinceStartup;
        }
    }
}