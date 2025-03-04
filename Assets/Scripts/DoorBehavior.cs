using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Transform player;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    private Vector3 targetPosition;
    public GameObject thisDoor;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player")?.transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator MoveDoor()
    {
        for (int i = 0; i < 600; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, 0.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

}
