using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public GameObject settingsPanel;

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
