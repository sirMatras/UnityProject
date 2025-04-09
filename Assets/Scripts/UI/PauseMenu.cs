using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    public GameObject pausedMenu;
    public static bool isPaused;
    
    // Start is called before the first frame update
    void Start()
    {
        pausedMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausedMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        playerInput.enabled = false; 
    }

    public void Resume()
    {
        pausedMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        playerInput.enabled = true;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
