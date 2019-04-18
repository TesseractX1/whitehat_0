namespace Whitehat.UnitMech
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Grid;

    public class ActiveUnit : Unit
    {
        public Unit target;

        public float speedFactor;
        public float rotateFactor=10;
        public float damage;

        protected float currentVelocity;
        protected float torque;

        protected void Start()
        {
            GameObject.FindWithTag("ActiveUnitManager").GetComponent<ActiveUnitManager>().unitCount++;
        }

        // Update is called once per frame
        protected void Update()
        {
            base.Update();

            if (!target)
            {
                return;
            }

            torque = CalculateTorque(transform, target.transform, 1 / rotateFactor);
            transform.Rotate(Vector3.forward * torque * Time.deltaTime);

            Move();
        }

        protected virtual void Move()
        {
            Mathf.SmoothDamp(0, Vector3.Distance(transform.position, target.transform.position), ref currentVelocity, 1 / speedFactor * 10);
            transform.Translate(Vector3.up * (currentVelocity + Random.value * 0.1f * currentVelocity) * Time.deltaTime);
        }

        protected void OnDestroy()
        {
            GameObject.FindWithTag("ActiveUnitManager").GetComponent<ActiveUnitManager>().unitCount--;
        }
    }
}