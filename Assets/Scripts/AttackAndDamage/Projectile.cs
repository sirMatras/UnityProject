using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    public int damage = 10;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damagable damagable = other.GetComponent<Damagable>();
        if (damagable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0
                ? knockback
                : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damagable.Hit(damage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log(other.name + " hit for " + damage);
            }

            Destroy(gameObject);
            return;
        }

        // 2. Попадание по "земле" (по Layer или Tag)
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
