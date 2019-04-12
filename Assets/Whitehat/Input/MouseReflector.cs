namespace Whitehat.Input
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Whitehat.Grid;
    using Whitehat.Player;

    public class MouseReflector : MonoBehaviour
    {
        public Camera mainCamera;
        public Hexagon mouseHex;
        public PlayerScript player;

        [SerializeField]private SpriteRenderer mouseBuildingSprite;
        [SerializeField]private GameObject mouseBuildingPrefab;
        public void AssignBuildingPrefab(GameObject newPrefab) { mouseBuildingPrefab = newPrefab; }

        private RaycastHit hit;

        /* Use this for initialization
        void Start()
        {

        }*/

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward, out hit) && hit.collider.GetComponent<Hexagon>() && hit.collider.GetComponent<Hexagon>().Visible)
            {
                mouseHex = hit.collider.GetComponent<Hexagon>();

                if (Input.GetMouseButtonDown(0) && mouseBuildingPrefab && !mouseHex.building)
                {
                    if (player.onTower)
                    {
                        Building secondTower=Build(mouseHex, player.onTower.gameObject);
                        BuildWalls(player.onTower.transform, secondTower.transform);
                        player.onTower = null;
                    }
                    else
                    {
                        Build(mouseHex, mouseBuildingPrefab);
                    }
                }

                if (Input.GetMouseButtonDown(1) && !mouseBuildingPrefab)
                {
                    mouseHex.Empty();
                }
                if(mouseHex.building){mouseBuildingSprite.color=Color.red;}else{mouseBuildingSprite.color=Color.white;}
                mouseBuildingSprite.enabled = true;
                mouseBuildingSprite.transform.parent = mouseHex.transform;
                mouseBuildingSprite.transform.localPosition = Vector3.zero;
            }
            else
            {
                mouseHex = null;
            }

            mouseBuildingSprite.sprite = mouseBuildingPrefab ? mouseBuildingPrefab.GetComponent<SpriteRenderer>().sprite : null;
        }

        public Building Build(Hexagon hex, GameObject buildingPrefab, bool ignoresTower=false)
        {
            Building building = buildingPrefab.GetComponent<Building>();
            if (player.RAM >= building.Maintenance &&
                    player.CPU >= building.Cost)
            {
                player.CPU -= building.Cost;
                hex.Build(buildingPrefab);
                hex.building.player = player;

                if (!ignoresTower && building.kind == "tower") { player.onTower = building; }
                return hex.building;
            }
            else { return null; }
        }

        public void BuildWalls(Transform tower1, Transform tower2)
        {
            float towerDistance = Vector3.Distance(tower2.position, tower1.position);
            Vector3 direction = (tower2.position - tower1.position).normalized;

            Vector3 boxCentre;
            RaycastHit[] hits;

            for (float distance=0; distance <= towerDistance; distance += Building.distance*2)
            {
                boxCentre = tower1.position + direction*distance;
                hits = Physics.BoxCastAll(boxCentre, new Vector3(Building.distance/4, Building.distance/2,1),Vector3.one);
                print(direction);
                if (hits.Length <1) { continue; }
                Building built=Build(hits[0].collider.GetComponent<Hexagon>(),player.wallPrefab);
                built.transform.position = boxCentre;
                foreach(RaycastHit hit in hits)
                {
                    hit.collider.GetComponent<Hexagon>().building = built;
                }
            }
           
        }
    }
}
