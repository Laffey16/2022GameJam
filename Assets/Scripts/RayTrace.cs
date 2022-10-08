using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayTrace : MonoBehaviour
{
    public float rayLength;
    public GameObject pppos;
    public GameObject cam;
    public static RayTrace instance;
    int floorHit = 0;
    public enum keytype { dud, global, room}

    bool HasKey;
    keytype currentKey;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        pppos = GameObject.FindGameObjectWithTag("Player");
        rayInteract();
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
                    }
                    else if(HasKey&& !hitGameObject.GetComponent<Renderer>().enabled)//PLACE KEY BACK ONLY ON CORRECT PLATFORM
                    {
                        HasKey = false;
                        hitGameObject.GetComponent<Renderer>().enabled = true;
                        //CHANGE KEYTYPE using keyname
                    }

                }
            }
            //Door
            if (hitGameObject.tag == "WIn")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("Win");
                
                    Debug.Log("win");
                }
            }
            //if (hitGameObject.tag == "Floor")
            //{
            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        ++floorHit;
            //        if (floorHit > 2)
            //        {
            //            hitGameObject.GetComponent<Renderer>().enabled = false;
            //        }
            //        else { }
               
                 
            //    }
            //}
            Debug.Log(hitGameObject.tag);
        }
    }
}
