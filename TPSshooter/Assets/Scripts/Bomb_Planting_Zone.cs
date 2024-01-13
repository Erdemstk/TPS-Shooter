using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Planting_Zone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("terrorist"))
        {
            GameManager1.Instance.is_terrorist_in_bombing_zone = true;
            UImanager.Instance.Initalize_Terrorist_Entered_Bomb_Zone();
        }
        else if (other.CompareTag("anti-terrorist") )
        {
            GameManager1.Instance.is_anti_terrorist_in_bombing_zone = true;
            UImanager.Instance.Initalize_Terrorist_Entered_Bomb_Zone();

        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("terrorist"))
        {
            GameManager1.Instance.is_terrorist_in_bombing_zone = false;
        }
        else if (other.CompareTag("anti-terrorist") )
        {
            GameManager1.Instance.is_anti_terrorist_in_bombing_zone = false;
        }
    }
}
