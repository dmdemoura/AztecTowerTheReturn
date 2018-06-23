using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	List<GameObject> targets = new List<GameObject>();
	GameObject currentTarget;
	[SerializeField] GameObject heart;
	Vector2 targetPos;
	bool isAttacking = false;
	enum FacingSide {Left, Right};
	FacingSide facingDirection;
	Rigidbody2D rigid;
	[SerializeField]
	LayerMask Player;
	[SerializeField]
	LayerMask Towers;
	int PlayerAndTowers;


	[SerializeField]
	float speed, xMoveOffset, yMoveOffset, hitRange, attackDelay, enemyDamage;
	[SerializeField]
	int health;

	void Update()
	{
		if(rigid.velocity.x > 0)
			facingDirection = FacingSide.Right;
		else if(rigid.velocity.x < 0)
			facingDirection = FacingSide.Left;
	}

	// Use this for initialization
	void Awake()
	 {
		PlayerAndTowers = Player.value & Towers.value;

		rigid = this.GetComponent<Rigidbody2D>();

		targets.AddRange(GameObject.FindGameObjectsWithTag("Tower"));
		targets.Add(GameObject.FindGameObjectWithTag("Player"));

		List<float> distances = new List<float>();
		foreach(var target in targets)
			distances.Add(Vector2.Distance(target.transform.position, this.transform.position));
		
		currentTarget = targets[distances.IndexOf(distances.Min())];
		if(currentTarget.tag == "Player")
			InvokeRepeating("TrackMovingTarget", 0f, Time.deltaTime);
		else
		{
			targetPos = currentTarget.transform.position;
			InvokeRepeating("TrackStillTarget", 0f, Time.deltaTime);
		}
	}		
	
	void TrackStillTarget()
	{
		if(!isAttacking)
		{
			if(Mathf.Abs(((Vector2)this.transform.position - targetPos).x) > xMoveOffset
			|| Mathf.Abs(((Vector2)this.transform.position - targetPos).y) > yMoveOffset)
				this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, speed);
			else
				StartCoroutine(AttemptAttack());
		}
	}

	void TrackMovingTarget()
	{
		targetPos = currentTarget.transform.position;

		if(!isAttacking)
		{
			if(Mathf.Abs(((Vector2)this.transform.position - targetPos).x) > xMoveOffset
			|| Mathf.Abs(((Vector2)this.transform.position - targetPos).y) > yMoveOffset)
				this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, speed);
			else
				StartCoroutine(AttemptAttack());
		}
	}

		IEnumerator AttemptAttack()
	{
		isAttacking = true;

		Vector2 hitDirection;

		if(facingDirection == FacingSide.Left)
			hitDirection = Vector2.left;
		else
			hitDirection = Vector2.right;
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, hitDirection, hitRange, PlayerAndTowers);
		if(hit)
		{
			Debug.Log("Yo3");
			yield return new WaitForSeconds(attackDelay);
			if(hit.transform.gameObject.tag != "Enemy")
			{
				Debug.Log("Yo4");
				hit.transform.gameObject.SendMessage("GetHit", enemyDamage);
			}
		}
		isAttacking = false;
	}

	void DrawAggro()
	{
		CancelInvoke("TrackStillTarget");
		CancelInvoke("TrackMovingTarget");
		currentTarget = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("TrackMovingTarget", 0f, Time.deltaTime);
	}

	void GetHit(int damage)
	{
		if(health>=damage)
			health -= damage;
		else
			health=0;

		if(health==0)
			Die();

	}

	void Die()
	{
		if(Random.Range(0f,1f)<=0.3f)
		{
			Instantiate(heart,this.transform.position,Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
}
