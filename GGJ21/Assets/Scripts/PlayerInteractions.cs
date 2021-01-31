using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject touchingObject = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Actions();
        if(touchingObject!=null){touchingObject.GetComponent<Outline>().OutlineColor = Color.green;}
    }

    private void Actions(){
        if(Input.GetKey(KeyCode.E) && touchingObject!=null){
            Debug.Log("Planted");
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
