using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    public class AlarmingEnemy : SimpleEnemy
    {
        internal AlarmingState alarmingState;

        AlarmingEnemy() : base()
        {
            alarmingState = new AlarmingState(this);
        }

        override internal void OnPlayerDetected()
        {
            ChangeState(alarmingState);
            Instantiate(exclamationMarkPrefab, gameObject.transform.position + (Vector3)exclamationMarkPosition, Quaternion.identity, gameObject.transform);
        }
    }
}