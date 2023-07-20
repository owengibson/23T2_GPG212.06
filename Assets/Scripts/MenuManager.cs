using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinskaVR
{
    public class MenuManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject tutorial;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TextMeshProUGUI winnerText;
        [Space]

        [Header("Object References")]
        [SerializeField] private Rigidbody log;

        void Start()
        {
            log.isKinematic = true;
        }

        public void Play()
        {
            mainMenu.SetActive(false);
            log.isKinematic = false;
        }

        public void OpenTutorial()
        {
            tutorial.SetActive(true);
        }

        public void CloseTutorial()
        {
            tutorial.SetActive(false);
        }

        public void Restart()
        {
            SceneManager.LoadScene("Main");
        }

        private void GameOverScreen(string winner)
        {
            gameOverScreen.SetActive(true);
            log.isKinematic = true;

            if (winner == "Human") winnerText.text = "You win!";
            else if (winner == "AI") winnerText.text = "AI wins!";
            else Debug.Log("Unrecognised winner");
        }

        private void OnEnable()
        {
            EventManager.OnGameOver += GameOverScreen;
        }
        private void OnDisable()
        {
            EventManager.OnGameOver -= GameOverScreen;
        }
    }
}
