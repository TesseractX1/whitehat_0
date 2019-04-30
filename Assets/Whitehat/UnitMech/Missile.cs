namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Missile : ActiveUnit
    {
        [SerializeField] private float lifeTime;

        private void Update()
        {
            if (lifeTime <= 0)
            {
                GetComponent<Explosive>().Hit();
            }
            else { lifeTime -= Time.deltaTime; }

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
    }
}