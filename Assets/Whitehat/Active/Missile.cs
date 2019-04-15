namespace Whitehat.Active
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Missile : ActiveUnit
    {
        [SerializeField] protected float range;

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
            {
                collision.collider.GetComponent<Unit>().Damage(damage/Time.deltaTime);
                Hit();
            }
        }

        private void Hit()
        {
            int layerMask = 0;
            if (faction == UnitFaction.faction1)
            {
                layerMask = Unit.faction2Layer;
            }
            else
            {
                layerMask = Unit.faction1Layer;
            }

            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, layerMask))
            {
                if (CanAttack(hit))
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
    }
}