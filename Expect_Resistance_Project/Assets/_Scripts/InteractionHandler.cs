using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] float interactionRadius;
    [SerializeField] Text playerTip;
    [SerializeField] GameObject progressBarParent;
    RectTransform progressBar;

    Collider2D[] detectedObjects = new Collider2D[10];
    Interactable nearestInteractable;
    Interactable previousInteractable;
    bool isInteractPressed;
    float timer;
    
    public void OnInteraction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isInteractPressed = true;
        }
        else if (ctx.canceled)
        {
            isInteractPressed = false;
            timer = 0f;
        }
    }

    private void Awake()
    {
        progressBar = progressBarParent.GetComponentsInChildren<RectTransform>()[1];
    }

    void Update()
    {
        Interactable temp;
        float minDistance = float.MaxValue;
        float tempMag;

        ClearDetectedObjects();
        previousInteractable = nearestInteractable;
        nearestInteractable = null;
        Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, interactionRadius, detectedObjects);
        for (int i = 0; i < detectedObjects.Length; i++)
        {
            if (detectedObjects[i] == null) break;

            temp = detectedObjects[i].gameObject.GetComponent<Interactable>();
            if (temp!=null && temp.isActive) 
            {
                tempMag = (detectedObjects[i].transform.position - gameObject.transform.position).sqrMagnitude;
                if (minDistance > tempMag)
                {
                    minDistance = tempMag;
                    nearestInteractable = temp;
                }
            }
        }

        if (nearestInteractable != null)
        {
            if (previousInteractable != nearestInteractable)
            {
                timer = 0f;
                if (playerTip != null)
                {
                    playerTip.text = "[E] " + nearestInteractable.UserTip; // this [E] should not be defined here, but it's good for now
                    if(nearestInteractable.InteractionTime>0) progressBarParent.SetActive(true);
                }
            }

            if (isInteractPressed)
            {
                timer += Time.deltaTime;
                nearestInteractable.Progress();
                if (timer > nearestInteractable.InteractionTime)
                {
                    nearestInteractable.Interact();
                    timer = 0f;
                }
            }
            else
            {
                nearestInteractable.Interupt();
            }

            if(nearestInteractable.InteractionTime != 0)
                progressBar.localScale = new Vector3(timer / nearestInteractable.InteractionTime, 1);
        }
        else
        {
            playerTip.text = "";
            progressBarParent.SetActive(false);
            timer = 0f;
        }

        
    }

    private void ClearDetectedObjects()
    {
        for (int i = 0; i < detectedObjects.Length; i++)
        {
            detectedObjects[i] = null;
        }
    }

    public void HandlePauseButton()
    {
        FindObjectOfType<ObjectiveController>().OnPause();
    }

}
