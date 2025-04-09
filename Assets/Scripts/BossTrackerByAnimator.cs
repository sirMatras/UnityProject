using UnityEngine;

public class BossTrackerByAnimator : MonoBehaviour
{
    public BossManager manager;
    private Animator animator;
    private bool isReportedDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isReportedDead && animator != null && !animator.GetBool("isAlive"))
        {
            isReportedDead = true;
            manager?.NotifyBossKilled();
        }
    }
}