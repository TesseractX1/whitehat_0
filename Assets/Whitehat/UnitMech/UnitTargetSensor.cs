namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Unit))]

    public class UnitTargetSensor : MonoBehaviour
    {
        public static int gridLayer = 513;
        public static int unitLayer = 1025;
        public static int mortarMarkLayer = 2049;

        public bool targetSameFaction = false;
        public bool targetIneffective;
        public bool ignoresCanBeTarget = false;

        private Unit unit;

        private void Start()
        {
            unit = GetComponent<Unit>();
        }

        private void Update()
        {
            if (GetComponent<Bot>()&& GetComponent<Bot>().target)
            {
                if (unit.kind != "fixBot")
                {
                    targetIneffective = GetComponent<Bot>().target == GetComponent<Bot>().enemyCore;
                }
                else
                {
                    targetIneffective = GetComponent<Bot>().target.health >= GetComponent<Bot>().target.maxHealth;
                }
            }
        }

        public bool CanAttack(RaycastHit2D hit)
        {
            return hit.collider && CanAttack(hit.collider);
        }

        public bool CanAttack(Collider2D hit)
        {
            if (!hit.GetComponent<Unit>())
            {
                return false;
            }
            bool factionMatch = hit.GetComponent<Unit>().faction != GetComponent<Unit>().faction;
            if (targetSameFaction)
            {
                factionMatch = !factionMatch;
            }
            return hit.GetComponent<Unit>() && factionMatch;
        }

        public Unit UpdateTarget(float range, int skipTarget = 0, float randomFactor = 0)
        {
            int turn = skipTarget;
            Unit target = null;
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, unitLayer))
            {
                if (CanAttack(hit) && (hit.collider.GetComponent<Unit>().canBeTarget&&!ignoresCanBeTarget))
                {
                    target = hit.collider.GetComponent<Unit>();
                    if (turn > 0 && randomFactor > Random.value)
                    {
                        turn--;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return target;
        }

        public Unit UpdateTargetOnGrid(float range, int skipTarget = 0, float randomFactor = 0)
        {
            int turn = skipTarget;
            Unit target = null;
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, gridLayer))
            {
                bool canAttack = ignoresCanBeTarget? CanAttack(hit):(CanAttack(hit) && hit.collider.GetComponent<Unit>().canBeTarget);
                if (canAttack)
                {
                    target = hit.collider.GetComponent<Unit>();
                    if (turn > 0 && randomFactor > Random.value)
                    {
                        turn--;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return target;
        }
    }
}