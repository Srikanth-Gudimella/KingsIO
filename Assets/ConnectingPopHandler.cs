using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingPopHandler : MonoBehaviour
{
    public static ConnectingPopHandler Instance;
    public Text ConnectingText;
    public string displayTxt = "Connecting";
    int count = 0;
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    private void Start()
    {
        ConnectingText.text = displayTxt;
        //InvokeRepeating(nameof(ConnectingTextUpdate), 0.2f, 0.2f);
    }
    public void Open()
    {
        gameObject.SetActive(true);
        ConnectingText.text = displayTxt;
        CancelInvoke(nameof(ConnectingTextUpdate));
        InvokeRepeating(nameof(ConnectingTextUpdate), 0.2f, 0.2f);
    }
    void ConnectingTextUpdate()
    {
        count++;
        //for (int i = 0; i < count; i++)
        //{
            ConnectingText.text = ConnectingText.text + ".";
        //}
        if (count > 3)
        {
        ConnectingText.text = displayTxt;
            count = 0;
        }
        
    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
    private void Update()
    {
        
    }
}
