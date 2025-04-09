using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackForce = Vector2.zero;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Damagable damagable = other.GetComponent<Damagable>();
            if(damagable != null && damagable.IsAlive)
            {
                damagable.IsAlive = false;
            }
        }
    }
}