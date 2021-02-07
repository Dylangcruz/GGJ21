using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public bool isGrounded;

    //If touching ground, "is grounded". Not grounded if leaves
    void OnCollisionStay(Collision other){if (other.gameObject.tag == "Ground") {isGrounded = true;}}
    void OnCollisionExit(Collision other){if (other.gameObject.tag == "Ground") {isGrounded = false;}}
    private void Start(){
        isGrounded = false;
        currentDirection = up;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded){Move();};
    }

    void Move()
    {
        int horizontalValue = 0;
        int verticalValue = 0;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            verticalValue += 1;
            currentDirection = up;
        }
        if (Input.GetKey(KeyCode.A))

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            verticalValue -= 1;
            currentDirection = down;

        }
        if (Input.GetKey(KeyCode.S))
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.back  * speed * Time.deltaTime;
            horizontalValue -= 1;
            currentDirection = left ;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            horizontalValue += 1;
            currentDirection = right;

        }



        //redirect model
        transform.localEulerAngles = currentDirection;

        Vector3 movementVector = (Vector3.right * horizontalValue +
                                  Vector3.forward * verticalValue).normalized;
        GetComponent<Rigidbody>().velocity = movementVector * speed;

    }


}
