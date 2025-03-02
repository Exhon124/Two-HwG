using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractVisibility : MonoBehaviour
{
    public bool visible = false;
    public GameObject interactUI;

    void Update()
    {
        interactUI.SetActive(visible);
    }
}
