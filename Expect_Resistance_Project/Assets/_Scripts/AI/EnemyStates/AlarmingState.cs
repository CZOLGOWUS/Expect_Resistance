using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    internal class AlarmingState : ChaseState
    {
        public AlarmingState(SimpleEnemy ctx) : base(ctx)
        {

        }

        internal override void Start()
        {
            base.Start();
            GameObject temp;
            if(FindNearestAlarmButton(out temp))
            {
                ctx.TargetObject = temp;
            }
            else
            {
                // if no alarms, default to chasing player
                ctx.ChangeState(ctx.chaseState);
            }
        }

        internal override void Update()
        {
            base.Update();
            if(targetDirection.magnitude < ctx.KillRadius) // may replace with dedicated radius
            {
                // TO DO: Interact with button
                ctx.ChangeState(ctx.chaseState);
            }
        }

        internal bool FindNearestAlarmButton(out GameObject nearestAlarmButton)
        {
            nearestAlarmButton = null;
            float smallestDistance = float.MaxValue;

            foreach (var alarmButton in ctx.TargetAlarmButtons)
            {
                float dis = (alarmButton.transform.position - ctx.gameObject.transform.position).magnitude;
                if (dis < smallestDistance)
                {
                    smallestDistance = dis;
                    nearestAlarmButton = alarmButton;
                }
            }
            return nearestAlarmButton != null;
        }
    }
}