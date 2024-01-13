using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject bulletHolePrefab;
    public GameObject bulletPrefab;
    private Transform firePoint;
    public float distance;
    public AudioSource seskaynak;
    public ParticleSystem muzzleFlash;
    public GameObject bloodeffect;
    void Start()
    {
       //firePoint = WeaponManager.Instance.currentWeapon.GetComponentInChildren<Transform>();
       
    }

   
    void Update()
    {
        
    }
    public void Fire()
    {
        firePoint = WeaponManager.Instance.currentWeapon.gameObject.GetComponentInChildren<Transform>().GetChild(0);
        Debug.Log("Mermi cýktýgý nokta" + firePoint.name);
        MouseTarget.Instance.MouseTargetCheck();
        RaycastHit hit = MouseTarget.Instance.raycastHit;

        Vector3 hitPoint = hit.point;
        GameObject mermi = Instantiate(bulletPrefab, firePoint.position, Quaternion.EulerAngles(0, 0,90));
        BulletCountDown();
        seskaynak.Play();
        muzzleFlash.Play();
        
        if (!hit.collider.gameObject.CompareTag("Boundry") && !hit.collider.transform.root.CompareTag("Player"))
        {
            GameObject hole = Instantiate(bulletHolePrefab, hitPoint, Quaternion.identity);
            
            if (hit.collider.transform.rotation.eulerAngles.y >= 0 && hit.collider.transform.rotation.eulerAngles.y < 180)
            {
                hole.transform.position = new Vector3(hole.transform.position.x + 0.001f, hole.transform.position.y, hole.transform.position.z - 0.001f);

            }
            else
            {
                hole.transform.position = new Vector3(hole.transform.position.x - 0.001f, hole.transform.position.y, hole.transform.position.z + 0.001f);
            }
            hole.transform.transform.Rotate(hit.collider.transform.rotation.eulerAngles);
            Debug.Log("Collider açýlarý" + hit.collider.transform.rotation.eulerAngles);
            Destroy(hole, 5f);
        }


        if (hit.collider.transform.root.CompareTag("Player") && hit.collider.gameObject.TryGetComponent(out MeshCollider head_collider))
        {
            //HS
            if (hit.collider.gameObject.CompareTag("Head"))
            {
                Debug.Log("Head HS");
                Instantiate(bloodeffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.collider.gameObject.CompareTag("Body"))
            {
                Debug.Log("Body BS");
                Instantiate(bloodeffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            
        }
    }
    public void BulletCountDown()
    {
        int bulletCount = WeaponManager.Instance.currentWeapon.weaponType.currentBulletAmount;
        Debug.Log($"BulletCountDown: {bulletCount}");
        if (bulletCount > 1) // debug da 1 adet eksik sayýyor. oyüzden > 1
        {
            Debug.Log("bulletcount çalýþtý");
            bulletCount -= 1;
            WeaponManager.Instance.currentWeapon.weaponType.currentBulletAmount = bulletCount;
        }
    }
}
