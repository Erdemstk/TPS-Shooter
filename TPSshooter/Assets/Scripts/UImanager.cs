using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    private static UImanager instance = null;

    public static UImanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UImanager>();
            }
            return instance;
        }
    }

    public Image Bomb_Planting_Loading_Image;
    public Image Bomb_Defusing_Loading_Image;
    public Button Bomb_Button;
    

    public TextMeshProUGUI CountdownText;
    [Space] [Space] 
    public Sprite Bomba_Coz_Sprite;
    public Sprite Bomba_Kur_Sprite;
    
    

    //private bool isButtonHeld = false;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private Coroutine loading_couritine;
    private Coroutine bomb_countdown_couritine;


    private void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        
    }

    private void Update()
    {
        
        {
            //Check if the left Mouse button is clicked
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;
    
                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();
    
                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);
    
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.name == Bomb_Button.name && !GameManager1.Instance.is_bomb_planted && GameManager1.Instance.is_terrorist_in_bombing_zone 
                        && GameManager1.Instance.owning_player.Asker.is_terrorist) 
                    {
                        Start_Bomb_Planting();
                    }
                    else if (result.gameObject.name != Bomb_Button.name && GameManager1.Instance.owning_player.Asker.is_terrorist)
                    {
                        Stop_Bomb_Planting();
                    }


                    //anti terrorist
                    if (result.gameObject.name == Bomb_Button.name && GameManager1.Instance.is_bomb_planted &&
                        GameManager1.Instance.is_anti_terrorist_in_bombing_zone &&
                        !GameManager1.Instance.owning_player.Asker.is_terrorist)
                    {
                        Start_Bomb_Defusing();
                    }
                    else if (result.gameObject.name != Bomb_Button.name && !GameManager1.Instance.owning_player.Asker.is_terrorist)
                    {
                        Stop_Bomb_Defusing();
                    }
                    
                }
                
                if (results.Count == 0 && GameManager1.Instance.owning_player.Asker.is_terrorist)
                {
                    Stop_Bomb_Planting();
                }
                if (results.Count == 0 && !GameManager1.Instance.owning_player.Asker.is_terrorist)
                {
                    Stop_Bomb_Defusing();
                }
            }
            else
            {
                if (GameManager1.Instance.owning_player.Asker.is_terrorist)
                {
                    Stop_Bomb_Planting();
                }
                else if (!GameManager1.Instance.owning_player.Asker.is_terrorist)
                {
                    Stop_Bomb_Defusing();
                }
               
            }
        }
        
    }

    public void askerolustur_button()
    {
        if(EventSystem.current.currentSelectedGameObject.CompareTag("UIasker düsman"))
        {
            GameObject go = GameManager1.Instance.Spawner.Spawn_Single_Player(true);
           go.tag = "terrorist";

           go.GetComponent<Asker_Gameobject>().Asker = new Asker();
           go.GetComponent<Asker_Gameobject>().Asker.is_terrorist = true;

           GameManager1.Instance.owning_player = go.GetComponent<Asker_Gameobject>();
           
           // Referans doldurmak icin sahne bekletmesi yapalim
           GameManager1.Instance.other_player = go.GetComponent<Asker_Gameobject>();  // for other client
        }
        else if (EventSystem.current.currentSelectedGameObject.CompareTag("UIaskerdost"))
        {
            GameObject go = GameManager1.Instance.Spawner.Spawn_Single_Player(false);
            go.tag = "anti-terrorist";
            
            go.GetComponent<Asker_Gameobject>().Asker = new Asker();
            go.GetComponent<Asker_Gameobject>().Asker.is_terrorist = false;
            
            GameManager1.Instance.owning_player = go.GetComponent<Asker_Gameobject>();
            GameManager1.Instance.other_player = go.GetComponent<Asker_Gameobject>();  // for other client
        }
    }

    public void bombabutton_func()
    {
        if (GameManager1.Instance.owning_player.Asker.is_terrorist)
        {
            GameManager1.Instance.owning_player.Asker.bomba_kur();
        }
        else
        {
            GameManager1.Instance.owning_player.Asker.bomba_coz();
        }
    }

    public void Initalize_Terrorist_Entered_Bomb_Zone()
    {
        Bomb_Button.gameObject.SetActive(true);
        if (GameManager1.Instance.is_bomb_planted && GameManager1.Instance.is_anti_terrorist_in_bombing_zone)//anti ( cozme ) 
        {
            //renkleri sprite'lar ile degistir
            Bomb_Button.GetComponent<Image>().color = Color.blue;
            Bomb_Button.interactable = true;
        }
        else if (!GameManager1.Instance.is_bomb_planted && GameManager1.Instance.is_terrorist_in_bombing_zone)//terror ( kurma )
        {
            //renkleri sprite'lar ile degistir
            Bomb_Button.GetComponent<Image>().color = Color.red;
            Bomb_Button.interactable = true;
        }
    }
    
    
    
    public IEnumerator Start_Bomb_Planting_IEnumerator(int bombing_time)
    {
        
        if (!Bomb_Planting_Loading_Image.gameObject.activeSelf)
        {
            Bomb_Planting_Loading_Image.gameObject.SetActive(true);
        }
        Bomb_Planting_Loading_Image.fillAmount = 0f;
        
        float elapsed_time = 0f;

        while (elapsed_time < bombing_time)
        {
            Bomb_Planting_Loading_Image.fillAmount += Time.fixedDeltaTime / bombing_time; 
            elapsed_time += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        // Bomba kurma metodu BURAYA !!!!

        PlayerController.Instance.Place_Bomb();
        Bomb_Button.gameObject.SetActive(false);
        Bomb_Planting_Loading_Image.gameObject.SetActive(false);
        Debug.Log("BOMB HAS BEEN PLANTED");

        if (GameManager1.Instance.owning_player.Asker.is_terrorist)
        {
            GameManager1.Instance.is_bomb_planted = true;
            Countdown(GameManager1.Instance.BombExplosionTimer);
        }
        else
        {
            GameManager1.Instance.is_bomb_planted = false;
        }
    }

    public IEnumerator Start_Bomb_Defusing_IEnumerator(int defusing_time)
    {//Bomb_Defusing_Loading_Image
        if (!Bomb_Defusing_Loading_Image.gameObject.activeSelf)
        {
            Bomb_Defusing_Loading_Image.gameObject.SetActive(true);
        }
        Bomb_Defusing_Loading_Image.fillAmount = 0f;
        
        float elapsed_time = 0f;

        while (elapsed_time < defusing_time)
        {
            Bomb_Defusing_Loading_Image.fillAmount += Time.fixedDeltaTime / defusing_time; 
            elapsed_time += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Bomb_Button.gameObject.SetActive(false);
        Bomb_Defusing_Loading_Image.gameObject.SetActive(false);
        Debug.Log("BOMB HAS BEEN DEFUSED");

        if (GameManager1.Instance.owning_player.Asker.is_terrorist)
        {
            GameManager1.Instance.is_bomb_planted = true;
        }
        else
        {
            GameManager1.Instance.is_bomb_planted = false;
            StopCoroutine(bomb_countdown_couritine);
            bomb_countdown_couritine = null;
            Debug.Log("ANTI KAZANDI");
        }
    }

    private void Start_Bomb_Planting()
    {
        if (GameManager1.Instance.is_terrorist_in_bombing_zone)
        {
            if (!Bomb_Planting_Loading_Image.gameObject.activeSelf)
            {
                Bomb_Planting_Loading_Image.gameObject.SetActive(true);
            }
            if (loading_couritine == null)
            {
                loading_couritine = StartCoroutine(Start_Bomb_Planting_IEnumerator(5));
            }
    
            PlayerController controller = GameManager1.Instance.owning_player.GetComponent<PlayerController>();
            if (!controller.animator.GetBool("IsPressing_Bomb_Button"))
            {
                controller.animator.SetBool("IsPressing_Bomb_Button", true);
            }
            
            if (!controller.animator.GetBool("Is_Terrorist"))
            {
                controller.animator.SetBool("Is_Terrorist", true);
            }
            
        }
    }

    private void Start_Bomb_Defusing()
    {
        Debug.Log("Start_Bomb_Defusing");
        if (GameManager1.Instance.is_anti_terrorist_in_bombing_zone)
        {
            if (!Bomb_Defusing_Loading_Image.gameObject.activeSelf)
            {
                Bomb_Defusing_Loading_Image.gameObject.SetActive(true);
            }
            if (loading_couritine == null)
            {
                loading_couritine = StartCoroutine(Start_Bomb_Defusing_IEnumerator(5));
            }
    
            PlayerController controller = GameManager1.Instance.owning_player.GetComponent<PlayerController>();
            if (!controller.animator.GetBool("IsPressing_Bomb_Button"))
            {
                controller.animator.SetBool("IsPressing_Bomb_Button", true);
            }
            if (!controller.animator.GetBool("Is_Terrorist"))
            {
                controller.animator.SetBool("Is_Terrorist", false);
            }
        }
    }

    private void Stop_Bomb_Planting()
    {
        if (loading_couritine != null)
        {
            StopCoroutine(loading_couritine);
            loading_couritine = null;
        }

        if (Bomb_Planting_Loading_Image.gameObject.activeSelf)
        {
            Bomb_Planting_Loading_Image.gameObject.SetActive(false);
        }
                
        PlayerController controller = GameManager1.Instance.owning_player.GetComponent<PlayerController>();
        if (controller.animator.GetBool("IsPressing_Bomb_Button"))
        {
            controller.animator.SetBool("IsPressing_Bomb_Button", false);
        }
    }
    
    private void Stop_Bomb_Defusing()
    {
        if (loading_couritine != null)
        {
            StopCoroutine(loading_couritine);
            loading_couritine = null;
        }

        if (Bomb_Defusing_Loading_Image.gameObject.activeSelf)
        {
            Bomb_Defusing_Loading_Image.gameObject.SetActive(false);
        }
                
        PlayerController controller = GameManager1.Instance.owning_player.GetComponent<PlayerController>();
        if (controller.animator.GetBool("IsPressing_Bomb_Button"))
        {
            controller.animator.SetBool("IsPressing_Bomb_Button", false);
        }
    }

    private void Countdown(int time)
    {
        if (!CountdownText.transform.parent.gameObject.activeSelf)
        {
            CountdownText.transform.parent.gameObject.SetActive(true);
            
        }
        
        bomb_countdown_couritine = StartCoroutine(Countdown_IEnumerator(time));
    }

    public IEnumerator Countdown_IEnumerator(int time)
    {
        while (time > 0)
        {
            CountdownText.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        bomba1.Instance.Explosion(); //sıkıntılıydı
        Debug.Log("TERROR KAZANDI");
        bomb_countdown_couritine = null;
        CountdownText.transform.parent.gameObject.SetActive(false);
    }
    
}
