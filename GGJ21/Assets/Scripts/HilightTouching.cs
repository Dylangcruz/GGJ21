using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HilightTouching : MonoBehaviour
{
    public GameObject touchingObject;

    //Highlight touching
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            //make sure only one is being touched
            if (touchingObject != null)
            {
                GameObject tempObject = touchingObject;
                tempObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            }
            touchingObject = other.gameObject;
            other.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot" && touchingObject==other)
        {
            other.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Outline>().OutlineColor = Color.yellow;
        if(other==touchingObject){
            touchingObject=null;
        }
    }
}
