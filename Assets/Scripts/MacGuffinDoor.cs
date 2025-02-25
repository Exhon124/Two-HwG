using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacGuffinDoor : MonoBehaviour
{
    private Transform player;
    public GameObject uI;
    private InteractVisibility uiScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        uiScript = uI.GetComponent<InteractVisibility>();
        if (uiScript != null)
            Debug.Log("found ui");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, door.transform.position);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (distanceToPlayer <= 4f)
            {
                if (gameManagerScript.macGuffin == true)
                {
                    uiScript.visible = false;
                    Destroy(door);
                    Destroy(gameObject);
                }
            }
        }
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
}
