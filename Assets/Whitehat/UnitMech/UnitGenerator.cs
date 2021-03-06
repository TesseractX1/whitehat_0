﻿namespace Whitehat.UnitMech
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Whitehat.Grid;
    using Whitehat.ObjectPools;

    public class UnitGenerator : MonoBehaviour
    {
        [SerializeField] private ActiveUnitManager manager;
        [SerializeField] private ObjectPool pool;

        [SerializeField] private Transform platform;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float interval;
        [SerializeField] private float amountPerTime;
        [SerializeField] private float randomness=1;

        private float stopWatch;

        [SerializeField] private Building enemyCore;

        // Use this for initialization
        void Start()
        {
            manager = GameObject.FindWithTag("ActiveUnitManager").GetComponent<ActiveUnitManager>();
            manager.generators.Add(this);
            stopWatch = interval;
        }

        // Update is called once per frame
        void Update()
        {
            if (stopWatch <= 0&&Random.value<randomness)
            {
                for (int i = 0; i < amountPerTime; i++)
                {
                    Unit generated=pool.UseAndInit(transform.position, transform.eulerAngles, platform).GetComponent<Unit>();
                    if (generated.GetComponent<Bot>())
                    {
                        generated.GetComponent<Bot>().enemyCore = enemyCore;
                    }
                }
                stopWatch = interval;
            }
            else if(manager.generatorsActive)
            {
                stopWatch -= Time.deltaTime;
            }
        }

        public void GenerateOnce(int count)
        {
            for(int i = 0; i < count; i++)
                {
                Unit generated = GameObject.Instantiate(prefab, transform.position, transform.rotation, platform).GetComponent<Unit>();
                if (generated.GetComponent<Bot>())
                {
                    generated.GetComponent<Bot>().enemyCore = enemyCore;
                }
            }
        }
    }
}