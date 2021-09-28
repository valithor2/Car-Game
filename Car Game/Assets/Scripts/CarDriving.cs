using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    public float moveSpeed;
    public float turnMultipiler;
    public float maxTurnSpeed;
    public float gravityMultiplier;
    public float turnSmoothing;
    public float slopeSmoothing;

    public Transform turnPivot;
    public Rigidbody sphereRb;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) {
            sphereRb.AddForce(turnPivot.forward * moveSpeed, ForceMode.Impulse);
        } else if (Input.GetKey(KeyCode.S)) {
            sphereRb.AddForce(turnPivot.forward * -moveSpeed, ForceMode.Impulse);
        }

        transform.position = sphereRb.position;

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, 2.0f, 1 << 6)) {
            transform.up = Vector3.Lerp(transform.up, hitInfo.normal, Time.deltaTime * slopeSmoothing);
            //transform.Rotate(0, transform.eulerAngles.y, 0);
        }

        float turnAmount = 0;
        if (Input.GetKey(KeyCode.D)) {
            if (sphereRb.velocity.magnitude != 0) {
                float turnSpeed = 1 / sphereRb.velocity.magnitude;
                if (turnSpeed > maxTurnSpeed) {
                    turnAmount = turnMultipiler * maxTurnSpeed;
                }
                else {
                    turnAmount = turnMultipiler * turnSpeed;
                }
            }
        } else if (Input.GetKey(KeyCode.A)) {
            if (sphereRb.velocity.magnitude != 0) {
                float turnSpeed = 1 / sphereRb.velocity.magnitude;
                if (turnSpeed > maxTurnSpeed) {
                    turnAmount = turnMultipiler * -maxTurnSpeed;
                }
                else {
                    turnAmount = turnMultipiler * -turnSpeed;
                }
            }
        }
        turnPivot.RotateAround(turnPivot.position, turnPivot.up, turnAmount * Time.deltaTime);
        //turnPivot.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        sphereRb.AddForce(-transform.up * gravityMultiplier);
    }
}
