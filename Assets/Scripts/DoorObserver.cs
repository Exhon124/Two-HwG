using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObserver : MonoBehaviour
{
    private Transform player;
    public GameObject uI;
    private InteractVisibility uiScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject door;
    private DoorBehavior doorBehaviorScript;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        uiScript = uI.GetComponent<InteractVisibility>();
        if (uiScript != null)
            Debug.Log("found ui");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        doorBehaviorScript = door.GetComponent<DoorBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (uiScript.visible)
            {
                if (gameManagerScript.keyCard == true)
                {
                    doorBehaviorScript.OpenSesame();
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
            uiScript.visible = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
            uiScript.visible = false;
    }
}
