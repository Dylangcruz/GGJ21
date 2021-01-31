using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointActions : MonoBehaviour
{
    public GameObject owner;

    public void Bury(GameObject player){
        transform.parent = null;
        //Do bury animation
        //Bury item underground
        Vector3 cPosition = transform.position;
        cPosition.y = -5; //temporary
        transform.position = cPosition;
        //set owner to burier for score
        owner = player;
    }
    public void DigUp(){
        //Delete BurySpace
        //Do Dig Up animation
        owner = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
