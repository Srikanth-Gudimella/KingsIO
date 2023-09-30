using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForOtherPlayersPop : MonoBehaviour
{
    public static WaitForOtherPlayersPop Instane;
    private void Awake()
    {
        Instane = this;
        gameObject.SetActive(false);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
