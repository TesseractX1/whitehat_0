namespace Whitehat.Grid
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.UnitMech;
    using Whitehat.Mechanics;

    public class Mortar : Turret
    {
        [SerializeField] private float frequency;
        [SerializeField] private float shellRecoveryRate;
        [SerializeField] private int shellRecovery;
        [SerializeField] private int maxShell;
        [SerializeField] private float deflection = 1;
        private float stopWatch;
        private float recoveryStopWatch;
        private float turnCount;

        [SerializeField] private float bombardmentRadius;
        private List<Transform> targetList;

        // Use this for initialization
        void Start()
        {
            targetList = new List<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            foreach(RaycastHit hit in Physics.SphereCastAll(transform.position, sensorRange, Vector3.one, Mathf.Infinity, UnitTargetSensor.mortarMarkLayer))
            {
                if (hit.collider.GetComponent<MortarMark>()&&!targetList.Contains(hit.collider.transform))
                {
                    targetList.Add(hit.collider.transform);
                }
            }

            if (stopWatch <= 0 && shellRecovery > 0 && targetList.Count > 0)
            {
                int randomIndex = Mathf.FloorToInt(Random.value * targetList.Count);
                MortarFire(targetList[randomIndex].position);
                Transform instance = targetList[randomIndex];
                targetList.Remove(instance);

                stopWatch = frequency + Random.value * frequency * 0.1f;
            }
            else { stopWatch -= Time.deltaTime; }

            if (recoveryStopWatch <= 0)
            {
                shellRecovery++;
                recoveryStopWatch = shellRecoveryRate;
            }
            else { recoveryStopWatch -= Time.deltaTime; }
            shellRecovery = Mathf.Clamp(shellRecovery, 0, maxShell);

            targetList.Clear();
        }

        private void MortarFire(Vector3 position)
        {
            position.x += (Random.value - 0.5f) * deflection * 2;
            position.y += (Random.value - 0.5f) * deflection * 2;
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(position, bombardmentRadius, Vector2.one, Mathf.Infinity, UnitTargetSensor.unitLayer))
            {
                if (GetComponent<UnitTargetSensor>().CanAttack(hit))
                {
                    hit.collider.GetComponent<Unit>().Damage(damage);
                }
            }
            GetComponent<Unit>().GenerateParticles(position);
            shellRecovery--;
        }
    }

}