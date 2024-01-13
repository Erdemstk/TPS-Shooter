using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Image heavyGunImage;
    public Image handGunImage;
    public Image projectileImage;
    
    public Image currentImage;
   
    public Text totalBulletAmount_UI;
    public Text bulletAmount_UI;
    
    Vector2 currentScale = Vector2.one;
    public FirstPersonMovement controller;

    private static UI_Manager instance = null;

    public static UI_Manager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UI_Manager>();
            }
            return instance;
        }
    }
    void Start()
    {
        Game_Starting();
        currentImage.transform.localScale = currentScale;
        Debug.Log("Scale deðeri" + currentScale);
        controller = new FirstPersonMovement();
        currentImage.sprite = WeaponManager.Instance.DefaultWeapon.Wsprite;
        
    }
    void Update()
    {
        
        UI_Contollers();
        
    }
    private void FixedUpdate()
    {
        bulletAmount_UI.text = WeaponManager.Instance.currentWeapon.weaponType.currentBulletAmount.ToString();
        totalBulletAmount_UI.text = WeaponManager.Instance.currentWeapon.weaponType.currentTotalBulletAmount.ToString();
    }
    void UI_Contollers()
    {
        //HeavyGun
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if(WeaponManager.Instance.weapon1 != null)
            {

                ChangeWeaponUI(WeaponManager.Instance.weapon1);
                WeaponManager.Instance.weaponSlot1.gameObject.SetActive(true);
                WeaponManager.Instance.weaponSlot2.gameObject.SetActive(false);
            }
            else
            {

            }  
        }
        //HandGun
        if (Input.GetKey(KeyCode.Alpha2))
        {

            if (WeaponManager.Instance.weapon2 != null)
            {
                //WeaponManager.Instance.EquipWeapon(WeaponManager.Instance.weapon2);
                ChangeWeaponUI(WeaponManager.Instance.weapon2);
                WeaponManager.Instance.weaponSlot2.gameObject.SetActive(true);
                WeaponManager.Instance.weaponSlot1.gameObject.SetActive(false);
            }
            else
            {

            }

        }
        //Projectile
        if (Input.GetKey(KeyCode.Alpha3))
        {

            if (WeaponManager.Instance.weapon3 != null)
            {

                ChangeWeaponUI(WeaponManager.Instance.weapon3);
            }
            else
            {

            }
        }
    }
    void Game_Starting()
    {
        handGunImage.sprite = WeaponManager.Instance.weapon2.Wsprite;
        handGunImage.enabled = true;
        currentImage.sprite = handGunImage.sprite;
        currentImage.enabled = true;
        
    }
    public void ChangeWeaponUI(Weapon weapon)
    {
      
        bulletAmount_UI.text = weapon.weaponType.currentBulletAmount.ToString();
        totalBulletAmount_UI.text = weapon.weaponType.currentTotalBulletAmount.ToString();
        //currentImage.sprite = weapon.weaponType.weaponSprite;
        if ((weapon.weaponSlotType - 1) == 0)
        {
            heavyGunImage.sprite = WeaponManager.Instance.weapon1.Wsprite;
            currentImage.sprite = heavyGunImage.sprite;
            heavyGunImage.enabled = true;
            handGunImage.enabled = false;
            projectileImage.enabled = false;
            currentImage.transform.localScale = currentScale;
            //controller.Animation_Control(1);
            SetAttackType.Instance.Set(1);//animasyon geçiþleri için
            
           


        }
        else if ((weapon.weaponSlotType - 1) == 1)
        {
            handGunImage.sprite = WeaponManager.Instance.weapon2.Wsprite;
            currentImage.sprite = handGunImage.sprite;
            heavyGunImage.enabled = false;
            handGunImage.enabled = true;
            projectileImage.enabled = false;
            currentImage.transform.localScale = currentScale;
            //controller.Animation_Control(2);
            SetAttackType.Instance.Set(0);


        }
        else if ((weapon.weaponSlotType - 1) == 2)
        {
            projectileImage.sprite = WeaponManager.Instance.weapon3.Wsprite;
            currentImage.sprite = projectileImage.sprite;
            Debug.Log(currentImage.sprite.name);
            heavyGunImage.enabled = false;
            handGunImage.enabled = false;
            projectileImage.enabled = true;
            currentImage.transform.localScale = new Vector3(1.5f, 1.5f, 0);
            
        }
        WeaponManager.Instance.currentWeapon = weapon;
        
    }
    
    
    



}
