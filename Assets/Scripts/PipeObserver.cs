using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeObserver : MonoBehaviour
{
    private Transform player;
    public Transform robot;
    private RobotBehavior2 robotScript;
    public GameObject gameManager;
    private GameManager gameManagerScript;
    public bool wakeUp = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        robot = GameObject.FindWithTag("EnemyRobot")?.transform;
        robotScript = robot.GetComponent<RobotBehavior2>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(DontHitPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pick up: " + other);
        if (other.transform == player)
        {
            if(wakeUp)
            {
                gameManagerScript.hasPipe = true;
                Debug.Log("Pick up");
                Destroy(transform.parent.gameObject);
            }
        }
        if (other.transform == robot)
        {
            robotScript.glitched = true;
            Debug.Log("Hit");
        }
    }
    IEnumerator DontHitPlayer()
    {
        yield return new WaitForSeconds(1);
        wakeUp = true;
    }
}
