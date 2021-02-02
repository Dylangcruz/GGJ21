using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTouching : MonoBehaviour
{
    public GameObject touchingObject;

    //Highlight touching
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "HidingSpot"){
            //make sure only one is being touched
            if (touchingObject != null){
                GameObject tempObject = touchingObject;
                tempObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            }
            touchingObject = other.gameObject;
            other.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    //make sure other player doesnt paint yellow over green if still in contact
    private void OnTriggerStay(Collider other){
        if (other.gameObject.tag == "HidingSpot"){
            touchingObject=other.gameObject;
            other.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "HidingSpot"){
            other.GetComponent<Outline>().OutlineColor = Color.yellow;
            touchingObject=null;
        }
    }
}
