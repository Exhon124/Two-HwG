using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Transform player;
    public GameObject gameManager;
    private Vector3 targetPosition;
    public GameObject thisDoor;
    private Animator doorAnimator; // Reference to the Animator
    // Start is called before the first frame update
    void Start()
    {
        
        doorAnimator = GetComponent<Animator>(); // Get Animator component
    }

    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open"); // Trigger Open animation
        }
    }

    public void CloseDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Close"); // Trigger Close animation (optional)
        }
    }
}
/*
void OnTriggerEnter(Collider other)
{
    if (other.transform == player) // If player enters trigger area
    {
        if (gameManagerScript.keyCard) // If player has keycard
        {
            doorAnimator.SetTrigger("Open"); // Play door open animation
        }
    }
}

void OnTriggerExit(Collider other)
{
    if (other.transform == player) // If player exits trigger area
    {
        doorAnimator.SetTrigger("Close"); // Play door close animation
    }
}
}
*/
