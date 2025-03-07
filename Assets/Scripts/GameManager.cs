using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool keyCard = false;
    public bool macGuffin = false;
    public Color endCanvasColor = Color.clear;
    public bool hasPipe = false;
    private Transform player;
    public GameObject pipePrefab;
    private PlayerControls controls;
    public AudioSource enemyRobot;
    public AudioSource GameDJ;
    public AudioClip mainTheme, spotted, ExitRun;
    private bool PlayedExitRun = false; // Flag for PlayerSeen audio
    public FirstPersonController playerScript;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        GameDJ.clip = mainTheme;
        GameDJ.Play();
        playerScript = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (macGuffin == true && GameDJ.clip != ExitRun)
        {
            GameDJ.clip = ExitRun;
            GameDJ.Play();

            PlayedExitRun = true;
        }
            
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (playerScript.playerCanMove)
        {
            if (hasPipe == true)
            {
                Debug.Log("Throw");
                hasPipe = false;
                Vector3 spawnPosition = (player.position + player.transform.forward * 2) + player.transform.up;

                GameObject pipe = Instantiate(pipePrefab, spawnPosition, player.transform.rotation);

                Rigidbody rb = pipe.GetComponent<Rigidbody>();
                rb.velocity = player.transform.forward * 7;

                rb.angularVelocity = player.transform.right * 10; // Spins around its side axis
            }
        }
    }
    private void OnEnable()
    {
        controls.Enable();

        // Bind input actions
        controls.Player.Throw.performed += OnThrow;


    }
    private void OnDisable()
    {
        controls.Disable();

        // Unbind input actions
        controls.Player.Throw.performed -= OnThrow;
    }




}
