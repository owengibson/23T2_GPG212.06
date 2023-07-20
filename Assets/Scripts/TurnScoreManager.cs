using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinskaVR
{
    public class TurnScoreManager : MonoBehaviour
    {
        [SerializeField] private float turnTimer = 4f;

        private int _noOfPinsKnockedOver;
        private int _pinValue;
        private bool _hasTimerStarted = false;

        private void CalculateScore()
        {
            int score = 0;
            if (_noOfPinsKnockedOver > 1) score = _noOfPinsKnockedOver;
            else if (_noOfPinsKnockedOver == 1) score = _pinValue;
            else score = 0;

            EventManager.OnTurnEnd?.Invoke(score);
            ResetScores();
        }

        private void IncrementPinsKnockedOver(int pinValue)
        {
            _noOfPinsKnockedOver++;
            _pinValue = pinValue;
        }

        public void ResetScores()
        {
            _noOfPinsKnockedOver = 0;
            _pinValue = 0;
            _hasTimerStarted = false;
        }

        private void StartTurnTimer()
        {
            if (!_hasTimerStarted)
            {
                StartCoroutine(TurnTimer(turnTimer));
                _hasTimerStarted = true;
            }
        }

        private IEnumerator TurnTimer(float turnTimer)
        {
            yield return new WaitForSeconds(turnTimer);
            CalculateScore();
        }

        private void OnEnable()
        {
            EventManager.OnPinKnockOver += IncrementPinsKnockedOver;
            EventManager.OnLogThrow += StartTurnTimer;
        }
        private void OnDisable()
        {
            EventManager.OnPinKnockOver -= IncrementPinsKnockedOver;
            EventManager.OnLogThrow -= StartTurnTimer;
        }
    }
}