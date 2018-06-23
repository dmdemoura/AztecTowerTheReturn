using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	void Start () {
	}
	
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.SendMessage("PickUpHeart");
			Destroy(this.gameObject);
		}
	}
}
