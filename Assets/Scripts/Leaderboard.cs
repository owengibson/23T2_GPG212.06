using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FinskaVR
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [Space]

        [SerializeField] private List<TextMeshProUGUI> humanRoundScores = new();
        [SerializeField] private List<TextMeshProUGUI> humanTotalScores = new();

        [SerializeField] private List<TextMeshProUGUI> aiRoundScores = new();
        [SerializeField] private List<TextMeshProUGUI> aiTotalScores = new();

        [SerializeField] private TextMeshProUGUI humanNeeded;
        [SerializeField] private TextMeshProUGUI aiNeeded;

        private bool _clearLeaderboardNextTurn = false;

        private void Start()
        {
            ClearLeaderboard();

            humanNeeded.text = "Needed: 50";
            aiNeeded.text = "Needed: 50";
        }

        private void UpdateScore(int roundScore, int totalScore)
        {
            if (_clearLeaderboardNextTurn)
            {
                ClearLeaderboard();
                _clearLeaderboardNextTurn = false;
            }

            int index = (gameManager.roundNumber - 1) % 8;

            if (gameManager.currentPlayer == GameManager.PlayerType.Human)
            {
                humanRoundScores[index].text = "+" + roundScore.ToString("0");
                humanTotalScores[index].text = totalScore.ToString("0");
                if (roundScore == 0) humanRoundScores[index].color = Color.red;
                else humanRoundScores[index].color = Color.black;

                humanNeeded.text = "Needed: " + (50 - totalScore).ToString("0");
            }
            else
            {
                aiRoundScores[index].text = "+" + roundScore.ToString("0");
                aiTotalScores[index].text = totalScore.ToString("0");
                if (roundScore == 0) aiRoundScores[index].color = Color.red;
                else aiRoundScores[index].color = Color.black;

                aiNeeded.text = "Needed: " + (50 - totalScore).ToString("0");

                if (index == 7) _clearLeaderboardNextTurn = true;
            }
        }

        private void ClearLeaderboard()
        {
            foreach (var score in humanRoundScores) score.text = "";
            foreach (var score in humanTotalScores) score.text = "";
            foreach (var score in aiRoundScores) score.text = "";
            foreach (var score in aiTotalScores) score.text = "";
        }

        private void OnEnable()
        {
            EventManager.OnScoreUpdate += UpdateScore;
        }
        private void OnDisable()
        {
            EventManager.OnScoreUpdate -= UpdateScore;
        }
    }
}