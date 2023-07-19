using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinskaVR
{
    public class PinReset : MonoBehaviour
    {
        private const float _groundPosition = 0.0765f;

        public void ResetPins()
        {
            StartCoroutine(ResetPinsAfterWait());
        }

        private IEnumerator ResetPinsAfterWait()
        {
            yield return new WaitForSecondsRealtime(1f);
            foreach (Transform child in transform)
            {
                child.eulerAngles = new Vector3(-90f, 0f, 0f);
                Vector3 pivot = child.Find("Pivot").position;
                child.position = new Vector3(pivot.x, _groundPosition, pivot.z);
            }
        }
    }
}