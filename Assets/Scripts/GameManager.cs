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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (hasPipe == true)
        {

                Debug.Log("Throw");
                hasPipe = false;
                Vector3 spawnPosition = player.position + player.transform.forward * 4;

                GameObject pipe = Instantiate(pipePrefab, spawnPosition, player.transform.rotation);

                Rigidbody rb = pipe.GetComponent<Rigidbody>();
                rb.velocity = player.transform.forward * 10;

        }

    }
    private void OnEnable()
    {
        controls.Enable();

        // Bind input actions
        controls.Player.Throw.performed += OnThrow;
        controls.Player.Throw.canceled += OnThrow;

    }
    private void OnDisable()
    {
        controls.Disable();

        // Unbind input actions
        controls.Player.Throw.performed -= OnThrow;
        controls.Player.Throw.canceled -= OnThrow;
    }
}
