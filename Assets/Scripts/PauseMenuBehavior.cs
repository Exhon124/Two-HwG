using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private PlayerControls controls;

    //WIN/LOSE
    public GameObject winScreenUI;
    public GameObject loseScreenUI;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
        winScreenUI.SetActive(false);
        loseScreenUI.SetActive(false);
        playerScript = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }
    // Update is called once per frame
    void Update()
    {
        if (coverUp)
            interactUI.SetActive(false);
        else
            interactUI.SetActive(visible);

    }
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (GameIsPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        playerScript.lockCursor = true;
        playerScript.playerCanMove = true;
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
        playerScript.playerCanMove = false;
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

    public void WinScreen()
    {
        Pause();
        pauseMenuUI.SetActive (false);
        winScreenUI.SetActive (true);
    }

    public void LoseScreen()
    {
        Pause();
        pauseMenuUI.SetActive (false);
        loseScreenUI.SetActive(true);
    }

    private void OnEnable()
    {
        controls.Enable();

        // Bind input actions
        controls.Player.Menu.performed += OnMenu;


    }
    private void OnDisable()
    {
        controls.Disable();

        // Unbind input actions
        controls.Player.Menu.performed -= OnMenu;

    }

}
