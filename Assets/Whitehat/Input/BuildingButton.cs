namespace Whitehat.Input
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] private GameObject buildingPrefab;
        [SerializeField] private MouseReflector mouse;

        public void AssignPrefab(bool change)
        {
           // if (!change) { return; }
            mouse.AssignBuildingPrefab(buildingPrefab);
        }
    }
}