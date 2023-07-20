using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FinskaVR
{
    public class PinKnockDownDetector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pinText;

        public int pinValue;

        private void Start()
        {
            pinValue = int.Parse(pinText.text);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                EventManager.OnPinKnockOver?.Invoke(pinValue);
                Debug.Log($"Pin number {pinValue} knocked down");
            }
        }
    }
}