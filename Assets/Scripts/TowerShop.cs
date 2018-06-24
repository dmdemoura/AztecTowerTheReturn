using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour {
    [SerializeField] private Tower tower;
    [SerializeField] private int cost;
    private new BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject.GetComponent<Player>().hearts >= cost)
                {
                    other.gameObject.GetComponent<Player>().hearts -= cost;
                    collider.isTrigger = false;
                    spriteRenderer.color = Color.white;
                    tower.enabled = true;
                }
            }
        }
    }

}
