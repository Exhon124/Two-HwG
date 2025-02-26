using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObserver : MonoBehaviour
{
    private Transform player;
    public GameObject uI;
    private PauseMenuBehavior uiScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        uiScript = uI.GetComponent<PauseMenuBehavior>();
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
                if (gameManagerScript.keyCard == true)
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
            if (gameManagerScript.keyCard)
                uiScript.visible = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
            uiScript.visible = false;
    }
}
