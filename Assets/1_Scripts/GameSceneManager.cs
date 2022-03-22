using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public AudioData BGM;

    private void Start()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        switch (buildIndex)
        {
            case 0:
                AudioManager.instance.PlayBGM(BGM, "Menu");
                break;
            case 1:
                AudioManager.instance.PlayBGM(BGM, "Gameplay");
                break;
        }
    }

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
