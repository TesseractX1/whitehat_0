namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Mechanics;

    public class ActiveUnitManager : MonoBehaviour
    {
        [SerializeField] private AttackWaveManager waveManager;

        [SerializeField] private int unitLimit;
        public int UnitLimit { get { return unitLimit; } set { unitLimit = value; } }
        public bool generatorsActive;

        public List<UnitGenerator> generators;
        private int unitCount;
        public int UnitCount { get { return unitCount; } private set { } }
        [SerializeField]private Transform activeUnitLayer;

        // Update is called once per frame
        void Update()
        {
            unitCount = activeUnitLayer.childCount;
            generatorsActive = waveManager.onWave && unitCount < unitLimit;
        }
    }
}