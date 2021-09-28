using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float movSmoothing;
    public float rotSmoothing;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, movSmoothing * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotSmoothing * Time.deltaTime);
    }
}
