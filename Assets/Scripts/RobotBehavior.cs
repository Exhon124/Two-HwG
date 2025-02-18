using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBehavior : MonoBehaviour
{
    public enum EnemyState { Passive, Search, Firing }

    public GameObject laser;
    public Light objectLight;
    public Transform[] waypoints;

    private NavMeshAgent agent;
    private Transform player;
    private EnemyState currentState = EnemyState.Passive;
    private int m_CurrentWaypointIndex = 0;
    private float fireRate = 2f;
    private float nextFireTime = 0f;
    private Coroutine modeChange;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;
        objectLight = GetComponent<Light>();
        agent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Passive:
                if (modeChange == null) modeChange = StartCoroutine(PassiveBehavior());
                break;
            case EnemyState.Search:
                if (modeChange == null) modeChange = StartCoroutine(SearchBehavior());
                break;
            case EnemyState.Firing:
                if (modeChange == null) modeChange = StartCoroutine(FiringBehavior());
                break;
        }
    }

    public void SetState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            if (modeChange != null)
            {
                StopCoroutine(modeChange);
                modeChange = null;
            }
        }
    }

    IEnumerator PassiveBehavior()
    {
        objectLight.color = Color.white;

        while (currentState == EnemyState.Passive) // Keep running while in Passive state
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }

            yield return new WaitForSeconds(0.5f); // Small delay before checking again
        }
    }


    IEnumerator SearchBehavior()
    {
        objectLight.color = Color.yellow;
        yield return new WaitForSeconds(1);
    }

    IEnumerator FiringBehavior()
    {
        objectLight.color = Color.red;
        yield return new WaitForSeconds(1);

        if (Time.time >= nextFireTime)
        {
            int layerMask = ~(1 << LayerMask.NameToLayer("Laser"));
            Vector3 direction = player.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.transform == player)
                {
                    ShootLaser();
                    nextFireTime = Time.time + fireRate;
                }
                else
                {
                    SetState(EnemyState.Search);
                }
            }
        }
    }

    private void ShootLaser()
    {
        Instantiate(laser, transform.position, Quaternion.identity);
    }
}
