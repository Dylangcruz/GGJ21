using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 up = Vector3.zero,
            right = new Vector3(0, 90, 0),
            down = new Vector3(0, 180, 0),
            left = new Vector3(0, 270, 0),
            currentDirection = Vector3.zero;

    Vector3 nextPos, destination, direction;
    public float speed = 5f;

    private void Start()
    {
        currentDirection = up;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        int horizontalValue = 0;
        int verticalValue = 0;
        if (Input.GetKey(KeyCode.W))
        {
            verticalValue += 1;
            currentDirection = up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            verticalValue -= 1;
            currentDirection = down;

        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalValue -= 1;
            currentDirection = left ;

        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalValue += 1;
            currentDirection = right;

        }



        //redirect model
        transform.localEulerAngles = currentDirection;
        //move

        Vector3 movementVector = (Vector3.right * horizontalValue +
                                  Vector3.forward * verticalValue).normalized;
        GetComponent<Rigidbody>().velocity = movementVector * speed;
        
        Debug.Log(GetComponent<Rigidbody>().velocity);

    }
    

}
