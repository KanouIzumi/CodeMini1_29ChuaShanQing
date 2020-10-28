using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speed = 10.0f;
    float zLimit = 20f;
    float xLimit = 10f;
    float yLimit = 0.5f;
    float Limit1 = 4.9f;
    float Limit2 = 9.9f;
    float Limit3 = 10.1f;



    bool isOnFloorA = false;
    //float DoubleJump = 0f;
    float SpacePress = 0f;

    float gravityModifier = 2.0f;
    private RaycastHit hit;



    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

       // Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < Limit3)
        {
            //in PlaneA
            if (transform.position.z < -Limit2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -Limit2);
            }

            else if (transform.position.z > zLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            }


            if (transform.position.x < -xLimit)
            {
                transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            }

            else if (transform.position.x > xLimit)
            {
                transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            }




            if (transform.position.x < -Limit1 && transform.position.z > Limit2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Limit2);
            }

            else if (transform.position.x > Limit1 && transform.position.z > Limit2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Limit2);
            }
        }

        else if (transform.position.z > Limit3)
        {
            //In PlaneB
            if (transform.position.x > Limit1)
            {
                transform.position = new Vector3(Limit1, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -Limit1)
            {
                transform.position = new Vector3(-Limit1, transform.position.y, transform.position.z);
            }
            if ( transform.position.z > zLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1))
            { // can jump
                playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }

        /*
        DoubleSpaceJump();
        */
    }
    
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            isOnFloorA = true;
            //SpacePress = 0;

            Debug.Log("PlaneA");
        }
        else if (collision.gameObject.CompareTag("PlaneB"))
        {
            isOnFloorA = false;
            Debug.Log("PlaneB");
        }
    }
    

    /*
    private void DoubleSpaceJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SpacePress < 2)
            {
                playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }

            SpacePress++;
        }
    }
    */
}

