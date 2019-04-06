using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    public Transform playCamera;
    public float speed = 10.0f;
    public float turnSpeed = 100.0f;
    public Rigidbody rb;
    public LayerMask ground;
    public float groundDistance = 0.2f;
    public float jumpHeight = 2.0f;
    public float maxVelocityChange = 10.0f;
    public float gravity = 10.0f;

    private bool canJump = true;
    private bool isGrounded;
    private Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playCamera = playCamera.transform;
        groundCheck = transform.GetChild(0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

    }

    void FixedUpdate()
    {
        Vector3 dir = (playCamera.right * Input.GetAxis("Horizontal")) + (playCamera.forward * Input.GetAxis("Vertical"));

        dir.y = 0;

        /*if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {*/
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            /*rb.velocity = transform.forward * speed;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
        }*/

        if (isGrounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
