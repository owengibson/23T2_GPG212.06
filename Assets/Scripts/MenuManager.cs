using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinskaVR
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
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
    }
}
