using UnityEngine;
using System.Collections;

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
