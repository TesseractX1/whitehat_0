﻿namespace Whitehat
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        [SerializeField] protected GameObject particlePrefab;
        public static int gridLayer = 513;
        public static int unitLayer = 1025;

        public bool canBeTarget=true;

        public UnitFaction faction;
        public float health;

        protected void Update()
        {
            if (health <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
       
        public void Damage(float damage)
        {
            health -= damage * Time.deltaTime;
        }

        public static float CalculateTorque(Transform self, Transform target, float speedFactor, float adjustment=0)
        {
            float targetPointer = 0;
            float positionX = (target.position.x - self.position.x);
            float positionY = (target.position.y - self.position.y);
            targetPointer = Mathf.Atan(positionX / positionY) * Mathf.Rad2Deg;

            targetPointer = -targetPointer;
            if (positionY < 0)
            {
                targetPointer += 180;
            }
            float torque = 0;
            Mathf.SmoothDampAngle(self.eulerAngles.z+adjustment, targetPointer, ref torque, speedFactor);
            return torque;
        }

        public bool CanAttack(RaycastHit2D hit)
        {
            return hit.collider&&CanAttack(hit.collider);
        }

        public bool CanAttack(Collider2D hit)
        {
            return hit.GetComponent<Unit>() && hit.GetComponent<Unit>().faction != faction;
        }

        public void GenerateParticles(Vector3 position)
        {
            GameObject.Instantiate(particlePrefab, position, transform.rotation);
        }

        public Unit UpdateTarget(float range, int skipTarget = 0, float randomFactor = 0)
        {
            int turn = skipTarget;
            Unit target=null;
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, unitLayer))
            {
                if (CanAttack(hit) && hit.collider.GetComponent<Unit>().canBeTarget)
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
                if (CanAttack(hit) && hit.collider.GetComponent<Unit>().canBeTarget)
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

    public enum UnitFaction
    {
        faction1, faction2
    }
}