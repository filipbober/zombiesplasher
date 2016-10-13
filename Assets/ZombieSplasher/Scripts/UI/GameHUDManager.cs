// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;
using UnityEngine.UI;

namespace ZombieSplasher
{
    public class GameHUDManager : MonoBehaviour
    {
        public Text LivesLeftText;
        public Text EnemiesLeftText;

        void OnEnable()
        {
            GameManager.EnemySpawnedNotification += UpdateEnemiesLeft;
        }

        void OnDisable()
        {
            GameManager.EnemySpawnedNotification -= UpdateEnemiesLeft;
        }

        void UpdateEnemiesLeft(object sender, ActorPropertiesEventArgs e)
        {
            EnemiesLeftText.text = "-1";
        }

    }
}