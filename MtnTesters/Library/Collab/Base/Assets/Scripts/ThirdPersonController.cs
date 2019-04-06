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
    public Vector3 moveVector;

    private bool isGrounded;
    private Transform groundCheck;
    private Vector3 _inputs = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playCamera = playCamera.transform;
        groundCheck = transform.GetChild(0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;
    }

    void FixedUpdate()
    {
        /*Vector3 dir = (playCamera.right * Input.GetAxis("Horizontal")) + (playCamera.forward * Input.GetAxis("Vertical"));

        dir.y = 0;*/

        /*rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);*/

        rb.MovePosition(rb.position + _inputs * speed * Time.fixedDeltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
        }
    }
}
