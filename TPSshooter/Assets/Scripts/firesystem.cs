using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firesystem : MonoBehaviour
{
    public Transform firepoint;
    public GameObject mermiPrefab;
    public float mermiH�z� = 10f;
    public AudioSource seskaynak;
    public float guncool;
    public float firetime;
    public ParticleSystem muzzleFlash;
    RaycastHit hit;
    private static firesystem instance = null;
    Vector3 bulletDirection = new Vector3(-1,0, 0);
    public static firesystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<firesystem>();
            }
            return instance;
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0) && Time.time > firetime + guncool)
        //{
        //    Ate�Et();
        //    firetime = Time.time;
        //}
    }

    public void Ate�Et()
    {
        GameObject mermi = Instantiate(mermiPrefab, firepoint.position, Quaternion.identity);
       

        //mermi.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        //Rigidbody rb = mermi.GetComponent<Rigidbody>();
        //rb.velocity = firepoint.transform.position + transform.forward * mermiH�z�; // BURADA de�i�iklik yapacaks�n. Mermi y�n� ile alakal�.


        Destroy(mermi, 2f); // Mermiyi bir s�re sonra yok et
        seskaynak.Play();
        muzzleFlash.Play();
        if (Physics.Raycast(firepoint.position, firepoint.forward, out hit))
        {
            // Vurulan nesnenin ad�n� debug log ile g�ster
            Debug.Log("Vurulan Nesne: " + hit.collider.gameObject.name);
        }

    }
    
   
     
    
}

