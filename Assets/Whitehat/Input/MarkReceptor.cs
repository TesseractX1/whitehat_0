namespace Whitehat.Input
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(SpriteRenderer))]

    public class MarkReceptor : MonoBehaviour
    {
        public MarkToggleList toggleList;
        [SerializeField] private int toggleNumber;

        [SerializeField] private Color toggledColor;
        [SerializeField] private Color unToggledColor = Color.white;

        // Update is called once per frame
        void Update()
        {
            GetComponent<SpriteRenderer>().color = toggleList.toggles[toggleNumber] ? toggledColor : unToggledColor;
        }
    }
}