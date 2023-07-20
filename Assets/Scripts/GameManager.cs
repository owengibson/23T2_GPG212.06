using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FinskaVR
{
    public class GameManager : MonoBehaviour
    {
        public enum PlayerType { Human, AI };

        public int roundNumber = 0;
        public PlayerType currentPlayer = PlayerType.Human;
        public Dictionary<PlayerType, int> playerScores = new() { { PlayerType.Human, 0 }, { PlayerType.AI, 0 } };

        [SerializeField] private GameObject logThrowDetector;
        [SerializeField] private GameObject log;
        [SerializeField] private TextMeshProUGUI humanText;
        [SerializeField] private TextMeshProUGUI aiText;

        private int _consecutiveHumanMisses = 0;
        private int _consecutiveAIMisses = 0;

        private void Start()
        {
            roundNumber = 1;
        }

        private void EndTurn(int score)
        {
            playerScores[currentPlayer] += score;

            if (playerScores[currentPlayer] == 50)
            {
                EventManager.OnGameOver?.Invoke(currentPlayer.ToString());
            }
            else if (playerScores[currentPlayer] > 50)
            {
                playerScores[currentPlayer] = 25;
            }

            EventManager.OnScoreUpdate?.Invoke(score, playerScores[currentPlayer]);

            if (currentPlayer == PlayerType.Human)
            {
                if (score == 0) _consecutiveHumanMisses++;
                else _consecutiveHumanMisses = 0;

                if (_consecutiveHumanMisses == 5)
                {
                    EventManager.OnGameOver?.Invoke("AI");
                }

                ToggleLogGrabbableComponents(false);
                currentPlayer = PlayerType.AI;

                humanText.color = Color.black;
                aiText.color = Color.green;

                EventManager.OnAITurn?.Invoke();
            }
            else
            {
                if (score == 0) _consecutiveAIMisses++;
                else _consecutiveAIMisses = 0;

                if (_consecutiveAIMisses == 5)
                {
                    EventManager.OnGameOver?.Invoke("Human");
                }

                ToggleLogGrabbableComponents(true);
                currentPlayer = PlayerType.Human;

                aiText.color = Color.black;
                humanText.color = Color.green;

                roundNumber++;
            }
        }

        private void ToggleLogGrabbableComponents(bool isGrabbable)
        {
            log.GetComponent<DistanceGrabInteractable>().enabled = isGrabbable;
            log.GetComponent<PhysicsGrabbable>().enabled = isGrabbable;
            log.GetComponent<Grabbable>().enabled = isGrabbable;

        }

        public void EnableLogThrowDetectorAfterWait()
        {
            StartCoroutine(EnableLogThrowDetector());
        }
        private IEnumerator EnableLogThrowDetector()
        {
            yield return new WaitForSecondsRealtime(1f);
            logThrowDetector.SetActive(true);
            Debug.Log("Log throw detector enabled.");
        }

        private void Update()
        {
            // log respawn
            if (log.transform.position.y < -2f) log.transform.position = new Vector3(0f, 0.12f, 0.44f);
        }

        private void OnEnable()
        {
            EventManager.OnTurnEnd += EndTurn;
        }
        private void OnDisable()
        {
            EventManager.OnTurnEnd -= EndTurn;

            PlayerPrefs.DeleteAll();
        }
    }
}