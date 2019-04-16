namespace Whitehat.Input
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] private GameObject buildingPrefab;
        [SerializeField] private MouseReflector mouse;

        [SerializeField] private KeyCode hotkey;

        public void AssignPrefab(bool change)
        {
           // if (!change) { return; }
            mouse.AssignBuildingPrefab(buildingPrefab);
        }

        private void Update()
        {
            if (Input.GetKeyDown(hotkey))
            {
                GetComponent<Toggle>().isOn = true;
            }
        }
    }

}