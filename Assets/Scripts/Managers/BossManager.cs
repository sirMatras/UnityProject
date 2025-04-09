using UnityEngine;
using UnityEngine.InputSystem;

public class BossManager : MonoBehaviour
{
    public StatsScreenUI statsScreenUI;

    private int totalBosses = 0;
    private int bossesKilled = 0;

    private void Start()
    {
        totalBosses = FindObjectsOfType<BossTrackerByAnimator>().Length;
    }

    public void NotifyBossKilled()
    {
        bossesKilled++;

        if (bossesKilled >= totalBosses)
        {
            var playerInput = FindObjectOfType<PlayerInput>();
            
            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            statsScreenUI.ShowStats();
        }
    }
}