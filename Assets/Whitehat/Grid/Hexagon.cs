namespace Whitehat.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Hexagon : MonoBehaviour
    {
        [SerializeField] private Renderer renderers;
        [SerializeField] private float hexDistance;
        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                renderers.gameObject.SetActive(value);
            }
        }
        private List<Building> lightingBuildings=new List<Building>();
        public void AddLightingBuilding(Building building) { lightingBuildings.Add(building); }
        public void RemoveLightingBuilding(Building building) { lightingBuildings.Remove(building); }

        public bool isCore;
        public Vector2 axis;
        public Building building;

        void Update()
        {
            Visible = lightingBuildings.Count > 0;
            if (visible)
            {
                renderers.gameObject.SetActive(GetComponent<Renderer>().isVisible);
            }
        }

        public Building Build(GameObject buildingPrefab)
        {
          //  if (building) { return; }
            building = GameObject.Instantiate(buildingPrefab, transform).GetComponent<Building>();
            building.hex = this;
            return building.GetComponent<Building>();
        }

        public void Empty()
        {
            if (!building) { return; }
            GameObject.Destroy(building.gameObject);
            building = null;
        }

        public void OnClick()
        {
            print("clicked");
        }

        public Hexagon NextHex(Vector3 direction, int distance) {
            /*float angle = 0;
            switch (direction)
            {
                case 0:
                    angle = 0;
                    break;
                case 1:
                    angle = 60;
                    break;
                case 2:
                    angle = 120;
                    break;
                case 3:
                    angle = 180;
                    break;
                case 4:
                    angle = 240;
                    break;
                case 5:
                    angle = 300;
                    break;
            }*/
            RaycastHit hit;
            Physics.SphereCast(transform.TransformPoint(direction * distance * hexDistance),1,Vector2.zero,out hit);
            return hit.collider ? hit.collider.GetComponent<Hexagon>() : null;
        }

    }
}
