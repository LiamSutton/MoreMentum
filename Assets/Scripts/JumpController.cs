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

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (isGrounded && canJump)
        //     {
        //         canJump = false;
        //         isGrounded = false;
        //         Debug.Log("JUMPING at: " + Time.time.ToString());
        //         StartCoroutine(Jump());
        //     }
        // }
    }

    private IEnumerator Jump()
    {
        rb.AddForce(Vector2.up * jumpForce * 1.5f);
        rb.AddForce(Vector3.up * jumpForce * 0.5f);
        Vector3 velocity = rb.velocity;
        if (rb.velocity.y < 0.5f)
        {
            rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(velocity.x, velocity.y / 2, velocity.z);
        }
        yield return new WaitForSeconds(jumpCooldown);
        
        ResetJump();
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void ResetGrounded() {
        isGrounded = true;
    }

    // private void Jump()
    // {
    //     if (isGrounded && isReadyToJump)
    //     {
    //         isReadyToJump = false;


    //         rb.AddForce(Vector2.up * jumpForce * 1.5f);
    //         rb.AddForce(normalVector * jumpForce * 0.5f);

    //         Vector3 velocity = rb.velocity;
    //         if (rb.velocity.y < 0.5f)
    //         {
    //             rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
    //         }
    //         else if (rb.velocity.y > 0)
    //         {
    //             rb.velocity = new Vector3(velocity.x, velocity.y / 2, velocity.z);
    //         }

    //         Invoke(nameof(ResetJump), jumpCooldown);
    //     }
    // }
}
