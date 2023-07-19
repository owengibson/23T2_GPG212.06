using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using oezyowen;

namespace FinskaVR
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Transform throwPoint;
        [SerializeField] private GameObject[] pins;
        [SerializeField] private Transform log;

        private float _maxThrowOffset = 0.15f;
        private float _targetHeight = 0.1f;

        private Rigidbody _logRigidbody;

        private void Start()
        {
            _logRigidbody = log.GetComponent<Rigidbody>();
        }
        private void StartAITurn()
        {
            Transform chosenPin = pins[Random.Range(0, pins.Length)].transform;
            Vector2 bottomLeft = new Vector2(chosenPin.position.x - _maxThrowOffset, chosenPin.position.z - _maxThrowOffset);
            Vector2 topRight = new Vector2(chosenPin.position.x + _maxThrowOffset, chosenPin.position.z + _maxThrowOffset);
            Vector2 targetXZ = Utils.RandomRangeVector2(bottomLeft, topRight);
            Vector3 target = new Vector3(targetXZ.x, _targetHeight, targetXZ.y);
            
            StartCoroutine(AITurn(target));
        }

        private IEnumerator AITurn(Vector3 target)
        {
            yield return new WaitForSecondsRealtime(1f);
            log.position = throwPoint.position;
            gameManager.EnableLogThrowDetectorAfterWait();
            _logRigidbody.isKinematic = true;

            yield return new WaitForSecondsRealtime(2f);
            _logRigidbody.isKinematic = false;
            _logRigidbody.velocity = new Vector3((target-throwPoint.position).x * 2, 0.51f, (target-throwPoint.position).z * 2);
        }

        private void OnEnable()
        {
            EventManager.OnAITurn += StartAITurn;
        }
        private void OnDisable()
        {
            EventManager.OnAITurn -= StartAITurn;
        }
    }
}
