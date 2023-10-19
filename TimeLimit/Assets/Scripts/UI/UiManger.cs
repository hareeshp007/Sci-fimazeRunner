using Game.player;
using Game.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI achevementsForCompletingLevel;
    [SerializeField]
    private List<string> achevements;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject inGame;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject gameWon;
    [SerializeField]
    private string mainMenuScene;
    [SerializeField]
    private int currLevel;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    public Image HealthImage;
    public TextMeshProUGUI HealthText;



    private void Awake()
    {
        PlayerService.Instance.SetUIManager(this);
    }
    private void Start()
    {
        init();
        
    }

    private void init()
    {
        currLevel = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        gameWon.SetActive(false);
        inGame.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerView.AchevementsUnlock += UnlockAchements;
    }

    private void UnlockAchements(int obj)
    {
        Debug.Log("Achevemenet :" + obj);
        switch (obj)
        {
            
            case 1:
                achevementsForCompletingLevel.text = achevements[0];
                break;
            case 2:
                achevementsForCompletingLevel.text = achevements[1];
                break;
            case 3:
                achevementsForCompletingLevel.text = achevements[2];
                break;
            case 4:
                achevementsForCompletingLevel.text = achevements[3];
                break;
        }

        
    }
    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked ;
        inGame.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        inGame.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void MainMenuActive()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        inGame.SetActive(false);
        gameOverMenu.SetActive(true);
    }
    public void gameWonMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        LevelManeger.Instance.MarkCurrentLevelCompleted();
        gameWon.SetActive(true);
        inGame.SetActive(false);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
    public void NextLevel()
    {
        LevelManeger.Instance.LoadNextLevel();
    }

    public void HealthSet(int Health)
    {
        health = Health;
        HealthUpdateGui();
    }

    internal void GameOver()
    {
        MainMenuActive();
    }
    private void HealthBarFiller()
    {
        HealthImage.fillAmount = (health / maxHealth);
        ColorChange();

    }
    private void ColorChange()
    {
        Color healthColor = Color.Lerp(Color.red, Color.blue, (health / maxHealth));
        HealthImage.color = healthColor;
    }
    public void HealthUpdateGui()
    {
        if (health > maxHealth) health = maxHealth;
        if (health < 0) health = 0;
        HealthText.text = health + "%";
        HealthBarFiller();
    }
    public void SetMaxHealth(int MaxHealth)
    {
        maxHealth= MaxHealth;
    }
}
