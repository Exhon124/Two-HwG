using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RobotBehavior;
using UnityEngine.AI;

public class RobotBehavior2 : MonoBehaviour
{
    public GameObject laser;
    public Light objectLight;
    public Transform[] waypoints;

    private NavMeshAgent agent;
    private Transform player;
    private int m_CurrentWaypointIndex = 0;
    public bool playerDetected = false;
    public bool playerSeen = false;
    public Vector3 lastKnownLoc;
    private bool chargingLaser = false;
    public int laserChargeTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;
        objectLight = GetComponent<Light>();
        agent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected == false)
        {
            objectLight.color = Color.white;
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }
        else
        {
            if (!playerSeen)
            {
                objectLight.color = Color.yellow;
                //set light yellow
                agent.SetDestination(lastKnownLoc); //take lastposition from observer as new waypoint
                int layerMask = ~(1 << LayerMask.NameToLayer("Laser"));
                Vector3 direction = player.position - transform.position;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.transform == player)
                    {
                        Debug.Log("waiting to find");
                        StartCoroutine(WaitFindPlayer());
                    }
                }
            }
            else
            {
                objectLight.color = Color.red;
                if (chargingLaser == false)
                {
                    chargingLaser = true;
                    StartCoroutine(ShootLaser());
                }
            }
        }
    }




    IEnumerator WaitFindPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        playerSeen = true;
        Debug.Log("wait and find player");
        int layerMask = ~(1 << LayerMask.NameToLayer("Laser"));
        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.transform == player)
            {
                playerSeen = true;
            }
        }
    }
    IEnumerator ShootLaser()
    {
        float elapsedTime = 0f;
        agent.ResetPath();
        while (elapsedTime < laserChargeTime)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / laserChargeTime);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        Debug.Log("shooting");
        Instantiate(laser, transform.position, Quaternion.identity);
        chargingLaser = false;
    }

}
