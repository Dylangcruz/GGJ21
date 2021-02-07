using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject touchingObject;
    private DirectorScript dScript;
    public int secretAmount;
    public KeyCode interactKey = KeyCode.E; //key used for interactions
    
    public AudioClip bonkSound, placedSound, foundSound;
    public AudioSource audioSrc;

    void Start()
    {
        dScript = GameObject.FindGameObjectWithTag("Director").GetComponent<DirectorScript>();
        secretAmount = dScript.initialSecrets;
    }

    // Update is called once per frame
    void Update()
    {
        touchingObject = GetComponentInChildren<HighlightTouching>().touchingObject;
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
            if(manager.getSecret()){
                // Debug.Log("Retrieved bug");
                audioSrc.PlayOneShot(foundSound,5);
                manager.setSecret(false);
                secretAmount++;
            //hide new bug
            }else if(secretAmount>0){
                // Debug.Log("Planted bug");
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                audioSrc.PlayOneShot(placedSound,5);
                manager.setSecret(true);
                secretAmount--;
                }
            }
            touchingObject=null;
    }
    private void SeekerActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //destroy bug
            if(manager.getSecret()){
                manager.setSecret(false);
                // Debug.Log("Destroyed bug");
                dScript.foundSecret();
                audioSrc.PlayOneShot(foundSound,5);
            }else{
                // Debug.Log("No bug found!");
                //Add some stall here
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "HiderPlayer" &&  other.gameObject.tag != "SeekerPlayer")
        {
            // Debug.Log("Here!");
            audioSrc.PlayOneShot(bonkSound,5);
        }   
    }
    public int getSecretAmount(){
        return secretAmount;
    }
}
