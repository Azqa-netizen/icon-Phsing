using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("Task Settings")]
    public int totalTasks = 20;
    public int completedTasks = 0;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;

    [Header("UI References")]
    public Text taskText;
    public GameObject endPanel; // Panel UI yang muncul saat semua task selesai
    public Text finalResultText; // Teks untuk menampilkan hasil akhir (benar/salah)

    void Start()
    {
        UpdateUI();
        endPanel.SetActive(false); // Pastikan panel akhir tidak aktif di awal
    }

    // Dipanggil setiap kali player menjawab email
    public void ReportAnswer(bool isCorrect)
    {
        completedTasks++;

        if (isCorrect)
        {
            correctAnswers++;
        }
        else
        {
            wrongAnswers++;
        }

        UpdateUI();

        if (completedTasks >= totalTasks)
        {
            ShowEndPanel();
        }
    }

    void UpdateUI()
    {
        taskText.text = $"EMAIL PROCESSED: {completedTasks}/{totalTasks}";
    }

    void ShowEndPanel()
    {
        endPanel.SetActive(true);
        finalResultText.text = $" CorrectAnswer: {correctAnswers}\n WrongAnswer: {wrongAnswers}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Pastikan scene ini ada di Build Settings
    }
}
