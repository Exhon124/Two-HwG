using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObserver : MonoBehaviour
{
    private Transform player;
    public GameObject robot;
    private RobotBehavior2 robotScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        robotScript = robot.GetComponent<RobotBehavior2>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            gameManagerScript.hasPipe = true;
            Debug.Log("Pick up");
            Destroy(transform.parent.gameObject);

        }
        if (other == robot)
        {
            robotScript.glitched = true;
            
        }
    }
}
