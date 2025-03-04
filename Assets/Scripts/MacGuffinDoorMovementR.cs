using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacGuffinDoorMovementR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator MoveDoor()
    {
        for (int i = 0; i < 40; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, 0.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
