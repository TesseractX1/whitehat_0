namespace Whitehat.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Active;

    public class MissileTurret : Turret
    {
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private Vector3[] launchPoints;

        [SerializeField] private float frequency;
        [SerializeField] private int missilePerLaunch;
        private float stopWatch;
        private float turnCount;

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
                stopWatch = frequency;
            }
            else { stopWatch -= Time.deltaTime; }
        }

        private void LaunchMissile(Unit target, int launchPointOrder)
        {
            Missile newMissile=GameObject.Instantiate(missilePrefab, transform.TransformPoint(launchPoints[launchPointOrder]), transform.rotation).GetComponent<Missile>();
            newMissile.target = target;
            target = UpdateTarget(sensorRange, missilePerLaunch * 3, 0.7f);
        }
    }
}