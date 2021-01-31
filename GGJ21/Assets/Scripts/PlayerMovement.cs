using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 5;
    public GameObject point;
    private PointActions pointScript;
    // Start is called before the first frame update
    void Start()
    {
        //point = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back  * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
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
    }
}
