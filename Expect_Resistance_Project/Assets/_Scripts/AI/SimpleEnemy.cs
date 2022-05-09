using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noGame.Characters;
using noGame.EnemyBehaviour;

[RequireComponent(typeof(AICharacterController2D))]
public class SimpleEnemy : MonoBehaviour
{
    [Header("Input Generator Options")]
    [SerializeField][Tooltip("Distance at which enemy will no longer approach target in horizontal axis")] float stopMovementDistance;
    [SerializeField] float horizontalInputBoost;

    [Header("Detection System")]
    [SerializeField] float killRadius;
    [SerializeField] float detectionRadius;

    
    internal GameObject TargetObject { get => targetObject; set => targetObject = value; }
    internal AICharacterController2D Controller { get => controller; }
    internal float StopMovementDistance { get => stopMovementDistance; }
    public float HorizontalInputBoost { get => horizontalInputBoost; }
    public float KillRadius { get => killRadius; set => killRadius = value; }

    EnemyState state;
    PatrollingState patrollingState;
    ChaseState chaseState;
    AICharacterController2D controller;
    GameObject targetObject;
    Collider2D[] detectedObjects = new Collider2D[10];

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
        Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, killRadius, detectedObjects);
        for(int i = 0; i < detectedObjects.Length; i++)
        {
            if (detectedObjects[i] == null) break;
            if(detectedObjects[i].gameObject.CompareTag("Player")) // TO DO: use layers?
            {
                Debug.Log("Player killed");
                detectedObjects[i].gameObject.GetComponent<PlayerCharacterController2D>().enabled = false;
            }
        }
        state.Update();

    }

    internal void ChangeState(EnemyState state)
    {
        this.state = state;
        state.Start();
    }
}
