using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{
    Text objectiveTip;

    [SerializeField] string hackObjectiveUserTip;
    [SerializeField] string escapeObjectiveUserTip;
    private void Awake()
    {
        objectiveTip = gameObject.GetComponent<Text>();
        objectiveTip.text = hackObjectiveUserTip;
    }
    public void MainComputerHacked()
    {
        objectiveTip.text = escapeObjectiveUserTip;
    }

    public void AdditionalComputerHacked()
    {
        Debug.Log("Additional objective.");
    }
}
