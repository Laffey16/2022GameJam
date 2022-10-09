using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayTrace : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioManager manager;
    public float rayLength;
    public GameObject pppos;
    public GameObject cam;
    public static RayTrace instance;
    int floorHit = 0;
    public enum keytype { dud, global, room1,room2 ,room3, room4}
    int doorsOpened; 

    bool HasKey;
    bool finished;
    float finishTimer;
    keytype currentKey;
    // Start is called before the first frame update
    private void Start()
    {
        HasKey = false;
        finished = false;
        doorsOpened = 1;
        manager.PlaySoundOnce(clips[3]);//getting over it refernce
    }
    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            finishTimer += Time.deltaTime;
            if (finishTimer > clips[6].length)
            {
                Debug.Log("quit");
                Application.Quit();
            }
        }
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        pppos = GameObject.FindGameObjectWithTag("Player");
        if (!finished)
        {
            rayInteract();
        }
       // Debug.Log(currentCar);
    }
    void rayInteract()
    {
        Vector3 playerPosition = cam.transform.position;
        Vector3 forwardVector = cam.transform.forward;

        Ray rayCast = new Ray(playerPosition, forwardVector);
        RaycastHit rayHitTester;

        Vector3 rayEnd = forwardVector * rayLength;
        Debug.DrawLine(playerPosition, rayEnd);

        bool hit = Physics.Raycast(rayCast, out rayHitTester, rayLength);
        if (hit)
        {
            GameObject hitGameObject = rayHitTester.transform.gameObject;

            //KEY
            if (hitGameObject.tag == "Key")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!HasKey && hitGameObject.GetComponent<Renderer>().enabled)//PICK UP KEY
                    {
                        HasKey = true;
                        hitGameObject.GetComponent<Renderer>().enabled = false;
                        //CHANGE KEYTYPE using keyname
                        char keyname = hitGameObject.name[2];
                        switch (keyname) { 
                            case '1': currentKey = keytype.room1;// repeat for other rooms when added 
                                break;
                            case '2':
                                currentKey = keytype.room2;// repeat for other rooms when added 
                                break;
                            case '3':
                                currentKey = keytype.room3;// repeat for other rooms when added 
                                break;
                            case '4':
                                currentKey = keytype.room4;// repeat for other rooms when added 
                                break;
                            case 'G': currentKey = keytype.global; 
                                break;
                            default: currentKey = keytype.dud; 
                                break;
                           }
                        Debug.Log(currentKey.ToString());
                        //////////////////////////////////////////////////RANDOMLY play either row5col  
                        int rand = Random.Range(1, 100);
                        if (rand>80)
                        {
                            manager.PlaySoundOnce(clips[0]);//57
                            
                        }else if (rand>90)
                        {
                            
                            manager.PlaySoundOnce(clips[8]);//wrong key (a lite bit of trolling)
                        }
                    }
                    else if(HasKey&& !hitGameObject.GetComponent<Renderer>().enabled)//PLACE KEY BACK ONLY ON CORRECT PLATFORM
                    {
                        HasKey = false;
                        hitGameObject.GetComponent<Renderer>().enabled = true;
                        if (Random.Range(0,100)>95)
                        {
                            manager.PlaySoundOnce(clips[5]);//testers
                        }
                        //CHANGE KEYTYPE using keyname //ACTUALLY NVM DONT THINK WELL need this
                    }

                }
            }


            //Door
            if (hitGameObject.tag == "Door")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    string roomname = "room" + doorsOpened.ToString();
                    if (HasKey && (currentKey == keytype.global || currentKey.ToString() == roomname))
                    {
                        //OPEN DOOR 
                        hitGameObject.active = false;
                        doorsOpened++;
                        if (doorsOpened==5)
                        {
                            manager.PlaySoundOnce(clips[1]);//
                            return;
                        }else if (doorsOpened==6)
                        {
                            //VICTORY STUFF
                            manager.PlaySoundOnce(clips[6]);//
                            finished = true;
                            return;
                        }

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////ADD DOOR OPEN AUDIO
                        Debug.Log("open");
                        manager.PlaySoundOnce(clips[4]);//door opened
                        
                        return;
                    }
                    else { 
                        Debug.Log("nice try");//////////////////////////////////////////////////////////////////////////WRONG KEY AUDIO
                        manager.PlaySoundOnce(clips[7]);//wrong key no cap
                        return;
                    }
                }
            }
      
            Debug.Log(hitGameObject.tag);
        }
    }

}
