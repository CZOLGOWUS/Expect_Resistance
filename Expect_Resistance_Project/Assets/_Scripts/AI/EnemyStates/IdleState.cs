using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    internal class IdleState : EnemyState
    {
        Vector3 targetDirection;
        public IdleState(SimpleEnemy ctx) : base(ctx)
        {
        }

        internal override void Start()
        {
            ctx.HorizontalInput = 0;
        }

        internal override void Update()
        {

        }
    }
}
