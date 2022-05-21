using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int i)
    {
        string sceneName = "level_" + i;
        if(Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Invalid scene name \"" + sceneName + "\"");
        }

    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
