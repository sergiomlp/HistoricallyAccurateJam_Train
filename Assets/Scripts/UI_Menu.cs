using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] GameObject tutorialActive;
    [SerializeField] GameObject CreditsActive;
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Background;

    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void TutorialBtn()
    {
        tutorialActive.SetActive(true);
        Button.SetActive(false);
        Background.SetActive(false);
    }

    public void TutorialBack()
    {
        tutorialActive.SetActive(false);
        Button.SetActive(true);
        Background.SetActive(true);
    }

    public void CreditsBtn() 
    { 
        CreditsActive.SetActive(true);
        Button.SetActive(false);
        Background.SetActive(false);
    }
    public void CreditsBack()
    {
        CreditsActive.SetActive(false);
        Button.SetActive(true);
        Background.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
