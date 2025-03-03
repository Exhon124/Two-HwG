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
    public AudioSource Lazer;
    public AudioClip imit, touchL;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        gameManagerScript = gameManager.GetComponent<GameManager>();
        Lazer.clip = imit;
        Lazer.Play();
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += direction * speed * Time.deltaTime;

    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the "CollisionTag"
        if (collision.gameObject.CompareTag("CollisionTag"))
        {
            // Play the collision sound
            Lazer.clip = touchL;
            Lazer.Play();

            // Destroy the projectile
            Destroy(gameObject);
        }
            void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyRobot"))
        {
           
        }
        
        
        
            
        }

    }
}
