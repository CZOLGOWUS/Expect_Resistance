using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject levelSeclectionGridPanel;
    [SerializeField] GameObject levelButtonPrefab;
    public const string BASE_LEVEL_NAME = "level_";

    void Start()
    {
        CreateSelectLevelMenu();
    }

    public void LoadLevel(int i)
    {
        string sceneName = BASE_LEVEL_NAME + i;
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

    public void CreateSelectLevelMenu()
    {
        int levelIndex = 1;
        string sceneName = BASE_LEVEL_NAME + levelIndex;
        while(Application.CanStreamedLevelBeLoaded(sceneName))
        {
            GameObject currentButton = Instantiate(levelButtonPrefab, levelSeclectionGridPanel.transform);
            currentButton.GetComponentInChildren<Text>().text = levelIndex.ToString();
            int levelIndex_ = levelIndex; // this is necessary!
            currentButton.GetComponent<Button>().onClick.AddListener(() => { LoadLevel(levelIndex_); });

            levelIndex++;
            sceneName = BASE_LEVEL_NAME + levelIndex;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
