namespace Whitehat.Grid
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Turret : Building
    {
        public Unit target;
        public Unit unitHit;
        private float torque;

        public float attackRange;
        public float sensorRange;
        public float damage;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            base.Update();

            if (!target)
            {
                target=UpdateTarget(sensorRange);
            }
            else
            {
                torque = CalculateTorque(transform, target.transform, 0.2f);
                transform.Rotate(Vector3.forward * torque * Time.deltaTime);
            }
        }

    }

}