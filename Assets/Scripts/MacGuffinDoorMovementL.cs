using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacGuffinDoorMovementL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator MoveDoor()
    {
        for (int i = 0; i < 40; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right, 0.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
