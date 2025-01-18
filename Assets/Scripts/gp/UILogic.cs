using System.Collections.Generic;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    // menu
    [SerializeField] private List<GameObject> _enablePanels;
    [SerializeField] private List<GameObject> _disablePanels;
    [SerializeField] private GameObject _gameOverPanel;

    // hud
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
    }
}