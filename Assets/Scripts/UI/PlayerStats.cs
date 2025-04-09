using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public event Action OnStatsUpdated;

    public int damageDealt = 0;
    public int damageTaken = 0;

    public void ResetStats()
    {
        damageDealt = 0;
        damageTaken = 0;
        OnStatsUpdated?.Invoke();
    }

    public void AddDamageDealt(int amount)
    {
        damageDealt += amount;
        OnStatsUpdated?.Invoke();
    }

    public void AddDamageTaken(int amount)
    {
        damageTaken += amount;
        OnStatsUpdated?.Invoke();
    }
}