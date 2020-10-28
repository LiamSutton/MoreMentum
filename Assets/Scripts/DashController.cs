using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public Transform playerCamera;
    public float dashForce = 75f;
    public float dashDuration = 0.25f;

    public float dashCooldown = 0.5f;
    public AudioClip dashAudio;
    private Rigidbody rb;

    public bool isReadyToDash;
    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isReadyToDash) {
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        
        rb.velocity = Vector3.zero;
        rb.AddForce(playerCamera.transform.forward * dashForce, ForceMode.VelocityChange);
        GetComponent<AudioSource>().PlayOneShot(dashAudio, 0.8f);
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector3.zero;

        isReadyToDash = false;
        yield return new WaitForSeconds(dashCooldown);
    }

    private void ResetDash() {
        isReadyToDash = true;
    }
}
