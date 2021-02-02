using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderInteractions : MonoBehaviour
{
    public GameObject touchingObject;
    private DirectorScript dScript;
    public int bugAmount;
    public KeyCode interactKey = KeyCode.E; //key used for interactions
    public bool isSetUp;
    public GameObject bug;
    
    public AudioClip bonkSound, placedSound, foundSound;
    public AudioSource audioSrc;

    void Start()
    {
        isSetUp=true;
        dScript = GameObject.FindGameObjectWithTag("Director").GetComponent<DirectorScript>();
        bugAmount = dScript.initialBugs;
    }

    // Update is called once per frame
    void Update(){
        touchingObject = GetComponentInChildren<HighlightTouching>().touchingObject;
        HiderActions();
    }

    private void HiderActions(){
        if(Input.GetKeyDown(interactKey) && touchingObject!=null){
            if(isSetUp){
                setUpActions();
            }else{
                gameActions();
            }
        }
    }

    private void setUpActions(){
        HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //retrieve old bug
            if(manager.getBug()){
                // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                audioSrc.PlayOneShot(foundSound,5);
                manager.setBug(false);
                bugAmount++;
            //hide new bug
            }else if(bugAmount>0){
                // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                audioSrc.PlayOneShot(placedSound,5);
                manager.setBug(true);
                bugAmount--;
        }
    }

    private void gameActions(){
        HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //retrieve old bug
            if(manager.getBug()){
                if(bugAmount == 0){
                    bug.SetActive(true);
                    audioSrc.PlayOneShot(foundSound,5);
                    manager.setBug(false);
                    bugAmount=1;
                }else{
                    //Drop bug warning
                }
            //hide new bug
            }else{
                if(bugAmount==1){
                    // touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    bug.SetActive(false);
                    audioSrc.PlayOneShot(placedSound,5);
                    manager.setBug(true);
                    bugAmount=0;
                }else{
                    //no bugs available warning
                }
            }         
    }
}
