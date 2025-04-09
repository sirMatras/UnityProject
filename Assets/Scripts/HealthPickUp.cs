using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private AudioSource pickupSource;
    public int healthHealed = 15;

    public Vector3 rotationSpeed = new Vector3(0, 180, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.eulerAngles += rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damagable damagable = other.GetComponent<Damagable>();

        if (damagable && damagable.Health < damagable.MaxHealth)
        {
            bool wasHealed = damagable.Heal(healthHealed);

            if (wasHealed)
            {
                if (pickupSource)
                {
                    SoundManager.instance.PlaySound2D("PickUpSound");
                }
                Destroy(gameObject);
            }
        }
    }
}
