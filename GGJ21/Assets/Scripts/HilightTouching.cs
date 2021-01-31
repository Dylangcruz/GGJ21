using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HilightTouching : MonoBehaviour
{
    public GameObject touchingObject;
    // Start is called before the first frame update
    void Start()
    {
        touchingObject = null;
    }

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
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            other.GetComponent<Outline>().OutlineColor = Color.green;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot" && touchingObject != null)
        {
            touchingObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            touchingObject = null;
        }
    }
}
