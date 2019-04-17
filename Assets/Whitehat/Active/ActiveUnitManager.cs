namespace Whitehat.Active
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
        public int unitCount;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            generatorsActive = waveManager.onWave && unitCount < unitLimit;
        }
    }
}