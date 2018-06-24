using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour {
	[SerializeField] float zOffset;
	[SerializeField] int damage;

	void Update(){
		this.transform.position = new Vector3(this.transform.position.x,
								this.transform.position.y, this.transform.position.y);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{	
		//Debug.Log("Yo, vai atirar setas");
		if(other.CompareTag("Enemy"))
		//&& Mathf.Abs(other.transform.position.z-this.transform.position.z)<=zOffset)
		{
			other.SendMessage("GetHit", damage);
			Destroy(this.gameObject);
		}
	}
}
