namespace Whitehat
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        [SerializeField] protected GameObject particlePrefab;

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
            /*if (positionX > 0)
            {
                targetPointer = 270 + targetPointer;
            }
            else if (positionX < 0 && positionY < 0)
            {
                targetPointer = 180 + targetPointer;
            }
            else if (positionX < 0 && positionY > 0)
            {
                targetPointer = 360 + targetPointer;
            }*/

            float torque = 0;
            Mathf.SmoothDampAngle(self.eulerAngles.z+adjustment, targetPointer, ref torque, speedFactor);
            return torque;
        }

        protected bool CanAttack(RaycastHit2D hit)
        {
            return hit.collider&&CanAttack(hit.collider);
        }

        protected bool CanAttack(Collider2D hit)
        {
            return hit.GetComponent<Unit>() && hit.GetComponent<Unit>().faction != faction;
        }

        protected void GenerateParticles(Vector3 position)
        {
            GameObject.Instantiate(particlePrefab, position, transform.rotation);
        }

        protected Unit UpdateTarget(float range, int skipTarget = 0, float randomFactor = 0)
        {
            int turn = skipTarget;
            Unit target=null;
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, 1025))
            {
                if (CanAttack(hit))
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

        protected Unit UpdateTargetOnGrid(float range, int skipTarget = 0, float randomFactor = 0)
        {
            int turn = skipTarget;
            Unit target = null;
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(transform.position, range, Vector2.one, Mathf.Infinity, 513))
            {
                if (CanAttack(hit))
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