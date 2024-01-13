using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponSlot1;
    public Transform weaponSlot2;
    public Transform weaponSlot3;
    public Transform weaponDropSlot;
    public Weapon weapon1;
    public Weapon weapon2;
    public Weapon weapon3;
    public Weapon currentWeapon;
    public Weapon DefaultWeapon;
    public bool kusanilanSilah = false;
    //Rigidbody currentRb;

    private static WeaponManager instance = null;

    public static WeaponManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WeaponManager>();
            }
            return instance;
        }
    }
    
    private void Start()
    {
        EquipWeapon(DefaultWeapon);
        UI_Manager.Instance.ChangeWeaponUI(DefaultWeapon);
        currentWeapon = DefaultWeapon;
    }
    public void EquipWeapon(Weapon weapon)
    {
        /// Alýnan silahýn türüne göre yerleþmesi gereken slotlar.
        
        if (weapon.weaponType.automaticRifle)
        {
            if (weapon1 == null)
            {
                //currentWeapon.gameObject.SetActive(false);
                weapon1 = Instantiate(weapon.weaponType.weaponPrefab, weaponSlot1).GetComponent<Weapon>();
                currentWeapon = weapon1;
                

            }
            else
            {
                //slot deðiþimi kýsmý
                //eðer elinde automaticRiffle var ise sadece 1 adet þarjör alacak. Ancak Interact yaparsa ve yerdeki slah farklý ise ozaman mevcut silahý yerine yerdeki silahý
                //instantiate edecek ve currentWeapon bu yeni instantiate edilecek olan silah olacak.
               
                
                   
            }
               
        }
       
        if (weapon.weaponType.handGun)
        {
            if (weapon2 == null) //baþnagýçta eli boþ (hierarþideki slot 2 kýsmý)
            {
                
                if (currentWeapon) // startta default eline gelecek silah olmadýðý için if sorgusu  koyduk.
                {
                    
                    //currentWeapon.gameObject.SetActive(true);
                    
                }
                weapon2 = Instantiate(weapon.weaponType.weaponPrefab, weaponSlot2).GetComponent<Weapon>();
                currentWeapon = weapon2;

            }
            else
            {
                //currentWeapon.gameObject.SetActive(true);
                currentWeapon = weapon2;
                if(!currentWeapon.gameObject.activeSelf)
                {
                    //currentWeapon.gameObject.SetActive(true);
                }
                //currentWeapon = weapon2;
                //autoriffle ile ayný þekilde olacak. ama current weapon olarak atanmayacak.

            }

        }
        
        if (weapon.weaponType.projectile)
        {
            if (weapon3 == null)
            {

                weapon3 = Instantiate(weapon.weaponType.weaponPrefab, weaponSlot3).GetComponent<Weapon>();
            }
            else
            {
                //weaponslot3 dolu ise ve trigger a girdiði bomba ayný ise sadece bomba mikyarý 3 adet artacak. Baþka tarz bomba ise Interact ettiðinde
                //yenisi ile deðiþecek.


            }
        }   
       
    }

    public void DropWeapon()
    {

        //currentRb = currentWeapon.GetComponent<Rigidbody>();
        if(currentWeapon != null)
        {
            GameObject droppedWeapon = Instantiate(currentWeapon.weaponType.weaponPrefab, weaponDropSlot.position,Quaternion.Euler(90f,90f,90f));
            kusanilanSilah = true; // yerdeki silahtan ayýrt etmek için koydum.
            droppedWeapon.AddComponent<Rigidbody>();
            Rigidbody currentRb = droppedWeapon.GetComponent<Rigidbody>();
            currentRb.velocity = weaponDropSlot.forward * 2f;
            Destroy(currentWeapon.gameObject);
            Destroy(droppedWeapon,2f);
            currentWeapon.GetComponentInParent<Transform>();
            Debug.Log(currentWeapon.GetComponentInParent<Transform>().name);
            


        }
    }
   


}
