namespace Whitehat.Active {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Grid;

    public class Bot : ActiveUnit {
        public Building enemyCore;
        [SerializeField] protected Vector3 hitPoint;

        public float sensorRange=30;

        // Update is called once per frame
        protected void Update() {
            base.Update();

            if (!target||target==enemyCore)
            {
                target=UpdateTargetOnGrid(sensorRange);
                if (!target)
                {
                    target = enemyCore;
                }
            }
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (CanAttack(collision.collider))
            {
                collision.collider.GetComponent<Unit>().Damage(damage);
                if (Random.value <= 0.1f)
                {
                    GenerateParticles(transform.TransformPoint(hitPoint));
                }
            }
        }
    }

}