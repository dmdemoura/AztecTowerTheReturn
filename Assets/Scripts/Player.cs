using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : GameCharacter {
	[SerializeField]	private LayerMask Enemies;
	public int hearts;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDistance;
    private CooldownTimer attackCooldown;
	private Rigidbody2D rb;
    private Animator animator;

	void Start () {
        Register();
        attackCooldown = new CooldownTimer(attackDelay);
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

	void FixedUpdate () {

        //Animation
        if (Input.GetAxis("Horizontal") == 0) animator.SetTrigger("startIdle");
        animator.SetFloat("direction", Input.GetAxis("Horizontal"));

        //Movement
        Move(new Vector2(Input.GetAxis("Horizontal") * MaxSpeed, Input.GetAxis("Vertical") * MaxSpeed));

		//Attacking
        attackCooldown.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (attackCooldown.Available)
            {
                GameCharacter enemy = FindGameCharInDirection(MovementDirection, attackRadius, attackDistance, "Enemy");
                if (enemy != null)
                {
                    attackCooldown.Activate();
                    enemy.SendMessage("GetHit", Damage);
                    enemy.SendMessage("DrawAggro");
                }
            }
        } 

	}
    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }

    public void PickUpHeart(){
		hearts++;
	}
}		