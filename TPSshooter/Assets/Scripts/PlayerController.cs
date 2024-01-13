using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]public Animator animator;
    private PhotonView PV;
    
    CharacterController characterController;
    public GameObject bomb;
    public Transform Camera_Pos;

    //public bool groundedPlayer;
    //public float jumpHeight = 1.0f;
    //private Vector3 playerVelocity;
    //private float gravityValue = -9.81f;

    private static PlayerController instance = null;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    #region Animation Bools

    public bool kk= false;
    
    public bool is_W_pressed = false;
    public bool is_S_pressed = false;
    public bool is_A_pressed = false;
    public bool is_D_pressed = false;
    

    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        PV = GetComponent<PhotonView>();
        animator = FindObjectOfType<Animator>();
    }

    void Start()
    {

        UImanager.Instance.transform.GetChild(UImanager.Instance.transform.childCount-1).GetComponent<Button>().onClick.AddListener(Button_method);

    }
    
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    void Update()
    {
        if (PV.IsMine)
        {
            Animation_Control();
            //groundedPlayer = characterController.isGrounded;
            //if (groundedPlayer && playerVelocity.y < 0)
            //{
            //    playerVelocity.y = 0f;
            //}

            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            //Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            characterController.SimpleMove(forward * curSpeed);
            //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //characterController.Move(move * Time.deltaTime * speed);
            //if (move != Vector3.zero)
            //{
            //    gameObject.transform.forward = move;
            //}
            //if (Input.GetButtonDown("Jump") && groundedPlayer)
            //{
            //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //}
            //playerVelocity.y += gravityValue * Time.deltaTime;
            //characterController.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            //PV.RPC("Animation_Control", RpcTarget.Others);
        }

        if (PV.IsMine)
        {
            if (kk)
            {
                Debug.Log("kk is true");
            }
            else
            {
                Debug.Log("kk !!!!!!!");
            }
        }
        
        
    }
    public void Place_Bomb()
    {
        Vector3 playerPosition = transform.position + new Vector3(0,0.5f,0); // bomba prefaba uygun �ekilde instantiate olmad��� i�in d�zeltiyorum.
        Vector3 playerForward = transform.forward;
        float offset = 2.0f; // Bomban�n oyuncudan uzakl�k mesafesi

        Vector3 spawnPosition = playerPosition + playerForward * offset;
        Quaternion spawnRotation = Quaternion.Euler(270, 0, 0); 

        GameObject newObj = Instantiate(bomb, spawnPosition, spawnRotation);
        Destroy(newObj, GameManager1.Instance.BombExplosionTimer +1f);
    }
    
    [PunRPC]
    void Animation_Control()
    {
        Debug.Log("Animation_Control");
        if (PV.IsMine)
        {
            Debug.Log("PV.IsMine W :"+ is_W_pressed);
            if (Input.GetKeyDown(KeyCode.W))
            {
                is_W_pressed = true;
                kk = true;
            }
            else
            {
                kk = false;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                is_W_pressed = false;

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                is_S_pressed = true;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                is_S_pressed = false;

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                is_A_pressed = true;

            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                is_A_pressed = false;

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                is_D_pressed = true;

            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                
                is_D_pressed = false;

            } 
            Debug.Log("PV.IsMine W :"+ is_W_pressed);
            GameManager1.Instance.owning_player.GetComponent<PlayerController>().animator.SetBool("IsRunningForward", is_W_pressed);

            GameManager1.Instance.owning_player.GetComponent<PlayerController>().animator.SetBool("IsRunningBackward", is_S_pressed);

            GameManager1.Instance.owning_player.GetComponent<PlayerController>().animator.SetBool("IsRunningLeft", is_A_pressed);

            GameManager1.Instance.owning_player.GetComponent<PlayerController>().animator.SetBool("IsRunningRight", is_D_pressed);
            
        }

    }

    

    public void Button_method()
    {
        if (kk)
        {
            PV.RPC("RPC_Button_method", RpcTarget.Others,0);
        }
        else
        {
            PV.RPC("RPC_Button_method", RpcTarget.Others,1);
        }
    }
    
    [PunRPC]
    void RPC_Button_method(int bool_param)
    {
        if (bool_param == 1)
        {
            kk = true;
        }
        else
        {
            kk = false;
        }
    }
}
