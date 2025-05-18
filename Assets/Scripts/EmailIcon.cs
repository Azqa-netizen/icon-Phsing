using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailIcon : MonoBehaviour
{
    public EmailController emailUIController;
    private string fullContent;
    private bool isPhishing;
    private EmailSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string content, bool phishing, EmailSpawner parentSpawner)
    {
        fullContent = content;
        isPhishing = phishing;
        spawner = parentSpawner;

        emailUIController = FindObjectOfType<EmailController>();
    }

    public void OnClick()
    {
        emailUIController.OpenEmailDetail(fullContent, isPhishing, spawner.gameObject);
        gameObject.SetActive(false); 
    }
}
