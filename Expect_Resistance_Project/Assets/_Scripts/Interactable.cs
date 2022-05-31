using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] float interactionTime;
    [SerializeField] string userTip;
    public bool isActive;
    [SerializeField] public UnityEvent onIteraction;
    [SerializeField] public UnityEvent onProgress;
    [SerializeField] public UnityEvent onInterupt;

    bool allowInteraction = true;

    public float InteractionTime { get => interactionTime; }
    public string UserTip { get => userTip; }

    public void Interact()
    {
        isActive = false;
        onIteraction.Invoke();
    }
    public void Progress()
    {
        onProgress.Invoke();
    }

    public void Interupt()
    {
        onInterupt.Invoke();
    }
}