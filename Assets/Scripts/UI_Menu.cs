using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] GameObject tutorialActive;
    [SerializeField] GameObject CreditsActive;
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Background;

    [SerializeField] Sprite[] tutorialImage;
    [SerializeField] Image tutorialDisplay;
    private int tutorialIndex = 0;

    [SerializeField] GameObject NextButton;

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

    public void TutorialClose()
    {
        tutorialActive.SetActive(false);
        Button.SetActive(true);
        Background.SetActive(true);
    }

    public void TutorialBack()
    {
        if(tutorialIndex==0)
        {
            tutorialActive.SetActive(false);
            Button.SetActive(true);
            Background.SetActive(true);
        }
        else
        {
            tutorialIndex--;
            tutorialDisplay.sprite = tutorialImage[tutorialIndex];
            if(!NextButton.activeInHierarchy)
            {
                NextButton.SetActive(true);
            }
        }
    }

    public void TutorialNext()
    {
        if (tutorialIndex < 5)
        {
            tutorialIndex++;
            tutorialDisplay.sprite = tutorialImage[tutorialIndex];
            if (tutorialIndex == 5)
            {
                NextButton.SetActive(false);
            }
        }
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
