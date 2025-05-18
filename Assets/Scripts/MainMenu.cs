using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Text tutorialText;
    public Button nextButton;
    public Image tutorialImage;
    public GameObject objective;
    private int tutorialStep = 0;

    private string[] tutorialSteps = new string[]
    {
        "Step 1: Click the email icon and wait for the inbox to open",
        "Step 2: Click the email subject to view the full message",
        "Step 3: It will open the content of email subject",
        "Step 4: Choose your answer (SAFE/PHISING) ",
        "Tutorial complete! Good luck!"
    };

    public Sprite[] tutorialImages; // Assign 5 images from the Inspector

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Ganti dengan nama scene gameplay kamu
    }

    public void ShowTutorial()
    {
        tutorialStep = 0;
        ShowStep();
        tutorialPanel.SetActive(true);
        nextButton.gameObject.SetActive(true);
        objective.SetActive(false);
    }

    public void NextTutorialStep()
    {
        tutorialStep++;

        if (tutorialStep < tutorialSteps.Length)
        {
            ShowStep();
        }
        else
        {
            tutorialPanel.SetActive(false);
            objective.SetActive(true);
        }
    }

    private void ShowStep()
    {
        tutorialText.text = tutorialSteps[tutorialStep];

        if (tutorialStep < tutorialImages.Length && tutorialImages[tutorialStep] != null)
        {
            tutorialImage.sprite = tutorialImages[tutorialStep];
            tutorialImage.gameObject.SetActive(true);
        }
        else
        {
            tutorialImage.gameObject.SetActive(false); // If no image for the step
        }
    }

    public void ExitGame()
    {
        
        Application.Quit();
    }
}
