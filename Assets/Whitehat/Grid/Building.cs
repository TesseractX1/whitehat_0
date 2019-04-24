namespace Whitehat.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Player;
    using Whitehat.UnitMech;

    public class Building : Unit
    {
        public Hexagon hex;
        public PlayerScript player;

        [SerializeField] protected int initialRange;
        [SerializeField] protected int cost;
        public int Cost { get { return cost; } protected set { } }
        [SerializeField] protected int maintenance;
        public int Maintenance { get { return maintenance; } protected set { } }
        public int productivity=0;

        public static float distance = 2.5f;

        protected List<Hexagon> lightenedUp = new List<Hexagon>();

        // Use this for initialization
        void Start()
        {
            LightUp(initialRange);

            if (kind == "RAM" || kind == "core")
            {
                player.MaxRAM += Mathf.Abs(maintenance);
            }
            player.RAM -= maintenance;
            player.Productivity += productivity;
        }

        // Update is called once per frame
        protected void Update()
        {
            base.Update();
        }

        public void LightUp(int range)
        {
            foreach (RaycastHit hit in Physics.SphereCastAll(transform.position, distance * (range+1), Vector3.one))
            {
                if (Vector3.Distance(hit.collider.transform.position, transform.position) >= distance * (range+1))
                {
                    continue;
                }
                if (hit.collider.gameObject.GetComponent<Hexagon>())
                {
                    lightenedUp.Add(hit.collider.gameObject.GetComponent<Hexagon>());
                    hit.collider.gameObject.GetComponent<Hexagon>().AddLightingBuilding(this);
                    hit.collider.gameObject.GetComponent<Hexagon>().Visible = true;
                }
            }
        }

        private void ClearLightUp()
        {
            foreach(Hexagon hex in lightenedUp)
            {
                hex.RemoveLightingBuilding(this);
            }
            lightenedUp.Clear();
        }

        public void OnDestroy()
        {
            ClearLightUp();
            if (!player)
            { return; }
                player.RAM += maintenance;
            

            if (kind == "RAM")
            {
                player.MaxRAM += maintenance;
            }

            player.Productivity -= productivity;
        }
    }
}