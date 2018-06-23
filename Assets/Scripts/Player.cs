using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField]	private LayerMask Enemies;
	[SerializeField]	private int lives;
	[SerializeField]	private int health;
	[SerializeField]	private int hearts;
	[SerializeField]	private int damage;
	[SerializeField]	private int horizontalSpeed;
	[SerializeField]	private int verticalSpeed;
	[SerializeField]	private float enemyAttackDelay;
	[SerializeField]	private float rayDistance;
	[SerializeField]	private float zOffset;
	private int raySide;
	private bool turnedRight;
	private bool attacking = false;
	private Rigidbody2D rb;
    private Animator animator;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        //Animation
        animator.SetFloat("direction", Input.GetAxis("Horizontal"));

		//Movement
		rb.velocity = new Vector2(horizontalSpeed*Input.GetAxis("Horizontal"), verticalSpeed*Input.GetAxis("Vertical"));	
		this.transform.position = new Vector3(this.transform.position.x,
								this.transform.position.y, this.transform.position.y);
		//Attacking
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, raySide*Vector2.right, rayDistance, Enemies);
		
		if(Input.GetKeyDown(KeyCode.Z)
			&& hit.collider!=null
			&& hit.transform.gameObject.tag=="Enemy"
			&& Mathf.Abs(hit.transform.position.z-this.transform.position.z)<=zOffset)
		{
			AttemptHit(hit.transform.gameObject);
		}
	}

	private void AttemptHit(GameObject enemy)
	{
		if(attacking==false)
		{
			Debug.Log("foi");
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

	public void PickUpHeart(){
		hearts++;
	}
}		