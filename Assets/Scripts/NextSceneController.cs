using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public float loadDelay = 3f;
    void Start()
    {
    }

    // Update is called once per frame
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Loading next scene");
            StartCoroutine("LoadNextScene");
        }
    }

    IEnumerator LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneIndex+1);
    }
}
