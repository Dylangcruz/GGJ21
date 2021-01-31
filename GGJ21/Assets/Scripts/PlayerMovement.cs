using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode upKey = KeyCode.W; //upKey
    public KeyCode downKey = KeyCode.S; //upKey
    public KeyCode leftKey = KeyCode.A; //upKey
    public KeyCode rightKey = KeyCode.D; //upKey


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        int horizontalValue = 0;
        int verticalValue = 0;


        if (Input.GetKey(upKey))
        {
            verticalValue += 1;

        }

        if (Input.GetKey(downKey))
        {
            verticalValue -= 1;


        }
        if (Input.GetKey(leftKey))
        {
            horizontalValue -= 1;


        }
        if (Input.GetKey(rightKey))
        {
            horizontalValue += 1;


        }
<<<<<<< HEAD
        //bury or dig
        if (Input.GetKey(KeyCode.E))
        {
            if (point!=null){
                //Do bury if holding a point or 
                pointScript = point.GetComponent<PointActions>();
                pointScript.Bury(this.gameObject);
            }else{
                //search
                //If point is found, set point as the found point and do dig up
                //point.transform.parent = this.transform;
            }
        }
=======

      
        //move

        Vector3 movementVector = (Vector3.right * horizontalValue +
                                  Vector3.forward * verticalValue).normalized;

        GetComponent<Rigidbody>().velocity = movementVector * speed;
        
        Debug.Log(GetComponent<Rigidbody>().velocity);
        
        //rotate model
        if (gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            transform.forward = gameObject.GetComponent<Rigidbody>().velocity;
        }

>>>>>>> befb368f54c10e0bdf39b0fd4801693e5ff8fb45
    }


}
