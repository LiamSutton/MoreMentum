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
    public bool currentlyDisplaying = false;
    public class CaptionOptions {
        public string captionText;
        public float time;

        public CaptionOptions() {

        }
            
        public CaptionOptions(string captionText, float time) {
            this.captionText = captionText;
            this.time = time;
        }
    }
    public IEnumerator ShowCaption(CaptionOptions options) {
        currentlyDisplaying = true;

        caption.SetText(options.captionText);

        panel.SetActive(true);

        yield return new WaitForSeconds(options.time);

        caption.SetText(string.Empty);

        panel.SetActive(false);

        currentlyDisplaying = false;
    }

    public void HandleCaption(CaptionOptions options) {
        if (currentlyDisplaying) {
            StopAllCoroutines();
        }
        StartCoroutine("ShowCaption", options);
    }
}
