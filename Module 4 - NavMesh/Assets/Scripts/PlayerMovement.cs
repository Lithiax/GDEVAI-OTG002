using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += transform.forward * (z * speed) * Time.deltaTime;
        transform.position += transform.right * (x * speed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 15f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = 5f;
    }
}
