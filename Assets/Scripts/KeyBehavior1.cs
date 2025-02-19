using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior1 : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManagerScript.keyCard = true;
            Destroy(gameObject);
        }
    }
}
