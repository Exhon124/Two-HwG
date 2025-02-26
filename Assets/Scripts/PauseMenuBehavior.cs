using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{

    public bool GameIsPaused = false;
    public GameObject player;
    public FirstPersonController playerScript;

    public GameObject pauseMenuUI;
    public bool visible = false;
    public GameObject interactUI;
    private bool coverUp = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        playerScript = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
        if (coverUp)
            interactUI.SetActive(false);
        else 
            interactUI.SetActive(visible);
    }


    public void Resume()
    {
        playerScript.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerScript.cameraCanMove = true;
        pauseMenuUI.SetActive(false);
        coverUp = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        playerScript.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
        playerScript.cameraCanMove = false;
        pauseMenuUI.SetActive(true);
        coverUp = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene (sceneName:"MainMenu");
    }

}
