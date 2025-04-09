using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class BaseEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkAccelaration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.6f;

    [Header("Detection Zones")]
    public DetectionZone attackZone;
    
    protected Rigidbody2D _rb;
    protected TouchingDirections _touchingDirections;
    protected Animator _animator;
    protected Damagable _damagable;

    public enum WalkableDirection
    {
        Left,
        Right
    }

    protected WalkableDirection _walkDirection = WalkableDirection.Right;
    protected Vector2 _walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (_damagable.LockVelocity) return;

            if (_walkDirection != value)
            {
                transform.localScale = new Vector2(
                    Mathf.Abs(transform.localScale.x) * (value == WalkableDirection.Right ? 1 : -1),
                    transform.localScale.y
                );

                _walkDirectionVector = (value == WalkableDirection.Right) ? Vector2.right : Vector2.left;
            }
            _walkDirection = value;
        }
    }

    protected bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget;
        protected set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get => _animator.GetBool(AnimationStrings.canMove);
    }

    public float AttackCooldown
    {
        get => _animator.GetFloat(AnimationStrings.AttackCooldown);
        protected set => _animator.SetFloat(AnimationStrings.AttackCooldown, Mathf.Max(value, 0));
    }

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _damagable = GetComponent<Damagable>();
    }

    protected virtual void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (!_animator.GetBool(AnimationStrings.isAlive))
        {
            _animator.SetBool(AnimationStrings.canMove, false);
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    protected virtual void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            WalkDirection = WalkableDirection.Right;
        }

        Debug.Log("Flipped. Now going " + WalkDirection);
    }

    public virtual void OnHit(int damage, Vector2 knockback)
    {
        Debug.Log($"{name} got hit! Knockback: {knockback}");
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }
    
    protected virtual void FixedUpdate()
    {
        if (_touchingDirections.IsOnWall && _touchingDirections.IsGround)
        {
            FlipDirection();
        }

        if (!_damagable.LockVelocity && _touchingDirections.IsGround)
        {
            if (CanMove)
            {
                float newX = Mathf.Clamp(
                    _rb.velocity.x + (walkAccelaration * _walkDirectionVector.x * Time.fixedDeltaTime),
                    -maxSpeed,
                    maxSpeed
                );

                _rb.velocity = new Vector2(newX, _rb.velocity.y);
            }
            else
            {
                float stoppedX = Mathf.Lerp(_rb.velocity.x, 0, walkStopRate);
                _rb.velocity = new Vector2(stoppedX, _rb.velocity.y);
            }
        }
    }
    
    public virtual void AttackSound()
    {
        SoundManager.instance.PlaySound2D("");
    }

    public virtual void HitSound()
    {
        SoundManager.instance.PlaySound2D("");
    }

    public virtual void DeathSound()
    {
        SoundManager.instance.PlaySound2D("");
    }
}
