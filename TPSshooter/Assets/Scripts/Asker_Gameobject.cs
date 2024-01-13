using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asker
{ 
    public bool is_terrorist;
    
    protected int healt;
    protected int damage;
    
    public void hasar_al(int hasarAl)
    {
        healt -= hasarAl;
    }
    
    public void hasar_ver(int hasarVer , Asker asker)
    {
        
        asker.hasar_al(hasarVer);
    }
    
    
    public void bomba_kur()
    {
        if (!GameManager1.Instance.is_bomb_planted)
        {
            // Gorselleri aktiflestir
            GameManager1.Instance.StartBomb_planting();
            
            Debug.Log("bomba_kur :"+ GameManager1.Instance.name);
        }
    }
    public void bomba_coz()
    {
        if (GameManager1.Instance.is_bomb_planted)
        {
            GameManager1.Instance.StartBomb_planting();
            
            Debug.Log("bomba_coz :"+ GameManager1.Instance.name);
            // Gorselleri aktiflestir
        }
    }
    
}





public class Asker_Gameobject : MonoBehaviour
{
    public Asker Asker;

}
