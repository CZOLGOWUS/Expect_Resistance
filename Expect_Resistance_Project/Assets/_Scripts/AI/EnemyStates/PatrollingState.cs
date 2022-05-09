using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace noGame.EnemyBehaviour
{
    internal class PatrollingState : EnemyState
    {
        float patrollDirection;
        float breakInitialTime;
        float breakTimer;
        bool isBreak;

        Vector2 detectionSource;

        public PatrollingState(SimpleEnemy ctx, float initialPatrollDirection, float breakInitialTime) : base(ctx)
        {
            patrollDirection = Mathf.Clamp(initialPatrollDirection, -1, 1);
            this.breakInitialTime = breakInitialTime;
        }

        internal override void Start()
        {
            ctx.HorizontalInput = patrollDirection;
            isBreak = false;
            breakTimer = 0;
        }

        internal override void Update()
        {
            HandleBreak();
            if(!isBreak)
            {
                HandleEdge();
                HandleObstacle();
            }
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
            if (hit && !hit.collider.CompareTag("Player")) // TODO: replace with layers?
            {
                StartWaiting();
            }
        }

        void StartWaiting()
        {
            isBreak = true;
            breakTimer = breakInitialTime;
            ctx.HorizontalInput = 0;
        }
    }
}
