using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : BaseEnemy
{
    public DetectionZone cliffDetectionZone;
    protected override void FixedUpdate()
    {
        if ((_touchingDirections.IsOnWall && _touchingDirections.IsGround) ||
            (cliffDetectionZone.detectedColliders.Count == 0 && _touchingDirections.IsGround))
        {
            FlipDirection();
        }

        // Если враг не заблокирован (не в анимации урона, например) и на земле – двигаем его
        else if (!_damagable.LockVelocity && _touchingDirections.IsGround)
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
    
    public override void AttackSound()
    {
        SoundManager.instance.PlaySound2D("SamuraiAttack");
    }

    public override void HitSound()
    {
        SoundManager.instance.PlaySound2D("SamuraiHit");
    }

    public override void DeathSound()
    {
        SoundManager.instance.PlaySound2D("SamuraiDeath");
    }
}
