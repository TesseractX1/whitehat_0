namespace Whitehat.Grid
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.UnitMech;

    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(UnitTargetSensor))]

    public class Turret : MonoBehaviour
    {
        public Unit target;
        public Unit unitHit;
        private float torque;

        public float sensorRange;
        public float damage;

        // Update is called once per frame
        protected void Update()
        {
            if (target&&!target.gameObject.activeSelf)
            {
                target = null;
            }

            if (GetComponent<ActiveUnit>()) {
                target = GetComponent<ActiveUnit>().target;
            }
            else if (!target)
            {
                target = GetComponent<UnitTargetSensor>().UpdateTarget(sensorRange);
            }
            else
            {
                torque = Unit.CalculateTorque(transform, target.transform, 0.2f);
                transform.Rotate(Vector3.forward * torque * Time.deltaTime);
            }
        }

    }

}