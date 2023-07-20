using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using oezyowen;

namespace FinskaVR
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PinReset pinResetter;
        [SerializeField] private Transform throwPoint;
        [SerializeField] private GameObject[] pins;
        [SerializeField] private Transform log;

        private float _generalMaxThrowOffset = 0.15f;
        private float _singleMaxThrowOffset = 0.2f;
        private float _targetHeight = 0.1f;

        private Rigidbody _logRigidbody;

        private void Start()
        {
            _logRigidbody = log.GetComponent<Rigidbody>();
        }
        private void StartAITurn()
        {
            Vector3 target;
            int currentScore = gameManager.playerScores[GameManager.PlayerType.AI];
            if (50 - currentScore <= 12 && Random.Range(1,2) == 2)
            {
                // target single pin
                int needed = 50 - currentScore;
                Vector3 chosenPin = Vector3.zero;
                foreach(var pin in pins)
                {
                    int pinValue = pin.GetComponentInChildren<PinKnockDownDetector>().pinValue;
                    if (pinValue == needed)
                    {
                        chosenPin = pin.transform.position;
                        break;
                    }
                }
                Vector2 bottomLeft = new Vector2(chosenPin.x - _singleMaxThrowOffset, chosenPin.z - _singleMaxThrowOffset);
                Vector2 topRight = new Vector2(chosenPin.x + _singleMaxThrowOffset, chosenPin.z + _singleMaxThrowOffset);
                Vector2 targetXZ = Utils.RandomRangeVector2(bottomLeft, topRight);
                target = new Vector3(targetXZ.x, _targetHeight, targetXZ.y);
            }
            else
            {
                // target general area
                Vector3 chosenPin = pins[Random.Range(0, pins.Length)].transform.position;
                Vector2 bottomLeft = new Vector2(chosenPin.x - _generalMaxThrowOffset, chosenPin.z - _generalMaxThrowOffset);
                Vector2 topRight = new Vector2(chosenPin.x + _generalMaxThrowOffset, chosenPin.z + _generalMaxThrowOffset);
                Vector2 targetXZ = Utils.RandomRangeVector2(bottomLeft, topRight);
                target = new Vector3(targetXZ.x, _targetHeight, targetXZ.y);
            }

            
            StartCoroutine(AITurn(target));
        }

        private IEnumerator AITurn(Vector3 target)
        {
            yield return new WaitForSecondsRealtime(1f);
            log.position = throwPoint.position;
            gameManager.EnableLogThrowDetectorAfterWait();
            pinResetter.ResetPins();
            _logRigidbody.isKinematic = true;

            yield return new WaitForSecondsRealtime(3f);
            _logRigidbody.isKinematic = false;
            _logRigidbody.velocity = new Vector3((target-throwPoint.position).x * 2, 0.51f, (target-throwPoint.position).z * 2);
        }

        private void DisableAI(string x)
        {
            enabled = false;
        }
        private void DisableAI()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            EventManager.OnAITurn += StartAITurn;
            EventManager.OnGameOver += DisableAI;
        }
        private void OnDisable()
        {
            EventManager.OnAITurn -= StartAITurn;
            EventManager.OnAITurn -= DisableAI;
        }

    }
}
