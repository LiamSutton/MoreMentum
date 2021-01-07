using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        // Load game end scene
        Debug.Log("Thankyou for playing <3");
        SceneManager.LoadScene(5);
    }
}
