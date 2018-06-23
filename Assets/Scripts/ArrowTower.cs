using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float fireRate;
    [SerializeField] private Vector2 heading;
    [SerializeField] private float arrowSpawnDistance;

    private void Start()
    {
        InvokeRepeating("Fire", 0, fireRate);

    }
    private void Fire()
    {
        Instantiate(arrow, transform.position + (Vector3) heading * arrowSpawnDistance, Quaternion.Euler(heading), transform);
    }
}
