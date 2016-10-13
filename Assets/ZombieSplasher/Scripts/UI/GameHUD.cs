// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace ZombieSplasher
{
    public class GameHUD : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }

        public void OnPlay()
        {
            MainController.SwitchScene(Scenes.TestScene);
        }
    }
}
