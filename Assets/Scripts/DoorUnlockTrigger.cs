using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    DoorController doorController;
    void Start()
    {
        doorController = door.GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            doorController.SendMessage("TriggerHide");
        }
    }
}
