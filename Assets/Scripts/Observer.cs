using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private Transform player;
    public GameObject laser;
    private Light objectLight;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Entered Detection Zone");
        m_IsPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
            m_IsPlayerInRange = false;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        objectLight = GetComponent<Light>();
    }

    void Update()
    {

        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            Debug.DrawRay(transform.position, direction * 100f, Color.red, 1f);

            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.Log("cast ray hit something");
                if (raycastHit.collider.transform == player)
                {
                    Debug.Log("firing");
                    shootLaser();
                    objectLight.color = Color.red;
                }
            }
        }
    }

    void shootLaser()
    {
        Vector3 targetPosition = player.position;
        // Spawn the projectile at the enemy's position
        GameObject projectile = Instantiate(laser, transform.position, Quaternion.identity);
    }
}