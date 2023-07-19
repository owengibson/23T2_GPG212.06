using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinskaVR
{
    public class LogThrowDetector : MonoBehaviour
    {
        [SerializeField] private MeshCollider logCollider;

        private bool _hasLogBeenThrown = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Log Throw Detector")
            {
                _hasLogBeenThrown = true;
                EventManager.OnLogThrow?.Invoke();
                Debug.Log("Log throw detected");

                other.gameObject.SetActive(false);
            }
        }

        public void DisableLogCollision()
        {
            if (_hasLogBeenThrown)
            {
                logCollider.isTrigger = true;
            }
        }

        public void EnableLogCollision()
        {
            if (_hasLogBeenThrown)
            {
                logCollider.isTrigger = false;
                _hasLogBeenThrown = false;
            }
        }
    }
}