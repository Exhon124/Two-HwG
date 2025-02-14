using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public RobotBehavior robot; // Reference to parent RobotBehavior
    private Transform player;
    private bool m_IsPlayerInRange;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
            robot.SetState(RobotBehavior.EnemyState.Search);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_IsPlayerInRange = false;
        robot.SetState(RobotBehavior.EnemyState.Passive);
    }

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            int layerMask = ~(1 << LayerMask.NameToLayer("Laser"));
            Vector3 direction = player.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            Debug.DrawRay(transform.position, direction * 100f, Color.red, 1f);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.transform == player)
                {
                    robot.SetState(RobotBehavior.EnemyState.Firing);
                }
                else
                {
                    robot.SetState(RobotBehavior.EnemyState.Search);
                }
            }
        }
    }
}
