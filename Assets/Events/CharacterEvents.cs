using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    public static UnityAction<GameObject, int> characterDmg;
    public static UnityAction<GameObject, int> characterHealed;
}