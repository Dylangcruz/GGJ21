using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject touchingObject;
    private DirectorScript dScript;
    public int bugAmount;
    public KeyCode interactKey = KeyCode.E; //key used for interactions
    
    public AudioClip bonkSound, placedSound, foundSound;
    public AudioSource audioSrc;

    void Start()
    {
        dScript = GameObject.FindGameObjectWithTag("Director").GetComponent<DirectorScript>();
        bugAmount = dScript.initialBugs;
    }

    // Update is called once per frame
    void Update()
    {
        touchingObject = GetComponentInChildren<HighlightTouching>().touchingObject;
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
                // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                audioSrc.PlayOneShot(foundSound,5);
                manager.setBug(false);
                bugAmount++;
            //hide new bug
            }else if(bugAmount>0){
                Debug.Log("Planted bug");
                // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                audioSrc.PlayOneShot(placedSound,5);
                manager.setBug(true);
                bugAmount--;
                }
            }
            touchingObject=null;
    }
    private void SeekerActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //destroy bug
            if(manager.getBug()){
                manager.setBug(false);
                Debug.Log("Destroyed bug");
                dScript.foundBug();
                // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                audioSrc.PlayOneShot(foundSound,5);
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
            audioSrc.PlayOneShot(bonkSound,5);
        }   
    }
    public int getBugAmount(){
        return bugAmount;
    }
}
