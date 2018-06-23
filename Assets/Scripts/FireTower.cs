using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : MonoBehaviour {
    [SerializeField] private GameObject flame;
    [SerializeField] private float flameSpeed;

	private void FixedUpdate () {
        flame.transform.RotateAround(transform.position, new Vector3(0,0, 1), flameSpeed);
	}
}
