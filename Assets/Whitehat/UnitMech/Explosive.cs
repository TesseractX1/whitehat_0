namespace Whitehat.UnitMech
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.ObjectPools;

    public class Explosive : MonoBehaviour
    {
        private GameObject particlePrefab;
        [SerializeField] private Unit unit;
        [SerializeField] protected float range;

        public float damage;
        [SerializeField] private bool splashHit;
        [SerializeField] private bool hitGridLayer;

        private void Start()
        {
            particlePrefab = unit.ParticlePrefab;
            if (unit.GetComponent<ActiveUnit>())
            {
                damage = unit.GetComponent<ActiveUnit>().damage;
            }
        }

        private void Update()
        {
            if (unit.health <= unit.maxHealth * 0.1f) { Hit(); }
        }

        protected void OnCollisionStay2D(Collision2D collision)
        {
            if (unit.GetComponent<UnitTargetSensor>().CanAttack(collision.collider))
            {
                Hit(collision.collider.GetComponent<Unit>());
            }
        }

        public void Hit(Unit hitUnit=null)
        {
            if (splashHit)
            {
                int layerMask = hitGridLayer ? UnitTargetSensor.gridLayer : UnitTargetSensor.unitLayer;

                foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, layerMask))
                {
                    if (unit.GetComponent<UnitTargetSensor>().CanAttack(hit))
                    {
                        hit.collider.GetComponent<Unit>().Damage(damage * (1 - Vector3.Distance(transform.position, hit.collider.transform.position) * 0.8f / (range)));
                    }
                }
            }
            if (hitUnit)
            {
                hitUnit.Damage(damage / Time.deltaTime);
            }

            if (Random.value <= 0.3f)
            {
                GenerateParticles(transform.position);
            }

            unit.health = 0;
        }

        public void GenerateParticles(Vector3 position)
        {
            if (!particlePrefab)
            {
                return;
            }
            GameObject.Instantiate(particlePrefab, position, transform.rotation);
        }
    }

}