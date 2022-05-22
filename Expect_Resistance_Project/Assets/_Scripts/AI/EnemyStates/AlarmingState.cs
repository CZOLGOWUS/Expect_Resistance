using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    internal class AlarmingState : ChaseState
    {
        AlarmingEnemy ctx;
        public AlarmingState(AlarmingEnemy ctx) : base(ctx)
        {
            this.ctx = ctx;
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

            ctx.ChangeSpeed(ctx.RunSpeed);
        }

        internal override void Update()
        {
            base.Update();
            if(targetDirection.magnitude < ctx.AlarmInteractionRadius)
            {
                // TO DO: Interact with button
                ctx.Alarm.AlarmAll();
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