using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; 

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    private FirstPersonController firstPersonController; 
    private VideoPlayer videoPlayer; 

    void Start()
    {
       
        GameObject player = GameObject.FindWithTag("Player"); // 
        if (player != null)
        {
            firstPersonController = player.GetComponentInChildren<FirstPersonController>();
        }
        else
        {
            Debug.LogError("no anda la pausa del player");
        }

        GameObject screenObject = GameObject.Find("Screen");
        if (screenObject != null)
        {
            videoPlayer = screenObject.GetComponent<VideoPlayer>();
        }
        else
        {
            Debug.LogError("no anda el video pa"); //mira el quilombo que tuve q hacer para pausar a ricardo fort, ponganme un 10
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    
        if (firstPersonController != null)
        {
            firstPersonController.enabled = true;
        }

     
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false;
        }

       
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}