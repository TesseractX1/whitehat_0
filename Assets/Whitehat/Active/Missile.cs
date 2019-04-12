namespace Whitehat.Active
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Missile : ActiveUnit
    {
        [SerializeField] protected float range;
        [SerializeField] protected float maxDamage;

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
            if (CanAttack(collision.collider))
            { Hit(); }
        }

        private void Hit()
        {
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, 1025))
            {
                if (CanAttack(hit))
                {
                    hit.collider.GetComponent<Unit>().Damage(maxDamage * (1-Vector3.Distance(transform.position,hit.collider.transform.position)*0.8f / range));
                }
            }
            GenerateParticles(transform.position);
            GameObject.Destroy(gameObject);
        }
    }
}