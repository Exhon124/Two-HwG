using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBehavior : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed = 26f;
    public GameObject gameobject;
    private Vector3 direction;
    private Transform player;
    public GameObject gameManager;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += direction * speed * Time.deltaTime;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyRobot"))
        {

        }else if (other.CompareTag("EnemyComponent")){

        }else
        {
            Destroy(gameObject);
        }




    }
}
