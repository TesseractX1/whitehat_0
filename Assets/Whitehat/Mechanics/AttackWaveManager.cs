namespace Whitehat.Mechanics
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Whitehat.UnitMech;

    public class AttackWaveManager : MonoBehaviour
    {
        [SerializeField] private Text textIndicator;

        [SerializeField] private float waveInterval;
        [SerializeField] private float waveLength;
        private float stopWatch;

        public bool onWave;

        public int wave;
        public GameObject[] generators;

        // Update is called once per frame
        void Update()
        {
            if (stopWatch <= 0)
            {
                onWave = !onWave;
                if (onWave)
                {
                    wave++;
                    if (wave < generators.Length) { generators[wave].SetActive(true); }
                    if (GetComponent<ActiveUnitManager>().UnitLimit <= 600)
                    {
                        GetComponent<ActiveUnitManager>().UnitLimit += 25;
                    }
                    if (wave > 6)
                    {
                        waveLength += 10;
                    }
                }
                stopWatch = onWave ? waveLength : waveInterval;
            }
            else
            {
                stopWatch -= Time.deltaTime;
            }

            textIndicator.text =  Mathf.Round(stopWatch*100)/100 + (onWave ? " seconds left in wave" : " seconds left befre next wave");
        }
    }
}