using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform Spawn_Points;

    public GameObject This_player;
    public GameObject Other_player;
    
    [PunRPC]
    public GameObject Spawn_Single_Player(bool is_terrorist)
    {
        Vector3 spawn_position;
        Quaternion spawn_rotation;
        
        if (is_terrorist)
        {
            spawn_position = Spawn_Points.GetChild(0).position;
            spawn_rotation = Spawn_Points.GetChild(0).rotation;
        }
        else
        {
            spawn_position = Spawn_Points.GetChild(1).position;
            spawn_rotation = Spawn_Points.GetChild(1).rotation;
        }
        
         
        This_player = PhotonNetwork.Instantiate(PlayerPrefab.name, spawn_position, spawn_rotation ) as GameObject;
        
        
        #region 3D World Spawning

        

        Transform Camera_transform = Camera.main.transform;
        
        
        Camera_transform.parent = This_player.transform;

        Vector3 target_pos = This_player.GetComponent<PlayerController>().Camera_Pos.position;
        Camera_transform.position = target_pos;

        #endregion
        
        return This_player;
    }
}
