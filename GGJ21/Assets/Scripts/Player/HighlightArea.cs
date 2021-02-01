using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightArea : MonoBehaviour
{
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
}
