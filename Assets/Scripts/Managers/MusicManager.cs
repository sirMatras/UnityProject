using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        StopAllCoroutines(); // Останавливаем любые другие треки
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }

    public void PlayMusicWithIntro(string introName, string loopName)
    {
        StopAllCoroutines(); // останавливаем всё, что играло раньше
        StartCoroutine(PlayIntroThenLoop(introName, loopName));
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }

    IEnumerator PlayIntroThenLoop(string introName, string loopName)
    {
        AudioClip intro = musicLibrary.GetClipFromName(introName);
        AudioClip loop = musicLibrary.GetClipFromName(loopName);

        musicSource.volume = 1f;
        musicSource.clip = intro;
        musicSource.loop = false;
        musicSource.Play();

        yield return new WaitForSeconds(intro.length);

        musicSource.clip = loop;
        musicSource.loop = true;
        musicSource.Play();
    }
}
