using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool paused;

    private void Awake()
    {
        paused = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                Pause();
                paused = true;
            }
            else
            {
                Resume();
                paused = false;
            }
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
