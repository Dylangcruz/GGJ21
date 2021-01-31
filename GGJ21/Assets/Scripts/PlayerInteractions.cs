using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject touchingObject;
    private DirectorScript dScript;
    private int bugAmount;
    void Start()
    {
        dScript = GameObject.Find("Director").GetComponent<DirectorScript>();
        bugAmount = dScript.initialBugs;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag=="HiderPlayer"){
            HiderActions();
        }else{
            SeekerActions();
        }
    }

    private void HiderActions(){
        if(Input.GetKeyDown(KeyCode.E) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
                //retrieve old bug
            if(manager.getBug()){
                Debug.Log("Retrieved bug");
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                manager.setBug(false);
                bugAmount++;
            //hide new bug
            }else if(bugAmount>0){
                Debug.Log("Planted bug");
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.red;
                manager.setBug(true);
                bugAmount--;
                }
            }
    }
    private void SeekerActions(){
        if(Input.GetKeyDown(KeyCode.Q) && touchingObject!=null){
            HidingSpotManager manager = touchingObject.GetComponent<HidingSpotManager>();
            //destroy bug
            if(manager.getBug()){
                manager.setBug(false);
                Debug.Log("Destroyed bug");
                dScript.foundBug();
                touchingObject.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }else{
                Debug.Log("No bug found!");
                //Add some stall here
            }
        }
    }

  //Highlight near
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            other.gameObject.GetComponent<Outline>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            other.gameObject.GetComponent<Outline>().enabled = false;
        }
    }



//Highlight touching
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            //make sure only one is being touched
            if(touchingObject!=null){
                GameObject tempObject = touchingObject;
                tempObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            }
            touchingObject = other.gameObject;
            touchingObject.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            touchingObject.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "HidingSpot" && touchingObject!=null)
        {
            touchingObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            touchingObject=null;
        }
    }

    public int getBugAmount(){
        return bugAmount;
    }
}
