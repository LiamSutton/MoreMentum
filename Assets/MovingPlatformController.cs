using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    Rigidbody rb;
    public Transform startPoint;
    public Transform endPoint;
    private Vector3 startPointVec;
    private Vector3 endPointVec;

    public float speed;

    public bool isTravelingToEndPoint;

    void Awake()
    {
        endPoint = transform.Find("EndPosition");
        startPoint = transform;
        startPointVec = transform.position;
        endPointVec = endPoint.position;

        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (isTravelingToEndPoint)
        {
            if (transform.position != endPointVec)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPointVec, step);
            }
            else
            {
                isTravelingToEndPoint = false;
            }
        }
        else
        {
            if (transform.position != startPointVec)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPointVec, step);
                
            }
            else
            {
                isTravelingToEndPoint = true;
            }
        }
    }
}
// transform.position = Vector3.MoveTowards(transform.position, startPointVec, step);