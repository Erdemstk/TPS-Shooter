using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    private Animator animator;
    Rigidbody rigidbody;
    public float horizontalInput;
    public float verticalInput;
    Hit hit;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        hit = GetComponent<Hit>();
    }
    void Update()
    {
        animator.SetFloat("XAxis", Input.GetAxis("Horizontal"));
        animator.SetFloat("YAxis", Input.GetAxis("Vertical"));
        MouseCheck();
        WeaponInteractControl();
        WeaponReload();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);
        if(IsRunning && Input.GetKeyDown(KeyCode.LeftAlt))
            {
                animator.SetTrigger("Roll");
                
            }
           
        
        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( horizontalInput * targetMovingSpeed, verticalInput * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        //Animation_Control();
    }
    public void Animation_Control(int slotNumber)
    {
       if(slotNumber == 1) 
        {
            animator.SetInteger("WeaponSlot", 1);
        }
       else if(slotNumber == 2)
        {
            animator.SetInteger("WeaponSlot", 2);
        }
    }
    public void MouseCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ateş edildi");
            hit.Fire();
        }
    }
    public void WeaponInteractControl()
    {
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            WeaponManager.Instance.DropWeapon();
        }
    }
    public void WeaponReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload.Instance.MagazineSystem();
        }
    }
   
    
    
}