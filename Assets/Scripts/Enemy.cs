using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

	List<GameObject> targets = new List<GameObject>();
	GameObject currentTarget;
	Vector2 targetPos;
	bool isAttacking = false;
	enum FacingSide {Left, Right};
	FacingSide facingDirection;
	Rigidbody2D rigid;

	[SerializeField]
	float speed, xMoveOffset, yMoveOffset, hitRange, attackDelay, enemyDamage;

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
		Debug.Log(Mathf.Abs(((Vector2)this.transform.position - targetPos).x));

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

		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, hitDirection, hitRange);
		if(hit)
		{
			yield return new WaitForSeconds(attackDelay);
			if(hit.transform.gameObject.tag != "Enemy")
				hit.transform.gameObject.SendMessage("GetHit", enemyDamage);
		}
		isAttacking = false;
	}

	void DrawAggro()
	{
		CancelInvoke("TrackStillTarget");
		currentTarget = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("TrackMovingTarget", 0f, Time.deltaTime);
	}
}
