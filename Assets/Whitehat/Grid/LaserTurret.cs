namespace Whitehat.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LaserTurret : Turret
    {
        [SerializeField] private LineRenderer beamLine;
        private RaycastHit2D beamHit;
        private Vector2 upDirection;

        // Update is called once per frame
        void Update()
        {
            base.Update();

            upDirection = new Vector2(transform.up.x, transform.up.y);
            beamHit = Physics2D.Raycast(transform.position, upDirection, attackRange, 1025);
            beamLine.SetPosition(1, Vector3.up * beamHit.distance);

            unitHit = CanAttack(beamHit) ? beamHit.collider.GetComponent<Unit>() : null;
            if (unitHit)
            {
                unitHit.Damage(damage);
                if (Random.value < 0.1f)
                {
                    GenerateParticles(beamHit.point);
                }
            }
        }
    }
}