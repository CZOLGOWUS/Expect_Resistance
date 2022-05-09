using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    internal class ChaseState : EnemyState
    {
        Vector3 targetDirection;
        float horizontalInput;
        public ChaseState(SimpleEnemy ctx) : base(ctx)
        {
        }

        internal override void Start()
        {
            
        }

        internal override void Update()
        {
            if (ctx.TargetObject == null) targetDirection = Vector3.zero;
            else targetDirection = ctx.TargetObject.transform.position - ctx.gameObject.transform.position;

            horizontalInput = Mathf.Clamp(targetDirection.x*ctx.HorizontalInputBoost, -1, 1);
            if(Mathf.Abs(horizontalInput) < ctx.StopMovementDistance)
            {
                horizontalInput = 0;
            }

            ctx.Controller.OnAIHorizontal(horizontalInput);
        }
    }
}
