namespace Whitehat.Active
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

        // Update is called once per frame
        protected void Update()
        {
            base.Update();

            if (!target)
            {
                return;
            }

            Mathf.SmoothDamp(0, Vector3.Distance(transform.position, target.transform.position), ref currentVelocity, 1 / speedFactor*10);
            torque = CalculateTorque(transform, target.transform, 1 / rotateFactor);

            transform.Translate(Vector3.up * (currentVelocity+Random.value*0.1f*currentVelocity) * Time.deltaTime);
            transform.Rotate(Vector3.forward * torque * Time.deltaTime);
        }
    }
}