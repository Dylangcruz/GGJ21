using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("here");

        if (other.gameObject.tag == "HidingSpot")
        {
            other.gameObject.GetComponent<Outline>().enabled = true;
            Debug.Log(other.gameObject.GetComponent<Outline>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("here");

        if (other.gameObject.tag == "HidingSpot")
        {
            other.gameObject.GetComponent<Outline>().enabled = false;
            Debug.Log(other.gameObject.GetComponent<Outline>());
        }
    }

}
