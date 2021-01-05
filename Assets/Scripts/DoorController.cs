using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;
    Vector3 endPointPosition;

    public AudioClip clip;
    // AudioSource source;
    public bool shouldHide = false;
    public bool isElavator = false;

    public bool clipPlayed = false;
    public float speed = 0.5f;
    void Start()
    {
        endPoint = transform.Find("End Point");
        endPointPosition = endPoint.transform.position;
        startPoint = transform;
    //     source = GetComponent<AudioSource>();
    //     clip = source.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldHide)
        {
            Hide();
        }

        if (!isElavator)
        {
            if (transform.position.y == endPointPosition.y)
            {
                Destroy(this.gameObject, 0.5f);
            }
        }
    }

    void Hide()
    {
        if (!clipPlayed) {
            clipPlayed = true;
            AudioSource.PlayClipAtPoint(clip, startPoint.position);
        }
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPointPosition, step);
    }

    void TriggerHide()
    {
        shouldHide = true;
    }

    void TriggerClose()
    {

    }
}
