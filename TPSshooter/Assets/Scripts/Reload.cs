using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    
    int magazineCap; // UI da göstermeyeceksin, reload yapýnca mevcut mermi miktarý þarjörün kapasitesi kadar.
    int totalBulletCap;// UI da göstermeyeceksin, arkada çalýþacak. // o silah için max taþýnabilir mermi miktarý
    int totalBulletAmount;
    int currentBulletCount;

    private static Reload instance = null;

    public static Reload Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Reload>();
            }
            return instance;
        }
    }
    void Start()
    {
        magazineCap = WeaponManager.Instance.currentWeapon.weaponType.magazineCapacity;
        totalBulletCap = WeaponManager.Instance.currentWeapon.weaponType.maxBulletCap;
        currentBulletCount = WeaponManager.Instance.currentWeapon.weaponType.currentBulletAmount;
    }


    void FixedUpdate()
    {
        
    }
    
    public void MagazineSystem()
    {
        if(totalBulletAmount >= magazineCap)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Reload");
            int newBulletAmount = totalBulletAmount - magazineCap;
            totalBulletAmount = newBulletAmount;
            currentBulletCount = magazineCap;
            Debug.Log($"totalBulletAmount: {totalBulletAmount}");
            Debug.Log($"currentBulletCount: {currentBulletCount}");


        }
        else
        {
            Debug.Log("if e girmedi");
        }
    }
}
