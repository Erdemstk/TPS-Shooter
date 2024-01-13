using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    RaycastHit hit;
    Vector3 hitPoint;
    Rigidbody rb;
    public float bulletSpeed = 0.01f;
    Vector3 target;
    void Start()
    {
        target = MouseTarget.Instance.raycastHit.point;
        hit = MouseTarget.Instance.raycastHit;
        rb = GetComponent<Rigidbody>();
        hitPoint = hit.point;
    }
     void FixedUpdate()
    {
        rb.velocity =/* transform.position + */target * bulletSpeed;
        
        //bulletPrefab.transform.Translate(transform.position + hitPoint * bulletSpeed *Time.deltaTime);
        //Debug.DrawLine(transform.position, transform.position + hit.point * 200, Color.red);
    }
    // Update is called once per frame
     void OnCollisionEnter(Collision collision)
    {
        Destroy(bulletPrefab);
    }
}
