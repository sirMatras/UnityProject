using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FlyingEye : MonoBehaviour
{
    public Collider2D deathCol; 
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    private Damagable _damagable;
    private Animator _animator;
    private Rigidbody2D _rb;

    private int waypointNum = 0;
    private Transform nextWaypoint;
    
    public bool hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return hasTarget;
        }
        private set
        {
            hasTarget = value;
            _animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _damagable = GetComponent<Damagable>();

    }

    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnabled()
    {
        _damagable.dmgDeath.AddListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }
    
    private void FixedUpdate()
    {
        if (_damagable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                _rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        
        _rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        
        if (transform.localScale.x > 0)
        {
            if (_rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (_rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath() 
    {
        _rb.gravityScale = 2f;
        deathCol.enabled = true;
    }

    public void AttackSound()
    {
        SoundManager.instance.PlaySound2D("FlyingEyeAttack");
    }
    
    public void DeathSound()
    {
        SoundManager.instance.PlaySound2D("PlayerDeathSound");
    }
    
    public void HitSound()
    {
        SoundManager.instance.PlaySound2D("FlyingEyeHit");
    }
}
