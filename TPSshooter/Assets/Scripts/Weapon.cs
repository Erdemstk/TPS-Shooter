using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Weapon_Type weaponType; // kuþanýlan weapon

    string typeName;
    public int magCapacity;
    int maxMagAmount;
    int currentMagAmount;
    bool autoRifle;
    public Sprite Wsprite;
    public int weaponSlotType;
    public Transform cameraTransform;
    public float rotationSpeed = 1;

    private static Weapon instance = null;

    public static Weapon Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Weapon>();
            }
            return instance;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        WeaponManager weaponManager = other.GetComponent<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.EquipWeapon(this);
            UI_Manager.Instance.ChangeWeaponUI(this);
            if (weaponSlotType < WeaponManager.Instance.currentWeapon.weaponSlotType) 
            {
                weaponManager.EquipWeapon(this);
                UI_Manager.Instance.ChangeWeaponUI(this);
            }
            if (!WeaponManager.Instance.kusanilanSilah)
                Destroy(gameObject);
        }
        
      
      
    }
    void Start()
    {
        //Start_Info();
        Wsprite = weaponType.weaponSprite;
    }
    void Update()
    {
        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    } 
    void Start_Info()
    {

        string typeName = weaponType.typeName;
        int magCapacity = weaponType.magazineCapacity;
        int maxMagAmount = weaponType.maxBulletCap;
        bool autoRifle = weaponType.automaticRifle;
        int currentMagAmount = weaponType.currentBulletAmount;
    }
   
}


