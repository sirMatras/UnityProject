using UnityEngine;

public class MiniBossZone : MonoBehaviour
{
    public GameObject[] miniBossHealthBars; 

    private void Start()
    {
        foreach (var bar in miniBossHealthBars)
        {
            if (bar != null)
                bar.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var bar in miniBossHealthBars)
            {
                if (bar != null)
                    bar.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var bar in miniBossHealthBars)
            {
                if (bar != null)
                    bar.SetActive(false);
            }
        }
    }
}