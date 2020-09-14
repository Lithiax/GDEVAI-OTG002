using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 8;

    void LateUpdate()
    {

        //mouse position ray cast code from https://www.youtube.com/watch?v=lkDGk3TjsIE 
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, this.transform.position);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(pointToLook);
        }

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput * moveSpeed;

        if(Vector3.Distance(cameraRay.GetPoint(rayLength), this.transform.position) > 0.5)
            transform.Translate(moveVelocity * Time.deltaTime);
    }
}
