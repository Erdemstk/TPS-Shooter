using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    private static GameManager1 instance = null;
    public static GameManager1 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager1>();
            }
            return instance;
        }
    }
    
    
    public Spawner Spawner;
    public Asker_Gameobject owning_player;
    public Asker_Gameobject other_player;
    
    public bomba1 bomb;
    public PlayerController playerController;


    public int BombExplosionTimer;
    public bool is_bomb_planted = false;


    public bool is_terrorist_in_bombing_zone = false;
    public bool is_anti_terrorist_in_bombing_zone = false;

    public void StartBomb_planting()
    {
        Debug.Log("StartBomb_planting");
        //StartCoroutine(UImanager.Instance.Start_Bomb_Planting(5));
        UImanager.Instance.Bomb_Planting_Loading_Image.gameObject.SetActive(true);
    }
    
    
}
