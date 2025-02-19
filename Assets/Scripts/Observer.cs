using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameObject robot;
    private RobotBehavior2 robotScript; // Reference to parent RobotBehavior
    private Transform player;
    private float detectDelay = 0.4f;
    private float nextDetect = 0.4f;
    private bool check = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        robotScript = robot.GetComponent<RobotBehavior2>();
        if (robotScript != null)
        {
            Debug.Log("got it");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            nextDetect = Time.time + detectDelay;
            robotScript.lastKnownLoc = player.position;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform == player)
        {
            if (Time.time >= nextDetect)
            {
                robotScript.playerDetected = true;
            }
            check = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        check = false;
        StartCoroutine(waitForCheck());
        //set a variable to false
        //wait a bit
        //if variable is still false then turn heat down one
        
    }

    private void Update()
    {
        
    }

    IEnumerator waitForCheck()
    {
        yield return new WaitForSeconds(5);
        if (check == false)
        {
            if (robotScript.playerSeen == true)
            {
                robotScript.playerSeen = false;
            } 
            else if (robotScript.playerDetected == true)
            {
                robotScript.playerDetected = false;
            }
        }
    }
}
