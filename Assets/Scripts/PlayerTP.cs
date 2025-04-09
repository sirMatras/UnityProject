using UnityEngine;
using UnityEngine.UI;

public class PlayerTP : MonoBehaviour
{
    private GameObject currTeleporter;
    [SerializeField] private GameObject teleportPrompt;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currTeleporter != null)
            {
                transform.position = currTeleporter.GetComponent<Teleporter>().GetDestination().position;
                HidePrompt();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            currTeleporter = other.gameObject;
            var teleporter = currTeleporter.GetComponent<Teleporter>();
            if (teleporter != null && teleporter.ShouldShowPrompt())
            {
                ShowPrompt();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            if (other.gameObject == currTeleporter)
            {
                HidePrompt();
                currTeleporter = null;
            }
        }
    }

    private void ShowPrompt()
    {
        if (teleportPrompt != null)
            teleportPrompt.SetActive(true);
    }

    private void HidePrompt()
    {
        if (teleportPrompt != null)
            teleportPrompt.SetActive(false);
    }
}