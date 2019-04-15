namespace Whitehat.Grid
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Active;

    [RequireComponent(typeof(Unit))]

    public class Turret : MonoBehaviour
    {
        public Unit target;
        public Unit unitHit;
        private float torque;

        public float sensorRange;
        public float damage;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            if (GetComponent<ActiveUnit>()) {
                target = GetComponent<ActiveUnit>().target;
            }
            else if (!target)
            {
                target = GetComponent<Unit>().UpdateTarget(sensorRange);
            }
            else
            {
                torque = Unit.CalculateTorque(transform, target.transform, 0.2f);
                transform.Rotate(Vector3.forward * torque * Time.deltaTime);
            }
        }

    }

}