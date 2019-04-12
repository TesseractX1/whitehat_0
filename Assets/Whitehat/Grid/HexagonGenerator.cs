namespace Whitehat.Grid
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Player;

    public class HexagonGenerator : MonoBehaviour
    {
        public Hexagon currentHexagon;
        [SerializeField] private PlayerScript player;

        [SerializeField] private GameObject hexagon;
        [SerializeField] private float distance;
        private int number;
        private bool stop = false;
        public int sideLength;
        private int thisRowLength;
        private int thisRow;
        private int cellThisRow = 1;
        private bool evenLine = true;

        public GameObject[] buildingPrefabIndex;
        public Hexagon core;


        // Use this for initialization
        void Start()
        {
            thisRowLength = sideLength;
        }

        // Update is called once per frame
        void Update()
        {
            GridRound();

            core.building.LightUp(6);
        }

        private void GenerateGrid(int position)
        {
            Vector3 relativePosition = Vector3.zero;
            switch (position)
            {
                case 0:
                    relativePosition = new Vector3(0, 0, distance);
                    break;
                case 1:
                    relativePosition = new Vector3((distance / 2) * Mathf.Tan(60 * Mathf.Deg2Rad), 0, distance / 2);
                    break;
                case 2:
                    relativePosition = new Vector3((distance / 2) * Mathf.Tan(60 * Mathf.Deg2Rad), 0, -distance / 2);
                    break;
                case 3:
                    relativePosition = new Vector3(0, 0, -distance);
                    break;
                case 4:
                    relativePosition = new Vector3(-(distance / 2) * Mathf.Tan(60 * Mathf.Deg2Rad), 0, -distance / 2);
                    break;
                case 5:
                    relativePosition = new Vector3(-(distance / 2) * Mathf.Tan(60 * Mathf.Deg2Rad), 0, distance / 2);
                    break;
            }
            currentHexagon = GameObject.Instantiate(hexagon, currentHexagon.transform.position - relativePosition, 
                currentHexagon.transform.rotation, transform).GetComponent<Hexagon>();
            currentHexagon.axis = new Vector2(thisRow - sideLength + 1, cellThisRow - thisRowLength / 2);
            if (evenLine && currentHexagon.axis.y >= 0)
            {
                currentHexagon.axis.y++;
            }
            if (currentHexagon.axis == Vector2.zero)
            {
                currentHexagon.isCore = true;
                currentHexagon.Build(buildingPrefabIndex[0]);
                core = currentHexagon;
                core.building.player = player;
            }
            //GetComponent<SpriteRenderer>().color = Color.white;
        }

        private void GridRound()
        {
            while (!stop)
            {
                if (thisRow >= sideLength * 2 - 1)
                {
                    stop = true;
                }
                if (cellThisRow >= thisRowLength)
                {
                    thisRow++;
                    if (thisRow < sideLength)
                    {
                        thisRowLength++;
                    }
                    else
                    {
                        thisRowLength--;
                    }
                    cellThisRow = 1;
                    bool instantEvenLine = evenLine;
                    if (thisRow + 1 > sideLength)
                    {
                        instantEvenLine = !instantEvenLine;
                    }
                    if (instantEvenLine)
                    {
                        GenerateGrid(5);
                    }
                    else
                    {
                        GenerateGrid(4);
                    }
                    evenLine = !evenLine;
                }
                else
                {
                    if (evenLine)
                    {
                        GenerateGrid(0);
                    }
                    else
                    {
                        GenerateGrid(3);
                    }
                    cellThisRow++;
                }
            }
        }
    }
}