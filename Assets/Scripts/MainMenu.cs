using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Stowaway_Hall");
    }

    public void quitGame()
    {
        quitGame();
    }

    public void loadArtScene()
    {
        SceneManager.LoadScene("Museum_Scene");
    }
}
