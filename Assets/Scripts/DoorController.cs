using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Transform endPoint;
    Vector3 endPointPosition;
    public bool shouldHide = false;
    public float speed = 0.5f;
    void Start()
    {
        endPoint = transform.Find("End Point");   
        endPointPosition = endPoint.transform.position;
        Debug.Log(endPointPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldHide) {
            Debug.Log("HIDING DOOR");
            Hide();
        }

        if (transform.position.y == endPointPosition.y) {
            Destroy(this.gameObject, 0.5f);
        }
    }

    void Hide() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPointPosition, step);
    }

    void H() {
        shouldHide = true;
    }
}
