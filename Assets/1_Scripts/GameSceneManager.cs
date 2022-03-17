using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void SwitchScene(int indexBuild)
    {
        SceneManager.LoadScene(indexBuild);
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game is closed");
    }
}
