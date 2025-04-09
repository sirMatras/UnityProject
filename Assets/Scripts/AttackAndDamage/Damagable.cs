using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    
    public PlayerStats playerStats;
    public UnityEvent<int, Vector2> damagableHit = new UnityEvent<int, Vector2>();
    public UnityEvent dmgDeath = new UnityEvent();
    public UnityEvent<int, int> healthChanged = new UnityEvent<int, int>();
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    
    
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set +" + value);

            if (value == false)
            {
                dmgDeath.Invoke();
            }
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            
            healthChanged?.Invoke(_health, MaxHealth);

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(LockVelocity);
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;    
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDmg.Invoke(gameObject, damage);
            playerStats?.AddDamageTaken(damage);

            return true;
        }

        return false;
    }

    public bool Heal(int health)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Math.Min(maxHeal, health);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
