using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderInteractions : MonoBehaviour
{
    public GameObject touchingObject;
    private DirectorScript dScript;
    public int secretAmount = 0;
    public KeyCode interactKey = KeyCode.E; //key used for interactions
    public bool isSetUp;
    public GameObject secret;
    
    public AudioClip bonkSound, placedSound, foundSound;
    public AudioSource audioSrc;

    void Start()
    {
        secret = transform.GetChild(3).gameObject;
        isSetUp=true;
        dScript = GameObject.FindGameObjectWithTag("Director").GetComponent<DirectorScript>();
        secretAmount = dScript.initialSecrets;
    }

    // Update is called once per frame
    void Update(){
        touchingObject = GetComponentInChildren<HighlightTouching>().touchingObject;
        if(secretAmount>0){
            secret.SetActive(true);
        }else{
            secret.SetActive(false);
        }
        hiderActions();
    }

//These could both be one function but made two for readability
    private void hiderActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //retrieve old secret
            if(manager.getSecret()){
                //Hider can have only have 1 secret during game
                if(!isSetUp){
                    if(secretAmount==0){
                        audioSrc.PlayOneShot(foundSound,5);
                        manager.setSecret(false);
                        secretAmount=1;
                    }else{
                        //Play "cant do that" sound, bonk standin
                        audioSrc.PlayOneShot(bonkSound,5);
                    }
                //Hider can have many secrets during setup
                }else{
                    audioSrc.PlayOneShot(foundSound,5);
                    manager.setSecret(false);
                    secretAmount++;
                }
            //hide new secret
            }else if(secretAmount>0){
                audioSrc.PlayOneShot(placedSound,5);
                manager.setSecret(true);
                secretAmount--;
            }
        }
    }
}
