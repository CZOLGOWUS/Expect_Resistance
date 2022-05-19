using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.EnemyBehaviour
{
    internal abstract class EnemyState
    {
        protected SimpleEnemy ctx;

        internal EnemyState(SimpleEnemy ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// This is not MonoBehaviout function!
        /// </summary>
        internal abstract void Start();

        /// <summary>
        /// This is not MonoBehaviout function!
        /// </summary>
        internal abstract void Update();
    }
}

