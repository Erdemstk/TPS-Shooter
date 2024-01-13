using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttackType : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] controllers;
    [SerializeField] private AnimationOverrider overrider;
    private static SetAttackType instance = null;

    public static SetAttackType Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SetAttackType>();
            }
            return instance;
        }
    }
    public void Set(int value)
    {
        overrider.SetAnimation(controllers[value]);
        
    }
}
