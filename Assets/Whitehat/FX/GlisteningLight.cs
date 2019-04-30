namespace Whitehat.FX
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GlisteningLight : MonoBehaviour
    {
        [SerializeField] private float maxSize;
        [SerializeField] private float minSize;
        [SerializeField] private float changingRate;

        private Vector3 beginningScale;
        private float scale;
        private float direction;

        // Use this for initialization
        void Start()
        {
            beginningScale = transform.localScale;
            direction = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (scale >= maxSize)
            {
                direction = -1;
            }

            if (scale <= minSize)
            {
                direction = 1;
            }

            scale += direction*Time.deltaTime * changingRate;
            transform.localScale = beginningScale*scale;
        }
    }
}