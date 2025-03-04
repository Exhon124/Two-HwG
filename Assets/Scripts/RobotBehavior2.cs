using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public Image endCanvas;
    public bool glitched = false;
    public float glitchCooldown = 4f;
    public AudioSource EnemyRobot;
    public AudioSource GameDJ;
    public AudioSource GuardV;
    public AudioClip PlayerSeen, Charge, PlayerLost, Spotted;
    private bool playerLostAudioPlayed = false; // Track if PlayerLost is currently playing
    private bool wasPlayerDetectedLastFrame = false; // Flag to track last frame state of detection
    private AudioClip originalTheme;
    private bool playerSeenAudioPlayed = false; // Flag for PlayerSeen audio

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;
        objectLight = GetComponent<Light>();
        agent.SetDestination(waypoints[0].position);

        if (GameDJ.clip != null) // Ensure there's an original theme
        {
            originalTheme = GameDJ.clip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (glitched == true)
        {
            objectLight.color = Color.green;
            StartCoroutine(RepairSequence());
            agent.ResetPath();
        }
        else if (playerDetected == false)
        {
            objectLight.color = Color.white;

            // Check if the enemy previously detected the player and now lost detection
            if (wasPlayerDetectedLastFrame && !playerDetected)
            {
                
                if (!playerLostAudioPlayed)
                {

                    playerLostAudioPlayed = true;

                    if (GameDJ.clip != PlayerLost)
                    {
                        GameDJ.clip = PlayerLost;
                        GameDJ.Play();
                    }

                    StartCoroutine(RestoreMainTheme(PlayerLost.length));



                    agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

                }
            }
            if (playerDetected)
            {
                playerLostAudioPlayed = false;
            }
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }

        // Reset the flag when the player is detected again
        

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
                        StartCoroutine(WaitFindPlayer());
                    }
                }
            }
            else
            {
                
                objectLight.color = Color.red;

                if (!playerSeenAudioPlayed && GameDJ != Spotted)
                {
                    Debug.Log(" Player seen! Playing PlayerSeen sound...");
                    GuardV.clip = PlayerSeen; // Set the audio clip to PlayerSeen
                    GuardV.Play(); // Play the sound
                    playerSeenAudioPlayed = true; // Prevent the sound from playing again

                    GameDJ.clip = Spotted;
                    GameDJ.Play();

                    
                }

                if (chargingLaser == false)
                {
                    EnemyRobot.clip = Charge;
                    EnemyRobot.Play();
                    chargingLaser = true;
                    StartCoroutine(ShootLaser());
                }
            }
        }
    }

    // Coroutine to restore the original theme after the sound effect ends
    IEnumerator RestoreMainTheme(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for PlayerLost to finish
        if (GameDJ.clip != originalTheme) // Only reset if the clip has changed
        {
            GameDJ.clip = originalTheme;
            GameDJ.Play(); // Resume the original theme
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

        if (playerDetected)
            Instantiate(laser, transform.position, Quaternion.identity);
        chargingLaser = false;
    }

    IEnumerator WaitFindPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        playerSeen = true;
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

    IEnumerator RepairSequence()
    {
        yield return new WaitForSeconds(glitchCooldown);
        glitched = false;
    }

}
