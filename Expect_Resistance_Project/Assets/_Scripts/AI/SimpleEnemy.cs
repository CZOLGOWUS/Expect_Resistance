using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noGame.Characters;
using noGame.EnemyBehaviour;

[RequireComponent(typeof(AICharacterController2D))]
public class SimpleEnemy : MonoBehaviour
{
    EnemyState state;
    PatrollingState patrollingState;
    ChaseState chaseState;

    private AICharacterController2D controller;
    private GameObject targetObject;
    internal GameObject TargetObject { get => targetObject; set => targetObject = value; }
    internal AICharacterController2D Controller { get => controller; }

    void Awake()
    {
        controller = GetComponent<AICharacterController2D>();
        chaseState = new ChaseState(this);
        patrollingState = new PatrollingState(this);
    }

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        //state = patrollingState;
        state = chaseState;
    }

    void Update()
    {
        state.Update();
    }

    internal void ChangeState(EnemyState state)
    {
        this.state = state;
        state.Start();
    }
}
