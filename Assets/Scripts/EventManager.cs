using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinskaVR
{
    public class EventManager : MonoBehaviour
    {
        public static Action<int> OnPinKnockOver;
        public static Action OnLogThrow;
        public static Action<int> OnTurnEnd;
        public static Action<int, int> OnScoreUpdate;
        public static Action OnAITurn;
    }
}