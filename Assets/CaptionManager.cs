using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CaptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public TMP_Text caption;
    void Awake()
    {
        // panel = GameObject.Find("Caption Panel");
        // caption = GameObject.Find("Caption Text").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public IEnumerator ShowCaption(string captionText, float time) {
        caption.SetText(captionText);
        panel.SetActive(true);
        yield return new WaitForSeconds(time);
        caption.SetText(string.Empty);
        panel.SetActive(false);
    } 
}
