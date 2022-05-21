using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] float interactionTime;
    [SerializeField] string userTip;
    [SerializeField] UnityEvent onIteraction;

    public float InteractionTime { get => interactionTime; }
    public string UserTip { get => userTip; }

    void Update()
    {

    }

    public void Interact()
    {
        // play animation
        onIteraction.Invoke();
    }
}
