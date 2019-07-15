using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScriptClick : MonoBehaviour
{
    public ControllerGameScript cgs;
    public GameObject menuPanel;

    private void Start()
    {
        Show();
    }

    public void Show()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    public void StartClick()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
        cgs.NewGame();
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
