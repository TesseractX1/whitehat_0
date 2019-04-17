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
        [SerializeField] private LineRenderer towerLines;

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
                if (player.onTower)
                {
                    LocateWalls(player.onTower.transform, mouseHex.transform);
                }

                if (Input.GetMouseButtonDown(0) && mouseBuildingPrefab)
                {
                    if (player.onTower)
                    {
                        Building secondTower = null;
                        if (mouseHex.building && mouseHex.building.kind == "tower")
                        {
                            secondTower = mouseHex.building;
                        }
                        else if (!mouseHex.building)
                        {
                            secondTower = Build(mouseHex, player.towerPrefab, true);
                        }
                        if (secondTower)
                        {
                            BuildWalls(player.onTower.transform, secondTower.transform);
                            player.onTower = null;
                        }
                    }
                    else if (mouseHex.building && mouseHex.building.kind == "tower")
                    {
                        player.onTower = mouseHex.building;
                    }
                    else if (!mouseHex.building)
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

            towerLines.enabled = player.onTower;
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
                if (hex.building.GetComponent<MarkReceptor>()) { 
                hex.building.GetComponent<MarkReceptor>().toggleList = player.GetComponent<MarkToggleList>(); }

                if (!ignoresTower && building.kind == "tower") { player.onTower = hex.building; }
                return hex.building;
            }
            else { return null; }
        }

        public void LocateWalls(Transform tower1, Transform tower2)
        {
            towerLines.SetPosition(0, player.onTower.transform.position);
            towerLines.SetPosition(1, mouseHex.transform.position);
            float towerDistance = Vector3.Distance(tower2.position, tower1.position);
            Vector3 direction = (tower2.position - tower1.position).normalized;

            int i = 0;
            for (float distance = 0; distance <= towerDistance; distance += Building.distance)
            {
                towerLines.positionCount = i+1 > towerLines.positionCount ? i+1 : towerLines.positionCount;
                towerLines.SetPosition(i, tower1.position + direction * distance);
                i++;
            }
            towerLines.positionCount = i + 1;
            towerLines.SetPosition(i, tower2.transform.position);
        }

        public void BuildWalls(Transform tower1, Transform tower2)
        {
            float angle = 0;
            float positionX = (tower2.position.x - tower1.position.x);
            float positionY = (tower2.position.y - tower1.position.y);
            angle = Mathf.Atan(positionX / positionY) * Mathf.Rad2Deg;

            angle = -angle;
            if (positionY < 0)
            {
                angle += 180;
            }

            Vector3 size = new Vector3(player.wallPrefab.GetComponent<BoxCollider2D>().size.x, player.wallPrefab.GetComponent<BoxCollider2D>().size.y, 0);


            if (player.CPU < player.wallPrefab.GetComponent<Building>().Cost * (towerLines.positionCount - 2)) { return; }
            for (int i=1;i< towerLines.positionCount-1; i++)
            {
                //RaycastHit2D hit2D = Physics2D.CircleCast(towerLines.GetPosition(i), 0.1f, Vector3.one, Mathf.Infinity, Unit.gridLayer);
               // if (hit2D&&hit2D.collider.GetComponent<Building>() && hit2D.collider.GetComponent<Building>().kind=="wall") { continue; }

                RaycastHit[] hits = Physics.SphereCastAll(towerLines.GetPosition(i), 0.1f, Vector3.one);
                if (hits.Length <= 0) { continue; }
                Building instance = hits[0].collider.GetComponent<Hexagon>().building;
                Building wallBuilt=Build(hits[0].collider.GetComponent<Hexagon>(), player.wallPrefab);
                hits[0].collider.GetComponent<Hexagon>().building = instance;
                foreach (RaycastHit hit in hits)
                {
                    if (!hit.collider.GetComponent<Hexagon>().building)
                    {
                        hit.collider.GetComponent<Hexagon>().building = wallBuilt;
                    }
                }
                wallBuilt.transform.position = towerLines.GetPosition(i);
                wallBuilt.transform.eulerAngles = Vector3.forward * angle;
            }
        }
    }
}
