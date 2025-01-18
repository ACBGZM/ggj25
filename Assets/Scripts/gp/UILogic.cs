using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILogic : MonoBehaviour
{
    // menu
    [SerializeField] private List<GameObject> _enablePanels;
    [SerializeField] private List<GameObject> _disablePanels;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _gameOverPanelScoreText;

    // hud
    [SerializeField] private TMP_Text _inGameScoreText;
    [SerializeField] private TMP_Text _inGamePlayerInfoText;

    [SerializeField] private Camera _gameplayCamera;
    
    public void SwitchPanel(bool enable)
    {
        foreach (GameObject panel in _disablePanels)
        {
            panel.SetActive(!enable);
        }

        foreach (GameObject panel in _enablePanels)
        {
            panel.SetActive(enable);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        foreach (GameObject panel in _disablePanels)
        {
            panel.SetActive(false);
        }

        PauseGame();

        _gameOverPanel.SetActive(true);
        _gameOverPanelScoreText.text = _inGameScoreText.text;

    }
    
    public void UpdateScore(uint score)
    {
        _inGameScoreText.text = score.ToString();
    }
    
    public void UpdatePlayerInfo(string info)
    {
        _inGamePlayerInfoText.text = info;
    }
}