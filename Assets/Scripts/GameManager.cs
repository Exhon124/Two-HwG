using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool keyCard = false;
    public bool macGuffin = false;
    public Color endCanvasColor = Color.clear;
    public bool hasPipe = false;
    private Transform player;
    public GameObject pipePrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPipe == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Throw");
                hasPipe = false;
                Vector3 spawnPosition = player.position + player.transform.forward * 4;

                GameObject pipe = Instantiate(pipePrefab, spawnPosition, player.transform.rotation);

                Rigidbody rb = pipe.GetComponent<Rigidbody>();
                rb.velocity = player.transform.forward * 10;


            }
        }
    }

}
