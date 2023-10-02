using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private PanelUIVisual settingsUI;
    
    private void Start()
    {
        continueButton.onClick.AddListener(LoadSavedGameScene);
        newGameButton.onClick.AddListener(LoadNewGameScene);
        settingsButton.onClick.AddListener(OpenSettingsPanel);
        quitButton.onClick.AddListener(Application.Quit);
        SetupContinueButton();
    }

    private void SetupContinueButton()
    {
        Color newGameColor = new Color32(255, 255, 255, 150);
        Color continueGameColor = new Color32(255, 255, 255, 255);

        bool isNewGame = SaveSystem.IsNewGame();
        continueButton.interactable = !isNewGame;
        continueButton.GetComponent<Image>().color = isNewGame ? newGameColor : continueGameColor;
    }

    private void LoadSavedGameScene()
    {
        int activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneBuildIndex + 1);
    }
    
    private void LoadNewGameScene()
    {
        SaveSystem.DeleteSaves();
        int activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneBuildIndex + 1);
    }
    
    private void OpenSettingsPanel()
    {
        settingsUI.Open();
    }
}
