using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused=false;
    public GameObject pauseMenuUI;
    public AudioSource hats;
    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                
                Resume();
            }
            else
            {
                
                Pause();
            }
        }
	}
    public void Resume()
    {
        hats.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    void Pause()
    {
        hats.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("QUITTING GAME.....");
        Application.Quit();
    }
}
