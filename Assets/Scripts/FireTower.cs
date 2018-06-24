using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower {
    [SerializeField] private GameObject flame;
    [SerializeField] private float flameSpeed;
    [SerializeField] private float downtime;
    [SerializeField] private float uptime;
    
    private enum State {active, inactive};
    private State state = State.inactive;

    private float lastTime = 0;

    private void Start()
    {
        Register();
    }

    private void FixedUpdate() {
        if (IsBroken) return;

        float deltaTime = Time.realtimeSinceStartup - lastTime;
        if (state==State.inactive && deltaTime >= downtime)
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
}