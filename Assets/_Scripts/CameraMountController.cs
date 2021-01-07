using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMountController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform currentMount;
    public float stepSpeed = 0.1f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentMount.position, stepSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, stepSpeed);
    }

    public void SetMount(Transform newMount) {
        currentMount = newMount;
    }
}
