namespace Whitehat.Input
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Whitehat.Grid;
    using Whitehat.Player;
    using Whitehat.Mechanics;

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
            bool hasHit = Physics.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.forward, out hit);

            if (player.onMortarMark)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    position.z = 0;
                    GameObject.Instantiate(player.mortarMarkPrefab, position, Quaternion.identity);
                }
                if (Input.GetMouseButtonDown(1) && hasHit && hit.collider.GetComponent<MortarMark>())
                {
                    GameObject.Destroy(hit.collider.gameObject);
                }
            }

            if (hasHit && hit.collider.GetComponent<Hexagon>() && hit.collider.GetComponent<Hexagon>().Visible)
            {
                mouseHex = hit.collider.GetComponent<Hexagon>();
                if (player.onTower)
                {
                    if (TowerRightAngle(player.onTower.transform.position, mouseHex.transform.position))
                    {
                        mouseBuildingSprite.transform.parent = mouseHex.transform;
                        mouseBuildingSprite.transform.localPosition = Vector3.zero;
                    }
                    LocateWalls(player.onTower.transform, mouseHex.transform);
                    if (Input.GetMouseButtonDown(1))
                    {
                        player.onTower = null;
                    }
                }
                else
                {
                    mouseBuildingSprite.transform.parent = mouseHex.transform;
                    mouseBuildingSprite.transform.localPosition = Vector3.zero;
                }

                if (Input.GetMouseButtonDown(0) && mouseBuildingPrefab&&!player.onMortarMark)
                {
                    if (player.onTower)
                    {
                        if (TowerRightAngle(player.onTower.transform.position, mouseHex.transform.position))
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

                if (Input.GetMouseButtonDown(1) && !mouseBuildingPrefab && !player.onMortarMark)
                {
                    mouseHex.Empty();
                }
                if(mouseHex.building){mouseBuildingSprite.color=Color.red;}else{mouseBuildingSprite.color=Color.white;}
            }
            else
            {
                mouseHex = null;
            }

            mouseBuildingSprite.sprite = (mouseBuildingPrefab&&!player.onMortarMark) ? mouseBuildingPrefab.GetComponent<SpriteRenderer>().sprite : null;

            towerLines.enabled = player.onTower;
        }

        private bool TowerRightAngle(Vector3 tower1, Vector3 tower2)
        {
            Vector3 relativePosition = tower2 - tower1;
            float positionRatio = relativePosition.x / relativePosition.y;

            return Mathf.Approximately(Mathf.Atan(positionRatio),60*Mathf.Deg2Rad)
            || Mathf.Approximately(Mathf.Atan(-positionRatio), 60 * Mathf.Deg2Rad)
            || Mathf.Approximately(Mathf.Atan(positionRatio), 0)
            || Mathf.Approximately(Mathf.Atan(-positionRatio), 0);
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
            towerLines.SetPosition(1, mouseBuildingSprite.transform.position);
        }

        public void BuildWalls(Transform tower1, Transform tower2)
        {
            if (player.CPU < player.wallPrefab.GetComponent<Building>().Cost * (towerLines.positionCount - 2)) { return; }
            float angle = 0;
            float positionX = (tower2.position.x - tower1.position.x);
            float positionY = (tower2.position.y - tower1.position.y);
            angle = Mathf.Atan(positionX / positionY) * Mathf.Rad2Deg;

            angle = -angle;
            if (positionY < 0)
            {
                angle += 180;
            }

            foreach(RaycastHit hit in Physics.RaycastAll(tower1.position, tower2.position - tower1.position, Vector3.Distance(tower1.position, tower2.position))){
                if (hit.collider.GetComponent<Hexagon>()&& !hit.collider.GetComponent<Hexagon>().building)
                {
                    Building built=Build(hit.collider.GetComponent<Hexagon>(), player.wallPrefab, true);
                    if (!built)
                    {
                        return;
                    }
                    built.transform.eulerAngles = Vector3.forward * angle;
                }
            }
        }
    }
}
