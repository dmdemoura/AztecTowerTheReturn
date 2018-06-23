using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : MonoBehaviour {
    [SerializeField] private GameObject flame;
    [SerializeField] private float flameSpeed;
    [SerializeField] private float downtime;
    [SerializeField] private float uptime;
    private float lastTime = 0;
    private bool isDownTime = true;

    private void FixedUpdate() {
        float deltaTime = Time.realtimeSinceStartup - lastTime;
        if (isDownTime && deltaTime >= downtime)
        {
            lastTime = Time.realtimeSinceStartup;
            flame.SetActive(true);
            isDownTime = false;
        }
        else if (!isDownTime && deltaTime >= uptime)
        {
            lastTime = Time.realtimeSinceStartup;
            flame.SetActive(false);
            isDownTime = true;
        }
        else if (!isDownTime)
        {
            flame.transform.RotateAround(transform.position, new Vector3(0, 0, 1), flameSpeed);
        }
	}
}
