using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MacGuffinDoor : MonoBehaviour
{
    private Transform player;
    public GameObject uI;
    private PauseMenuBehavior uiScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject door;
    private PlayerControls controls;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        uiScript = uI.GetComponent<PauseMenuBehavior>();
        if (uiScript != null)
            Debug.Log("found ui");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
            if (gameManagerScript.macGuffin)
                uiScript.visible = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
            uiScript.visible = false;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        float distanceToPlayer = Vector3.Distance(player.position, door.transform.position);
        if (distanceToPlayer <= 4f)
        {
            if (gameManagerScript.macGuffin == true)
            {
                uiScript.visible = false;
                GameObject.Find("Canvas").GetComponent<PauseMenuBehavior>().WinScreen();
                Destroy(door);
                Destroy(gameObject);
            }
        }
    }
    private void OnEnable()
    {
        controls.Enable();

        // Bind input actions
        controls.Player.Interact.performed += OnInteract;
        controls.Player.Interact.canceled += OnInteract;

    }
    private void OnDisable()
    {
        controls.Disable();

        // Unbind input actions
        controls.Player.Interact.performed -= OnInteract;
        controls.Player.Interact.canceled -= OnInteract;
    }

}
