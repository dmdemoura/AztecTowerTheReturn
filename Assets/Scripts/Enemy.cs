using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : GameCharacter
{
	[SerializeField] private GameObject heart;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDistance;
    private Animator animator;
    private GameCharacter target;
    private CooldownTimer attackCooldown;

    private void Awake()
    {
    }
    private void Start()
	{
        Register();
        attackCooldown = new CooldownTimer(attackDelay);
        animator = GetComponent<Animator>();
	}
    private void FixedUpdate()
    {
        if (animator)
        {
            animator.SetFloat("direction", MovementDirection.x);
        }

        target = FindClosestGameChar("Player");
        attackCooldown.Update();
        if (target != null)
            TrackMovingTarget();
    }

    /*void TrackStillTarget()
	{
		if(!isAttacking)
		{
			if(Mathf.Abs((this.transform.position - targetPos).x) > xMoveOffset
			|| Mathf.Abs((this.transform.position - targetPos).y) > yMoveOffset)
				//this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed);
                rigid.velocity = (targetPos - transform.position).normalized * speed;
			else
            {
                rigid.velocity = Vector2.zero;
                StartCoroutine(AttemptAttack());
            }
		}
	}*/

    void TrackMovingTarget()
	{
		Vector3 targetPos = target.transform.position;

        if ((transform.position - targetPos).magnitude >= attackDistance)
            Move((targetPos - transform.position).normalized * MaxSpeed);
        else
        {
            if (attackCooldown.Available)
            {
                animator.SetTrigger("startAttack");
                attackCooldown.Activate();
                target.SendMessage("GetHit", Damage);
            }
        }
	}

/*		IEnumerator AttemptAttack()
	{
		isAttacking = true;
		Debug.Log("Yo, i1m attacking");

		Vector2 hitDirection;

		if(facingDirection == FacingSide.Left)
			hitDirection = Vector2.left;
		else
			hitDirection = Vector2.right;
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, hitDirection, hitRange, Towers);

		if(hit.collider != null)
		{
			Debug.Log("Yo3"  + hit.transform.gameObject.name);
			if((hit.transform.gameObject.tag == "Tower" || hit.transform.gameObject.tag == "Player")
                && Mathf.Abs(hit.transform.position.z - this.transform.position.z)<= zOffset)
			{
                yield return new WaitForSeconds(attackDelay);
				Debug.Log("Enemy: attacking " + hit.transform.gameObject.name);
				hit.transform.gameObject.SendMessage("GetHit", enemyDamage);
			}
		}
        else
        {
            Debug.Log("Yo raycast fail");
        }
		isAttacking = false;
	}
    */
	void DrawAggro()
	{
		CancelInvoke("TrackStillTarget");
		CancelInvoke("TrackMovingTarget");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<GameCharacter>();
		InvokeRepeating("TrackMovingTarget", 0f, Time.deltaTime);
	}

	protected override void OnDeath()
	{
        base.OnDeath();
		if(Random.Range(0f,1f)<=0.3f)
		{
			Instantiate(heart,this.transform.position,Quaternion.identity);
		}
        Destroy(this.gameObject);
	}
}
