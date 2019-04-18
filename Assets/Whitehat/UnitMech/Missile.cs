namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Missile : ActiveUnit
    {
        [SerializeField] protected float range;
        [SerializeField] private bool hitGridLayer;

        private void Update()
        {
            if (!target)
            {
                Hit();
            }
            base.Update();
        }

        protected void OnCollisionStay2D(Collision2D collision)
        {
            if (GetComponent<UnitTargetSensor>().CanAttack(collision.collider))
            {
                collision.collider.GetComponent<Unit>().Damage(damage/Time.deltaTime);
                Hit();
            }
        }

        private void Hit()
        {
            int layerMask = hitGridLayer ? UnitTargetSensor.gridLayer : UnitTargetSensor.unitLayer;

            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, layerMask))
            {
                if (GetComponent<UnitTargetSensor>().CanAttack(hit))
                {
                    hit.collider.GetComponent<Unit>().Damage(damage * (1-Vector3.Distance(transform.position,hit.collider.transform.position)*0.8f / (range)));
                }
            }
            if (Random.value <= 0.3f)
            {
                GenerateParticles(transform.position);
            }
            GameObject.Destroy(gameObject);
        }

        protected override void Move()
        {
            transform.Translate(Vector3.up * (speedFactor + Random.value * 0.1f * speedFactor) * Time.deltaTime);
        }
    }
}