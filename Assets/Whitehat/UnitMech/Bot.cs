namespace Whitehat.UnitMech {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Grid;
    using Whitehat.Mechanics;

    [RequireComponent(typeof(UnitTargetSensor))]

    public class Bot : ActiveUnit {
        [SerializeField] private AttackWaveManager waveManager;

        public Building enemyCore;
        [SerializeField] protected Vector3 hitPoint;
        [SerializeField] private bool destroyOnInterval;

        public float sensorRange=30;

        public void Start()
        {
            base.Start();
            waveManager = GameObject.FindGameObjectWithTag("ActiveUnitManager").GetComponent<AttackWaveManager>();
        }

        // Update is called once per frame
        protected void Update() {
            base.Update();

            if (!target||GetComponent<UnitTargetSensor>().targetIneffective)
            {
                target=GetComponent<UnitTargetSensor>().UpdateTargetOnGrid(sensorRange);
                if (!target)
                {
                    target = enemyCore;
                }
            }

            if (!waveManager.onWave&&destroyOnInterval) { health = 0; }
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (GetComponent<UnitTargetSensor>().CanAttack(collision.collider))
            {
                collision.collider.GetComponent<Unit>().Damage(damage);
                if (Random.value <= 0.1f)
                {
                    GenerateParticles(transform.TransformPoint(hitPoint));
                }
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

}