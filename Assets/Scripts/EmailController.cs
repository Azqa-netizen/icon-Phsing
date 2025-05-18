using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EmailController : MonoBehaviour
{
    public AudioClip correctSFX;
    public AudioClip wrongSFX;
    private AudioSource audioSource;
    public GameObject inboxPanel;
    public GameObject emailAppIcon;
    public Text notifBadgeText;
    public GameObject emailPopupPanel;
    public Text emailContentText;
    public GameObject answerPanel;

    
    public TaskManager taskManager;
    public EmailSpawner spawner;

    private int unreadEmails = 20;
    private bool currentIsPhishing;
    private GameObject currentEmailGO;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateBadge();
        inboxPanel.SetActive(false);
        emailPopupPanel.SetActive(false);
        answerPanel.SetActive(false);
    }

    public void OnEmailAppClicked()
    {
        inboxPanel.SetActive(!inboxPanel.activeSelf);
    }

    public void UpdateBadge()
    {
        notifBadgeText.text = unreadEmails.ToString();
        notifBadgeText.gameObject.SetActive(unreadEmails > 0);
    }

    public void OpenEmailDetail(string content, bool isPhishing, GameObject emailGO)
    {
        emailContentText.text = content;
        currentIsPhishing = isPhishing;
        currentEmailGO = emailGO;

        emailPopupPanel.SetActive(true);
        answerPanel.SetActive(true);
    }

    public void CloseEmailDetail()
    {
        emailPopupPanel.SetActive(false);
        answerPanel.SetActive(false);
    }

    public void ChooseSafe()
    {
        CheckAnswer(false);
    }

    public void ChoosePhishing()
    {
        CheckAnswer(true);
    }

    private void CheckAnswer(bool playerAnswer)
    {
        bool isCorrect = playerAnswer == currentIsPhishing;
        taskManager.ReportAnswer(isCorrect); // ✅ panggil selalu

        // Mainkan sound effect
        if (isCorrect)
            audioSource.PlayOneShot(correctSFX);
        else
            audioSource.PlayOneShot(wrongSFX);

        emailContentText.text = "";
        CloseEmailDetail();
        if (currentEmailGO != null) currentEmailGO.SetActive(false);

        unreadEmails--;
        UpdateBadge();

        spawner.OnEmailAnswered();
    }




}
