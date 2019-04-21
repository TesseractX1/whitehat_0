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
        public bool generatorsActive;

        public List<UnitGenerator> generators;
        private int unitCount;
        public int UnitCount { get { return unitCount; } set { unitCount = value; } }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            unitCount = Mathf.Max(unitCount, 0);
            generatorsActive = waveManager.onWave && unitCount < unitLimit;
        }
    }
}