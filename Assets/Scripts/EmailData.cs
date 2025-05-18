using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailData : MonoBehaviour
{
    public string sender;
    public string subject;
    public string content;
    public bool isPhishing;

    public EmailData(string sender, string subject, string content, bool isPhishing)
    {
        this.sender = sender;
        this.subject = subject;
        this.content = content;
        this.isPhishing = isPhishing;
    }
}
