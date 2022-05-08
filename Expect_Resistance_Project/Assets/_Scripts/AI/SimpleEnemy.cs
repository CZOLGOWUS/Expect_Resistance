using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noGame.Characters;

[RequireComponent(typeof(AICharacterController2D))]
public class SimpleEnemy : MonoBehaviour
{
    AICharacterController2D controller;
    GameObject targetObject;
    [SerializeField] Vector3 targetDirection;

    void Awake()
    {
        controller = GetComponent<AICharacterController2D>();
    }

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(targetObject == null) targetDirection = Vector3.zero;
        else targetDirection = targetObject.transform.position - gameObject.transform.position;
        controller.OnAIHorizontal(Mathf.Sign(targetDirection.x));
    }
}
