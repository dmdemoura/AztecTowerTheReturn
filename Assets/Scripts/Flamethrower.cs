using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour {
	[SerializeField] float damage;

	void Update(){
		this.transform.position = new Vector3(this.transform.position.x,
								this.transform.position.y, this.transform.position.y);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Enemy"))
		{
			other.SendMessage("GetHit", damage);
		}
	}
}
