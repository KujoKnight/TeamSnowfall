using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Tooltip("Main camera following the Player")]
    public Transform playCamera;
    [Tooltip("Player speed")]
    public float speed = 10.0f;
    //[Tooltip("")]
    public float turnSpeed = 100.0f;
    //[Tooltip("")]
    public float rotationSpeed = 2.0f;
    [Tooltip("Player rigidbody")]
    public Rigidbody rb;
    [Tooltip("Walkable area layer")]
    public LayerMask ground;
    [Tooltip("Player distance to ground")]
    public float groundDistance = 0.2f;
    [Tooltip("Player jump height")]
    public float jumpHeight = 5.0f;
    [Tooltip("Maximum change in velocity")]
    public float maxVelocityChange = 10.0f;
    [Tooltip("Gravity")]
    public float gravity = 5.0f;
    [Tooltip("Can the player move?")]
    public bool CanMove;
    [Tooltip("Player character model")]
    public GameObject playerModel;
    [Tooltip("Force")]
    public float force = 5;

    private Vector3 dir;
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

        playerModel.transform.rotation = Quaternion.Euler(0, playCamera.transform.eulerAngles.y, 0);
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            if (isGrounded)
            {
                // Calculate how fast the player should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                targetVelocity = playCamera.TransformDirection(targetVelocity);
                targetVelocity *= speed;

                // Apply a force that attempts to reach the target velocity
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
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            col.gameObject.SetActive(false);
            CarryOverData.playerScore++;
        }
    }

    //Flags the player as caught after it has collided with the npc
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = col.contacts[0].point - playerModel.transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            rb.AddForce(dir * force, ForceMode.Impulse);

            Debug.Log("Player Hit");
        }
    }
}
