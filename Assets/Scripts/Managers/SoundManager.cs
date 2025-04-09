using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
 
    [SerializeField]
    private SoundLibrary sfxStore;
    [SerializeField]
    private AudioSource sfx2DSource;
 
    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }
 
    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxStore.GetClipFromName(soundName), pos);
    }
 
    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(sfxStore.GetClipFromName(soundName));
    }
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}