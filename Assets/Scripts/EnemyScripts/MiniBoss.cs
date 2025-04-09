using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : BaseEnemy
{
    private float flipCooldown = 0.3f;
    private float timeSinceLastFlip = 0f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        timeSinceLastFlip += Time.fixedDeltaTime;
        
        if (_touchingDirections.IsOnWall && _touchingDirections.IsGround  && timeSinceLastFlip > flipCooldown)
        {
            FlipDirection();
            timeSinceLastFlip = 0f;
        }

        // Если враг не заблокирован (не в анимации урона, например) и на земле – двигаем его
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
                float stoppedX = Mathf.Lerp(_rb.velocity.x, 0, 1);
                _rb.velocity = new Vector2(stoppedX, _rb.velocity.y);
            }
        }
    }
    
    public override void AttackSound()
    {
        SoundManager.instance.PlaySound2D("MiniBoss1Attack");
    }

    public override void HitSound()
    {
        SoundManager.instance.PlaySound2D("MiniBoss1Hit");
    }

    public override void DeathSound()
    {
        SoundManager.instance.PlaySound2D("MiniBoss1Death");
    }
}
