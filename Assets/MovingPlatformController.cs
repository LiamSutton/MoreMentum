using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    private Vector3 startPointVec;
    private Vector3 endPointVec;

    public float speed = 0.5f;

    public bool isTravelingToEndPoint = true;

    void Awake() {
        endPoint = transform.Find("End Point");
        Debug.Log(endPoint != null);
        startPoint = transform;
        startPointVec = transform.position;
        endPointVec = endPoint.position;
        Debug.Log(endPoint.ToString());
    }
    void Update()
    {
        if (transform.position.x == endPoint.position.x) {
            isTravelingToEndPoint = false;
        }

        if (transform.position.x == startPoint.position.x) {
            isTravelingToEndPoint = true;
        }

         float step = speed * Time.deltaTime;
        if (isTravelingToEndPoint) {
            transform.position = Vector3.MoveTowards(transform.position, endPointVec, step);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, startPointVec, step);
        }
        
       
        
    }
}
