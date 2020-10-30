using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{
    public Transform playerCamera;
    public float dashForce = 75f;
    public float dashDuration = 0.25f;

    public float dashCooldown = 0.5f;
    public AudioClip dashAudio;
    public ParticleSystem dashParticles;
    private Rigidbody rb;

    public bool isGrounded;

    public bool isReadyToDash;

    public Image dashUI;
    public Color dashEnabled;
    public Color dashDisabled;
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
        dashUI.color = dashDisabled;
        // dashUI.enabled = false;   
        isReadyToDash = false;
        rb.velocity = Vector3.zero;
        dashParticles.Play();
        rb.AddForce(playerCamera.transform.forward * dashForce, ForceMode.VelocityChange);
        GetComponent<AudioSource>().PlayOneShot(dashAudio, 0.8f);
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);

        // If the player is grounded after waiting the cooldown duration, chances are they performed
        // a ground dash, so re-enable dash
          if (isGrounded) {
            ResetDash();
        }
    }

    private void ResetDash() {
        isReadyToDash = true;
        dashUI.color = dashEnabled;
    }

    private void ResetGrounded() {
        isGrounded = true;
    }

    private void LeftGround() {
        isGrounded = false;
    }
}
