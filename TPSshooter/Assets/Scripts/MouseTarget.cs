using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Collider currentGunCollider;
    public RaycastHit raycastHit;
    private static MouseTarget instance = null;

    public static MouseTarget Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MouseTarget>();
            }
            return instance;
        }
    }
    void Update()
    {
        
    }
    public void MouseTargetCheck()
    {
        currentGunCollider = WeaponManager.Instance.currentWeapon.GetComponent<Collider>();
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.Log(mainCamera.name);
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider != currentGunCollider)
            {
                transform.position = raycastHit.point;
                //Debug.Log("Mouse Traget" + transform.position);
                //Debug.DrawLine(ray.origin, ray.origin + ray.direction * 200, Color.red);
            }

        }
    }
}
