using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject touchingObject;
    public int bugAmount = 3;

    // initial values
    void Start()
    {
        touchingObject = null;
        bugAmount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag=="HiderPlayer"){
            HiderActions();
        }else{
            SeekerActions();
        }
        if(touchingObject!=null){touchingObject.GetComponent<Outline>().OutlineColor = Color.green;}
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
        if(Input.GetKeyDown(KeyCode.Q) && touchingObject!=null){
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

}
