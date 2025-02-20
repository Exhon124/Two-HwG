using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed = 26f;
    public GameObject gameobject;
    private Vector3 direction;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        Debug.Log("Made laser");
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

        }else {
            Debug.Log("destroy laser");
            Destroy(gameObject);
        }
        
    }
}
