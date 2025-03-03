using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorObserver : MonoBehaviour
{
    private Transform player;
    public GameObject uI;
    private PauseMenuBehavior uiScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject door;
    private DoorBehavior doorBehavior;
    private Animator doorAnimator;
    private PlayerControls controls;
    private bool doorOpen = false; // Track if door is open
    private bool isPlayerNearby = false; // Track if player is near

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        uiScript = uI.GetComponent<PauseMenuBehavior>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        doorAnimator = door.GetComponent<Animator>();
        uI.SetActive(false);
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }
    // Update is called once per frame


    private void OnEnable()
    {

        if (controls == null) // Ensure controls is not null
        {
            controls = new PlayerControls();
        }
        controls.Enable();
        controls.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Player.Interact.performed -= OnInteract;
            controls.Disable();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            uI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            uI.SetActive(false); // Hide UI when player leaves
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && gameManagerScript.keyCard)
        {
            doorBehavior.OpenDoor(); // Open the door
            uI.SetActive(false); // Hide UI after opening
        }
    }
}
/*
void Update()
{
   // float distanceToPlayer = Vector3.Distance(player.position, door.transform.position);
}
void OnTriggerEnter(Collider other)
{
    if (other.transform == player)
    {
        if (gameManagerScript.keyCard)
        {
            uiScript.visible = true;
            if (!doorOpen) // Only open if not already open
            {
                doorAnimator.SetTrigger("Open");
                doorOpen = true; // Set door state as open
            }
        }
        else
        {
            uiScript.visible = false;
            // Optionally show a message or prevent opening if the card is missing
        }
    }
}


void OnTriggerExit(Collider other)
{
    if (other.transform == player)
    {
        uiScript.visible = false;
        if (doorOpen) // Only close if it was opened
        {
            doorAnimator.SetTrigger("Close");
            doorOpen = false; // Set door state as closed
        }
}
}
public void OnInteract(InputAction.CallbackContext context)
{
    float distanceToPlayer = Vector3.Distance(player.position, door.transform.position);
    if (distanceToPlayer <= 4f)
    {
        if (gameManagerScript.keyCard)
        {
            gameManagerScript.keyCard = false; // Consume keycard
            doorAnimator.SetTrigger("Open");
            doorOpen = true;

        }
    }

    // if (gameManagerScript.keyCard == true)
    //{
          //  uiScript.visible = false;
           // doorAnimator.SetTrigger("DoorOpen");

       // }
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
*/
