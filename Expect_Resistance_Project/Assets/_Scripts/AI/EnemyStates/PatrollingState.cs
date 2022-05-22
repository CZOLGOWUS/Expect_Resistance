using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace noGame.EnemyBehaviour
{
    internal class PatrollingState : EnemyState
    {
        float patrollDirection;
        float breakTimer;
        float detectionLevel;
        bool isBreak;
        bool isSeeingPlayer;

        Vector2 detectionSource;
        Vector3 targetDirection;

        public PatrollingState(SimpleEnemy ctx, float initialPatrollDirection) : base(ctx)
        {
            patrollDirection = Mathf.Clamp(initialPatrollDirection, -1, 1);
        }

        internal override void Start()
        {
            ctx.HorizontalInput = patrollDirection;
            isBreak = false;
            breakTimer = 0;
            ctx.ChangeSpeed(ctx.PatrollingSpeed);
        }

        internal override void Update()
        {
            HandleBreak();
            if(!isBreak)
            {
                HandleEdge();
                HandleObstacle();
            }

            HandlePlayerDetection();
            HandleDetectionLevel();
        }
        void HandleBreak()
        {
            if (breakTimer > 0) breakTimer -= Time.deltaTime;
            else if (isBreak)
            {
                isBreak = false;
                patrollDirection = -patrollDirection;
                ctx.HorizontalInput = patrollDirection;
            }
            ctx.KillCheck();
        }

        private void HandleEdge()
        {
            detectionSource = new Vector3(
                ctx.gameObject.transform.position.x + (ctx.GetComponent<BoxCollider2D>().size.x / 2 + ctx.StopMovementDistance) * patrollDirection,
                ctx.GetComponent<BoxCollider2D>().bounds.min.y + 0.001f,
                0
            );

            RaycastHit2D hit = Physics2D.Raycast(detectionSource, Vector2.down, 5);
            if (!hit || hit.distance > 0.002f)
            {
                StartWaiting();
            }
        }

        private void HandleObstacle()
        {
            detectionSource = new Vector3(
                ctx.gameObject.transform.position.x + (ctx.GetComponent<BoxCollider2D>().size.x / 2 +0.001f) * patrollDirection,
                ctx.GetComponent<BoxCollider2D>().bounds.min.y + 0.001f,
                0
            );

            RaycastHit2D hit = Physics2D.Raycast(detectionSource, Vector2.right * patrollDirection, ctx.StopMovementDistance);
            if (hit && !hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Enemy")) // TODO: replace with layers?
            {
                StartWaiting();
            }
        }

        void StartWaiting()
        {
            isBreak = true;
            breakTimer = ctx.BreakTime;
            ctx.HorizontalInput = 0;
        }

        private void HandlePlayerDetection()
        {
            targetDirection = ctx.PlayerObject.transform.position - ctx.gameObject.transform.position;
            if (Mathf.Abs(Mathf.Atan2(Mathf.Abs(targetDirection.y), Mathf.Abs(targetDirection.x))) < Mathf.Deg2Rad*45  &&
                Mathf.Sign(targetDirection.x) == Mathf.Sign(patrollDirection))    
            {
                Debug.DrawLine(ctx.gameObject.transform.position, ctx.gameObject.transform.position + targetDirection, Color.grey);
                RaycastHit2D hit = Physics2D.Raycast(ctx.gameObject.transform.position, targetDirection, ctx.DetectionRadius, ctx.DetectionMask);

                if (hit && hit.collider.CompareTag("Player"))
                {
                    Debug.DrawLine(ctx.gameObject.transform.position, ctx.gameObject.transform.position + targetDirection, Color.red);
                    isSeeingPlayer = true;
                }
                else
                {
                    isSeeingPlayer = false;
                }
            }
            else
            {
                isSeeingPlayer = false;
            }
        }

        private void HandleDetectionLevel()
        {
            if (isSeeingPlayer)
            {
                detectionLevel += Time.deltaTime;
                if (detectionLevel > ctx.DetectionLevelThreashold)
                {
                    ctx.OnPlayerDetected();
                }
            }
            else if (detectionLevel > 0)
            {
                detectionLevel -= Time.deltaTime * ctx.DetectionLevelDecreaseFactor;
                if (detectionLevel < 0)
                    detectionLevel = 0;
            }
        }
    }
}
