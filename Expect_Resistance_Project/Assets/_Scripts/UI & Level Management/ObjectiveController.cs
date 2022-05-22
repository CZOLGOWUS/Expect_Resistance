using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveController : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] int totalAdditionalComputers;
    [SerializeField] Interactable levelExit;

    [Header("UI")]
    [SerializeField] string hackObjectiveUserTip;
    [SerializeField] string escapeObjectiveUserTip;
    [SerializeField] Text objectiveText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject pauseScreen;


    bool mainRequirement;
    int additionalComputers = 0;
    float totalTime;
    bool isPaused;
    bool isInMenu;

    private void Awake()
    {
        objectiveText.text = hackObjectiveUserTip;
        Time.timeScale = 1;
        if (levelExit == null)
        {
            levelExit = GameObject.Find("Exit").GetComponent<Interactable>();
        }
        levelExit.isActive = false;
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
    }
    public void MainComputerHacked()
    {
        objectiveText.text = escapeObjectiveUserTip;
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
            isInMenu = true;
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

        isInMenu = true;
    }

    public void OnPause()
    {
        if(!isInMenu)
        {
            isPaused = !isPaused;
            hud.SetActive(!isPaused);
            pauseScreen.SetActive(isPaused);

            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
