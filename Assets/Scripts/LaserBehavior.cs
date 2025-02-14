using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed = 17f;
    public GameObject gameobject;
    private Vector3 direction;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
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

            Destroy(gameObject);
        }
        
    }
}
