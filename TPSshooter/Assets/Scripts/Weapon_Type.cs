using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Weapon Type", menuName = "Weapon")]
public class Weapon_Type : ScriptableObject
{
    
    public Sprite weaponSprite;
    public GameObject weaponPrefab;
    public string typeName;
    public int magazineCapacity;
    public int maxBulletCap;
    public int currentTotalBulletAmount;
    public int currentBulletAmount;
    public bool handGun;
    public bool automaticRifle;
    public bool projectile;
    public int damage;
    public Transform rayCastPoint;

}
