namespace Whitehat.Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerScript : MonoBehaviour
    {
        public Text cpuNum;
        public Text ramNum;

        [SerializeField] private int cpu;
        [SerializeField] private int maxCPU;
        [SerializeField] private int productivity;
        [SerializeField] private int ram;
        [SerializeField] private int maxRAM;

        public int CPU { get { return cpu; } set { cpu = value; } }
        // public int MaxCPU { get { return maxCPU; } set { maxCPU = value; } }
        public int Productivity { get { return productivity; } set { productivity = value; } }
        public int RAM { get { return ram; } set { ram = value; } }
        public int MaxRAM { get { return maxRAM; } set { maxRAM = value; } }

        private float stopWatch = 1;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            cpuNum.text = "" + cpu + "+" + productivity + "/" + maxCPU;
            ramNum.text = "" + ram + "/" + maxRAM;

            cpu = Mathf.Clamp(cpu, 0, maxCPU);
            // ram = Mathf.Clamp(ram, 0, maxRAM);

            if (stopWatch <= 0)
            {
                CPU += productivity;
                stopWatch = 1;
            }
            else
            {
                stopWatch -= Time.deltaTime;
            }
        }
    }
}