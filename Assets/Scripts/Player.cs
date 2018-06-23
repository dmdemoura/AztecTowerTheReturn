using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField]	private LayerMask Enemies;
	[SerializeField]	private int lives;
	[SerializeField]	private int health;
	[SerializeField]	private int damage;
	[SerializeField]	private int horizontalSpeed;
	[SerializeField]	private int verticalSpeed;
	[SerializeField]	private float enemyAttackDelay;
	[SerializeField]	private float rayDistance;
	private int raySide;
	private bool turnedRight;
	private bool attacking = false;
	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	void Update () {
		if(rb.velocity.x > 0)
		{
			turnedRight = true;
			raySide = 1;
		}
		else if(rb.velocity.x < 0)
		{
			turnedRight = false;
			raySide = -1;
		}

		//Movement
		rb.velocity = new Vector2(horizontalSpeed*Input.GetAxis("Horizontal"), verticalSpeed*Input.GetAxis("Vertical"));	

		//Attacking
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, raySide*Vector2.right, rayDistance, Enemies);
//		Debug.DrawLine(this.transform.position, transform.position + Vector3.right*rayDistance, Color.green);

		if(hit.collider!=null)
		{
			Debug.Log("foi");
			if(hit.transform.gameObject.tag=="Enemy")
			{	
				AttemptHit(hit.transform.gameObject);
			}
		}
	}

	private void AttemptHit(GameObject enemy)
	{
		if(attacking==false)
		{
			enemy.SendMessage("GetHit",damage);
			enemy.SendMessage("DrawAggro");
			StartCoroutine(attackDelay());
		}
	}

	public void GetHit(int damage)
	{
		health -= damage;
	}
	private IEnumerator attackDelay()
	{	
		attacking = true;
		yield return new WaitForSeconds(enemyAttackDelay);
		attacking = false;
	}
}		