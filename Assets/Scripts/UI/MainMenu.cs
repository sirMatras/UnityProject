using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    
    private void Start()
    {
        LoadVolume();
        MusicManager.Instance.PlayMusic("MenuMusic");
    }
    
    public void PlayFirstScene()
    {
        SceneManager.LoadSceneAsync("Level 1");
        MusicManager.Instance.PlayMusicWithIntro("ActionMusic1_intro", "ActionMusic1_loop");
    }
    
    public void PlaySecondScene()
    {
        SceneManager.LoadSceneAsync("Level 2");
        MusicManager.Instance.PlayMusicWithIntro("ActionMusic2_intro", "ActionMusic2_loop");
    }
    
    public void PlayThirdScene()
    {
        SceneManager.LoadSceneAsync("Level 3");
        MusicManager.Instance.PlayMusicWithIntro("ActionMusic3_intro", "ActionMusic3_loop");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
    }
    
    public void SaveVolume()
    {
        mixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
 
        mixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
 
    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}
