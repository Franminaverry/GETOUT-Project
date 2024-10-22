using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Make sure to include this namespace

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    private FirstPersonController firstPersonController; // Reference to the player movement script
    private VideoPlayer videoPlayer; // Reference to the VideoPlayer component

    void Start()
    {
        // Find the FirstPersonController component in the Player object
        GameObject player = GameObject.FindWithTag("Player"); // Ensure your Player has the "Player" tag
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
        // Hide the cursor and lock it to the center
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Re-enable the player movement
        if (firstPersonController != null)
        {
            firstPersonController.enabled = true;
        }

        // Resume the video playback
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
        // Show the cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Disable the player movement
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false;
        }

        // Pause the video playback
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    private void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("title");
    }

    private void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}