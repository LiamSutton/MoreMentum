using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        // Load game end scene
        Debug.Log("Thankyou for playing <3");
    }
}
