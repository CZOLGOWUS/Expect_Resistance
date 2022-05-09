using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noGame.Characters;
using noGame.EnemyBehaviour;

[RequireComponent(typeof(AICharacterController2D))]
public class SimpleEnemy : MonoBehaviour
{
    [Header("Input Generation Options")]
    [SerializeField][Tooltip("Distance below which enemy will no longer approach target, edge or obstacle in horizontal axis")] float stopMovementDistance;
    [SerializeField] float horizontalInputBoost;
    [SerializeField][Range(-1,1)] float initailPatrollDirection;
    [SerializeField][Tooltip("Break time before changing patrolling direction")] float breakTime;

    [Header("Detection System")]
    [SerializeField] float killRadius;
    [SerializeField] float detectionRadius;
    //[SerializeField][Tooltip("Determines what height counts as edge")]

    float horizontalInput;

    EnemyState state;
    PatrollingState patrollingState;
    ChaseState chaseState;
    IdleState idleState;

    AICharacterController2D controller;
    GameObject targetObject;
    Collider2D[] detectedObjects = new Collider2D[10];

    public float HorizontalInputBoost { get => horizontalInputBoost; }
    public float KillRadius { get => killRadius; set => killRadius = value; }
    internal GameObject TargetObject { get => targetObject; set => targetObject = value; }
    internal float StopMovementDistance { get => stopMovementDistance; }
    internal float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }

    void Awake()
    {
        controller = GetComponent<AICharacterController2D>();
        chaseState = new ChaseState(this);
        patrollingState = new PatrollingState(this,initailPatrollDirection,breakTime);
        idleState = new IdleState(this);
    }

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        ChangeState(patrollingState);
    }

    void Update()
    {
        state.Update();
        controller.OnAIHorizontal(horizontalInput);
    }

    internal void KillCheck()
    {
        Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, killRadius, detectedObjects);
        for (int i = 0; i < detectedObjects.Length; i++)
        {
            if (detectedObjects[i] == null) break;
            if (detectedObjects[i].gameObject.CompareTag("Player")) // TO DO: use layers?
            {
                detectedObjects[i].gameObject.GetComponent<PlayerCharacterController2D>().enabled = false;
                ChangeState(idleState);
            }
        }
    }

    internal void ChangeState(EnemyState state)
    {
        this.state = state;
        state.Start();
    }
}
