using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    private static List<GameCharacter> gameCharacters = new List<GameCharacter>();
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float hitBlinkDuration;
    private bool isBlinking;
    protected SpriteRenderer spriteRenderer;
    private Vector2 movementDirection;
    protected float MaxSpeed
    {
        get { return maxSpeed; }
    }
    protected float Damage
    {
        get { return damage;  }
    }
    protected Vector2 MovementDirection
    {
        get { return movementDirection;  }
    }
    public float MaxHealth { get { return maxHealth;  } }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value > maxHealth)
                health = maxHealth;
            else if (value < 0)
            {
                health = 0;
                OnDeath();
            }
            else
                health = value;
        }
    }
    private void Start()
    {
        Register();
    }
    private void RestoreColor()
    {
        spriteRenderer.enabled = true;
        isBlinking = false;
    }
    protected void Register()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameCharacters.Add(this);
    }
    protected GameCharacter FindClosestGameChar(string tag)
    {
        GameCharacter closest = null;
        float distance = float.PositiveInfinity;
        foreach (GameCharacter gameChar in gameCharacters)
        {
           if (gameChar == this || gameChar.tag != tag)
            continue;

           if (closest == null)
           {
                closest = gameChar;
                distance = (gameChar.transform.position - transform.position).magnitude;
                continue;
           }

           float newDistance = (gameChar.transform.position - transform.position).magnitude;
           if (newDistance < distance)
           {
                closest = gameChar;
                distance = newDistance;
           }
        }
        return closest;
    }
    protected GameCharacter FindGameCharInDirection(Vector2 direction, float radius, int distance, string tag)
    {
        foreach (GameCharacter gameChar in gameCharacters)
        {
            if (gameChar == this || gameChar.tag != tag)
                continue;

            Vector2 myPos = transform.position;
            Vector2 gPos = gameChar.transform.position;
            
            for (int i = 0; i < distance; i++)
            {
                if ((gPos - (myPos + direction * i)).magnitude <= radius)
                {
                    return gameChar;
                }
            }
        }
        return null;
    }
    protected void Move(Vector3 movement)
    {
        transform.position += movement;
        movementDirection = movement.normalized;
    }
    protected virtual void OnDeath()
    {
        gameCharacters.Remove(this);
    }
    public void GetHit(float damage)
    {
        Health -= damage;
        if (spriteRenderer && !isBlinking)
        {
            isBlinking = true;
            spriteRenderer.enabled = false;
            Invoke("RestoreColor", hitBlinkDuration);
        }
    }
}
