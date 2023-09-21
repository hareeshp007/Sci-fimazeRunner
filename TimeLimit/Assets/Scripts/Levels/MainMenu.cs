using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelMenu;
    public GameObject MainMenuObject;
    private void Awake()
    {
        MainMenuObject.SetActive(true);
        LevelMenu.SetActive(false);
    }
    public void Play()
    {
        MainMenuObject.SetActive(false);
        LevelMenu.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
