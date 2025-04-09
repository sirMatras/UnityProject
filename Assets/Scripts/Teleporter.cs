using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform dest;
    [SerializeField] private bool showPrompt = false; 

    public Transform GetDestination()
    {
        return dest;
    }

    public bool ShouldShowPrompt()
    {
        return showPrompt;
    }
}