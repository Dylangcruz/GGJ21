using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerInteractions : MonoBehaviour
{
    public GameObject touchingObject;
    public GameObject   touchingPlayer;
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
        touchingPlayer = GetComponentInChildren<HighlightTouching>().touchingPlayer;
        SeekerActions();
       
    }

    private void SeekerActions(){
        if(Input.GetKeyDown(interactKey)){
            if (touchingObject!=null){
                HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
                //destroy bug
                if(manager.getSecret()){
                    manager.setSecret(false);
                    dScript.foundSecret();
                    audioSrc.PlayOneShot(foundSound,5);
                }else{
                    //object empty, maybe add a penalty?
                    audioSrc.PlayOneShot(bonkSound,5);
                }
            } //Having an else if prioritizes object, having two if's does both if both are being touched. Design choice?
            if (touchingPlayer!=null && touchingPlayer.tag == "HiderPlayer"){
                HiderInteractions tPlayer = touchingPlayer.GetComponentInParent<HiderInteractions>();
                if (tPlayer.secretAmount!=0){
                    tPlayer.secretAmount=0;
                    audioSrc.PlayOneShot(foundSound,5);
                    dScript.foundSecret();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "HidingSpot"){
            audioSrc.PlayOneShot(bonkSound,1);
        }   
    }
}
