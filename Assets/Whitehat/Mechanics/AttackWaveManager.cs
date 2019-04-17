namespace Whitehat.Mechanics
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class AttackWaveManager : MonoBehaviour
    {
        [SerializeField] private Text textIndicator;

        [SerializeField] private float waveInterval;
        [SerializeField] private float waveLength;
        private float stopWatch;

        public bool onWave;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (stopWatch <= 0)
            {
                onWave = !onWave;
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