using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayTheGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HelpScene()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
