using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractVisibility : MonoBehaviour
{
    public bool visible = false;
    public TextMeshProUGUI text;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (visible)
            text.color = Color.white;
        else
            text.color = Color.clear;
        
    }
}
