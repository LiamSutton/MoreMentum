using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    private Rigidbody rb;

    public bool canJump;
    public float jumpForce = 550f;
    public float jumpCooldown = 0.25f;
    public bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && canJump)
            {
                Jump();
            }
        }
    }

    public void Jump() {
        if (isGrounded && canJump) {
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce * 1.5f);

            Vector3 velocity = rb.velocity;
            if (rb.velocity.y < 0.5f)
            {
                rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(velocity.x, velocity.y / 2, velocity.z);
            }
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }
    
    private void LeftGround() {
        isGrounded = false;
    }
    private void ResetGrounded() {
        isGrounded = true;
    }
}
