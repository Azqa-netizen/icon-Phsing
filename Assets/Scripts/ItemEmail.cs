using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEmail : MonoBehaviour
{

   
    public Text emailText; 
    private string fullContent;
    private bool isPhishing;
    private EmailController emailUIController;
    private GameObject spawnerObj;

    
    public void Setup(string shortText, string fullText, bool phishingStatus, EmailController controller, GameObject spawnerRef)
    {
        if (emailText != null)
            emailText.text = shortText;

        fullContent = fullText;
        isPhishing = phishingStatus;
        emailUIController = controller;
        spawnerObj = spawnerRef;
    }

  
    public void OnClick()
    {
        if (emailUIController == null)
        {    
            return;
        }

        emailUIController.OpenEmailDetail(fullContent, isPhishing, this.gameObject);
    }
    void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnClick);
        }
    }
}
