using UnityEngine;
 
[System.Serializable]
public struct SoundEffect
{
    public string id;
    public AudioClip[] audios;
}
 
public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;
 
    public AudioClip GetClipFromName(string name)
    {
        foreach (var sound in soundEffects)
        {
            if (sound.id == name)
            {
                return sound.audios[Random.Range(0, sound.audios.Length)];
            }
        }
        return null;
    }   
}