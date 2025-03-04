using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject firstSelectedButton;

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
