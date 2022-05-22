using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noGame.Characters;
using noGame.EnemyBehaviour;


namespace noGame.EnemyBehaviour
{
    [RequireComponent(typeof(AICharacterController2D))]
    public class SimpleEnemy : MonoBehaviour
    {
        [Header("Input Generation Options")]
        [SerializeField] float patrollingSpeed;
        [SerializeField] float runSpeed;
        [SerializeField][Tooltip("Distance below which enemy will no longer approach edge or obstacle in horizontal axis")] float stopMovementDistance;
        [SerializeField] float horizontalInputBoost;
        [SerializeField][Range(-1, 1)] float initailPatrollDirection;
        [SerializeField][Tooltip("Break time before changing patrolling direction")] float breakTime;
        [SerializeField][Tooltip("Break time before changing patrolling direction")] float minJumpDelay;

        [Header("Detection System")]
        [SerializeField] float killRadius;
        [SerializeField] float detectionRadius;
        [SerializeField][Range(0, 90)][Tooltip("Above this angle (both up and down) enemy will not see player")] float detectionMaxAngle;
        [SerializeField][Tooltip("Time in seconds in which enemy will react after continously seeing player (increasing detection level)")] float detectionLevelThreashold;
        [SerializeField][Range(0, 1)][Tooltip("Rate at which enemy will loose detection level when not seeing player")] float detectionLevelDecreaseFactor;
        // note: Detection level is defined in PatrollingState since it's not used elsewhere. Consider moving it to this class if more states would like to detect player.
        [SerializeField] LayerMask visableToEnemy;

        [Header("Other")]
        [SerializeField] protected GameObject exclamationMarkPrefab;
        [SerializeField] protected Vector2 exclamationMarkPosition;


        float horizontalInput;

        internal EnemyState state;
        internal PatrollingState patrollingState;
        internal ChaseState chaseState;
        internal IdleState idleState;

        AICharacterController2D AIController;
        CharacterController2D characterController;
        GameObject targetObject;
        GameObject playerObject;
        Collider2D[] detectedObjects = new Collider2D[10];
        AlarmSystem alarm;

        public float HorizontalInputBoost { get => horizontalInputBoost; }
        public float KillRadius { get => killRadius; set => killRadius = value; }
        public float DetectionRadius { get => detectionRadius; }
        internal GameObject TargetObject { get => targetObject; set => targetObject = value; }
        internal float StopMovementDistance { get => stopMovementDistance; }
        internal float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
        public float DetectionLevelThreashold { get => detectionLevelThreashold; }
        public float DetectionLevelDecreaseFactor { get => detectionLevelDecreaseFactor; }
        public LayerMask DetectionMask { get => visableToEnemy; }
        public float MinJumpDelay { get => minJumpDelay; set => minJumpDelay = value; }
        public float BreakTime { get => breakTime; set => breakTime = value; }
        public GameObject PlayerObject { get => playerObject; }
        internal AlarmSystem Alarm { get => alarm; }
        public float RunSpeed { get => runSpeed; }
        public float PatrollingSpeed { get => patrollingSpeed;  }

        void Awake()
        {
            AIController = gameObject.GetComponent<AICharacterController2D>();
            characterController = gameObject.GetComponent<CharacterController2D>();
            chaseState = new ChaseState(this);
            patrollingState = new PatrollingState(this, initailPatrollDirection);
            idleState = new IdleState(this);
            playerObject = GameObject.FindGameObjectWithTag("Player");
            alarm = GameObject.FindObjectOfType<AlarmSystem>();
            if (alarm != null)
                alarm.Subscribe(OnAlarm);
        }

        void Start()
        {
            ChangeState(patrollingState);
            //ChangeState(chaseState);

        }

        void Update()
        {
            state.Update();
            AIController.OnAIHorizontal(horizontalInput);
        }

        internal void KillCheck()
        {
            ClearDetectedObjects();
            Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, killRadius, detectedObjects);
            for (int i = 0; i < detectedObjects.Length; i++)
            {
                if (detectedObjects[i] == null) break;
                if (detectedObjects[i].gameObject.CompareTag("Player")) // TO DO: use layers?
                {
                    ChangeState(idleState);
                    ObjectiveController oc = FindObjectOfType<ObjectiveController>();
                    if (oc!=null)
                    {
                        oc.OnPlayerDeath();
                    }
                }
            }
        }

        internal void PhaseDownInput()
        {
            AIController.OnAIDownKey(true);
        }

        internal void JumpInput(bool active)
        {
            AIController.OnAIJump(active);
        }

        internal bool IsFalling()
        {
            return AIController.IsFalling;
        }

        internal bool IsGrounded()
        {
            return characterController.isGrounded;
        }

        internal virtual void OnPlayerDetected()
        {
            ChangeState(chaseState);
            Instantiate(exclamationMarkPrefab, gameObject.transform.position + (Vector3)exclamationMarkPosition, Quaternion.identity, gameObject.transform);
        }

        public void OnAlarm()
        {
            ChangeState(chaseState);
        }

        internal void ChangeState(EnemyState state)
        {
            this.state = state;
            state.Start();
        }

        internal void ChangeSpeed(float speed)
        {
            AIController.MoveSpeed = speed;
        }

        private void ClearDetectedObjects()
        {
            for (int i = 0; i < detectedObjects.Length; i++)
            {
                detectedObjects[i] = null;
            }
        }
    }
}