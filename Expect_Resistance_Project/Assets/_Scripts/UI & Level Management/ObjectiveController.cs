using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveController : MonoBehaviour
{
    Text objectiveTip;

    [Header("Level")]
    [SerializeField] int totalAdditionalComputers;
    [SerializeField] Interactable levelExit;

    [Header("UI")]
    [SerializeField] string hackObjectiveUserTip;
    [SerializeField] string escapeObjectiveUserTip;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject deathScreen;


    bool mainRequirement;
    int additionalComputers = 0;
    float totalTime;
    private void Awake()
    {
        objectiveTip = gameObject.GetComponent<Text>();
        objectiveTip.text = hackObjectiveUserTip;
        Time.timeScale = 1;
        levelExit.isActive = false;
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
    }
    public void MainComputerHacked()
    {
        objectiveTip.text = escapeObjectiveUserTip;
        mainRequirement = true;
        levelExit.isActive = true;
    }

    public void AdditionalComputerHacked()
    {
        Debug.Log("Additional objective.");
        additionalComputers++;
    }

    public void TryExit()
    {
        if (mainRequirement)
        {
            Time.timeScale = 0;
            timeText.text += (int)totalTime;
            scoreText.text += additionalComputers + "/" + totalAdditionalComputers;

            endScreen.SetActive(true);
            hud.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        else
            SceneManager.LoadScene("MainMenu");
    }
    public void OnPlayerDeath()
    {
        Time.timeScale = 0;

        deathScreen.SetActive(true);
        hud.SetActive(false);
    }
}
