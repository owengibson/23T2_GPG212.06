using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FinskaVR
{
    public class PinKnockDownDetector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pinText;

        private int _pinValue;

        private void Start()
        {
            _pinValue = int.Parse(pinText.text);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                EventManager.OnPinKnockOver?.Invoke(_pinValue);
                Debug.Log($"Pin number {_pinValue} knocked down");
            }
        }
    }
}