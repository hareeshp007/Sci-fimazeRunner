using Game.player;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI AchevementsForCompletingLevel;
    [SerializeField]
    private List<string> Achevements;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject InGame;
    [SerializeField]
    private GameObject GameOverMenu;
    [SerializeField]
    private GameObject GameWon;
    [SerializeField]
    private string mainMenuScene;
    [SerializeField]
    private int CurrLevel;
    [SerializeField]
    private int MaxLevel=3;

    public Image HealthImage;
    public TextMeshProUGUI HealthText;

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float lerpSpeed;

    private Coroutine AchevementDisplay;

    private void Awake()
    {
        PlayerService.Instance.SetUIManager(this);
    }
    private void Start()
    {

        CurrLevel = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        GameOverMenu.SetActive(false);
        GameWon.SetActive(false);
        InGame.SetActive(true);
        PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerView.AchevementsUnlock += UnlockAchements;
    }
    private void UnlockAchements(int obj)
    {
        Debug.Log("Achevemenet :" + obj);
        switch (obj)
        {
            
            case 1:
                AchevementsForCompletingLevel.text = Achevements[0];
                break;
            case 2:
                AchevementsForCompletingLevel.text = Achevements[1];
                break;
            case 3:
                AchevementsForCompletingLevel.text = Achevements[2];
                break;
            case 4:
                AchevementsForCompletingLevel.text = Achevements[3];
                break;
        }

        
    }
    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked ;
        InGame.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        InGame.SetActive(false);
        PauseMenu.SetActive(true);
    }
    public void MainMenuActive()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        InGame.SetActive(false);
        GameOverMenu.SetActive(true);
    }
    public void GameWonMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        LevelManeger.Instance.MarkCurrentLevelCompleted();
        GameWon.SetActive(true);
        InGame.SetActive(false);
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
