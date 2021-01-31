using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject touchingObject;
    public int bugAmount = 3;
    public KeyCode interactKey = KeyCode.E; //key used for interactions
    
    public AudioClip bonkSound;
    public AudioSource audioSrc;
    public int 

    // initial values
    void Start()
    {
        touchingObject = null;
        bugAmount = 3;

    }

    // Update is called once per frame
    void Update()
    {
        touchingObject = GetComponentInChildren<HilightTouching>().touchingObject;
        Debug.Log(touchingObject);
        if (this.gameObject.tag=="HiderPlayer"){
            HiderActions();
        }else{
            SeekerActions();
        }
       
    }

    private void HiderActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
                //retrieve old bug
            if(manager.getBug()){
                Debug.Log("Retrieved bug");
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                manager.setBug(false);
                bugAmount++;
            //hide new bug1
            }else if(bugAmount>0){
                Debug.Log("Planted bug");
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                manager.setBug(true);
                bugAmount--;
                }
            }
    }
    private void SeekerActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //destroy bug
            if(manager.getBug()){
                manager.setBug(false);
                Debug.Log("Destroyed bug");
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }else{
                Debug.Log("No bug found!");
                //Add some stall here
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "HiderPlayer" &&  other.gameObject.tag != "SeekerPlayer")
        {
            Debug.Log("Here!");
            audioSrc.PlayOneShot(bonkSound,10);
        }   
    }
}
