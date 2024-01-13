using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game_Mode : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    
    
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        MainMenu_UI.Instance.is_connected_sw(PhotonNetwork.IsConnected);
        
        
    }

    public void Play_Button()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log("OnJoinedLobby");
    }

    
    
    
    
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("Load_Level",RpcTarget.All);
        }
    }

    [PunRPC]
    void Load_Level()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("GamePlay_Scene");
        

        //StartCoroutine(Start_Yukleme_Process());
    }

    IEnumerator Start_Yukleme_Process()
    {
        MainMenu_UI.Instance.Render_Loading_Image(true);
        AsyncOperation process = SceneManager.LoadSceneAsync("GamePlay_Scene");
        while (true)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);

            if (process.isDone)
            {

                MainMenu_UI.Instance.Render_Loading_Image(!process.isDone);
            }
        }
    }
}
