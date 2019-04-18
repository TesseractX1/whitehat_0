namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Unit : MonoBehaviour
    {
        [SerializeField] protected GameObject particlePrefab;
        public string kind;

        public bool canBeTarget=true;
        public UnitFaction faction;
        public float health;
        public float maxHealth;

        protected void Update()
        {
            health = Mathf.Clamp(health, 0, maxHealth);
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

        public void GenerateParticles(Vector3 position)
        {
            if (!particlePrefab)
            {
                return;
            }
            GameObject.Instantiate(particlePrefab, position, transform.rotation);
        }

    }

    public enum UnitFaction
    {
        faction1, faction2
    }
}