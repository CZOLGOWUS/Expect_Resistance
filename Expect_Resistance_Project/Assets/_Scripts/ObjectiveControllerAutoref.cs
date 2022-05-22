using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveControllerAutoref : MonoBehaviour
{
    ObjectiveController controller;
    private void Awake()
    {
        controller = FindObjectOfType<ObjectiveController>();
        if (controller == null)
        {
            Debug.LogError("Cannot reference ObjectiveController");
        }
    }

    public void OnExit()
    {
        controller.TryExit();
    }

    public void OnMainComputer()
    {
        controller.MainComputerHacked();
    }

    public void OnAdditionalComputer()
    {
        controller.AdditionalComputerHacked();
    }
}
