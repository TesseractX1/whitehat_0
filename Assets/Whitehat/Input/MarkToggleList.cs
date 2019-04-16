namespace Whitehat.Input
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MarkToggleList : MonoBehaviour
    {
        public List<bool> toggles;

        public void SetToggle(int index, bool value)
        {
            toggles[index] = value;
        }
    }
}