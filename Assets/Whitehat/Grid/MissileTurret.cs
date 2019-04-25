namespace Whitehat.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.UnitMech;
    using Whitehat.ObjectPools;

    [RequireComponent(typeof(Unit))]

    public class MissileTurret : Turret
    {
        private ObjectPool missilePool;
        [SerializeField] private string poolName;

        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private Vector3[] launchPoints;

        [SerializeField] private float frequency;
        [SerializeField] private int missilePerLaunch;
        private float stopWatch;
        private float turnCount;

        private void Start()
        {
            missilePool = GameObject.Find(poolName).GetComponent<ObjectPool>();
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            if (stopWatch <= 0 && target)
            {
                for (int j = 0; j < launchPoints.Length; j++)
                {
                    for (int i = 0; i < missilePerLaunch; i++)
                    {
                        LaunchMissile(target, j);
                    }
                }
                stopWatch = frequency+Random.value*frequency*0.1f;
            }
            else { stopWatch -= Time.deltaTime; }
        }

        private void LaunchMissile(Unit target, int launchPointOrder)
        {
            Missile newMissile=missilePool.UseAndInit(transform.TransformPoint(launchPoints[launchPointOrder]), transform.eulerAngles).GetComponent<Missile>();
            newMissile.target = target;
            target = GetComponent<UnitTargetSensor>().UpdateTarget(sensorRange, missilePerLaunch * 3, 0.7f);
        }
    }
}