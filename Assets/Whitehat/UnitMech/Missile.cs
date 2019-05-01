namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Missile : ActiveUnit
    {
        [SerializeField] private float lifeTime;
         private float lifeTimeCount;

        public override void Start()
        {
            base.Start();
            lifeTimeCount = lifeTime;
            GetComponent<TrailRenderer>().Clear();
        }

        private void Update()
        {
            if (lifeTimeCount <= 0)
            {
                GetComponent<Explosive>().Hit();
            }
            else { lifeTimeCount -= Time.deltaTime; }

            if (!target)
            {
                transform.Translate(Vector3.up * (speedFactor + Random.value * 0.1f * currentVelocity) * Time.deltaTime);
            }
            base.Update();
        }

        protected override void Move()
        {
            transform.Translate(Vector3.up * (speedFactor + Random.value * 0.1f * speedFactor) * Time.deltaTime);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}