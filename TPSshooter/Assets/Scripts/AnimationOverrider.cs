using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrider : MonoBehaviour
{
    Animator animator;
    private static AnimationOverrider instance = null;

    public static AnimationOverrider Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AnimationOverrider>();
            }
            return instance;
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(AnimatorOverrideController overrideController)
    {
        animator.runtimeAnimatorController = overrideController;
        
        
    }
}
