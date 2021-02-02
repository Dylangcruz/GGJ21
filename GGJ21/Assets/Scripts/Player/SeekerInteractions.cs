using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerInteractions : MonoBehaviour
{
    public GameObject touchingObject;
    private DirectorScript dScript;
    public KeyCode interactKey = KeyCode.Space; //key used for interactions
    
    public AudioClip bonkSound, placedSound, foundSound;
    public AudioSource audioSrc;

    void Start(){
        dScript = GameObject.FindGameObjectWithTag("Director").GetComponent<DirectorScript>();
    }

    // Update is called once per frame
    void Update(){
        touchingObject = GetComponentInChildren<HighlightTouching>().touchingObject;
        Debug.Log(touchingObject);
        SeekerActions();
       
    }

    private void SeekerActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //destroy bug
            if(manager.getBug()){
                manager.setBug(false);
                dScript.foundBug();
                // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                audioSrc.PlayOneShot(foundSound,5);
            //didn't find bug
            }else{
                //Add some stall here
            }
        }
    }
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "HidingSpot"){
            audioSrc.PlayOneShot(bonkSound,1);
        }   
    }
}
