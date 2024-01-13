using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; 

    public AudioClip gunshotSound;
    public AudioClip hitSound;
    public AudioClip hurtSound;
    public AudioClip slotChangeSound;
    public AudioClip slotChangeFailSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþtiðinde yok olmasýný önlemek için
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGunshotSound()
    {
        PlaySound(gunshotSound);
    }

    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }

    public void PlayHurtSound()
    {
        PlaySound(hurtSound);
    }

    public void PlaySlotChangeSound()
    {
        PlaySound(slotChangeSound);
    }

    public void PlaySlotChangeFailSound()
    {
        PlaySound(slotChangeFailSound);
    }

    public void PlayNoAmmoSound()
    {
        PlaySound(noAmmoSound);
    }

    public void PlayReloadSound()
    {
        PlaySound(reloadSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
