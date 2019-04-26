namespace Whitehat.Mechanics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MortarMark : MonoBehaviour
    {
        [SerializeField]private float lifeTime;

        // Update is called once per frame
        void Update()
        {
            if (lifeTime <= 0) { GameObject.Destroy(gameObject); } else { lifeTime -= Time.deltaTime; }
        }
    }
}