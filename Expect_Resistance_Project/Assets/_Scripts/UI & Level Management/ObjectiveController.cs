using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{
    Text objectiveTip;
    Interactable exit;

    [SerializeField] string hackObjectiveUserTip;
    [SerializeField] string escapeObjectiveUserTip;

    bool mainRequirement;
    private void Awake()
    {
        objectiveTip = gameObject.GetComponent<Text>();
        objectiveTip.text = hackObjectiveUserTip;
        exit = GameObject.Find("Exit").GetComponent<Interactable>();
    }
    public void MainComputerHacked()
    {
        objectiveTip.text = escapeObjectiveUserTip;
        mainRequirement = true;
        exit.isActive = true;
    }

    public void AdditionalComputerHacked()
    {
        Debug.Log("Additional objective.");
    }

    public void TryExit()
    {
        if (mainRequirement)
        {
            Debug.Log("WIN");
        }
    }
}
