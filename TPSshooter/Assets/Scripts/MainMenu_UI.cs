using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    private static MainMenu_UI instance = null;

    public static MainMenu_UI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MainMenu_UI>();
            }
            return instance;
        }

    }
    
    public Image Is_Connected_Image;
    public GameObject Loading_Image;


    private void Awake()
    {
        is_connected_sw(PhotonNetwork.IsConnected);
    }

    public void is_connected_sw(bool is_connected)
    {
        if (is_connected)
        {
            Is_Connected_Image.color = Color.green;
        }
        else
        {
            Is_Connected_Image.color = Color.red;
        }
    }

    public void Render_Loading_Image(bool is_activating)
    {
        Loading_Image.SetActive(is_activating);
    }
}
