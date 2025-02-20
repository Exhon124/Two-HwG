using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacGuffin : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManagerScript.macGuffin = true;
            Destroy(gameObject);
        }
    }
}
