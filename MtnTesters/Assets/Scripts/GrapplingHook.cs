using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    //  Public Variables
    [Tooltip("Layer to Target")]
    public LayerMask TargetLayer;
    [Tooltip("The max distance at which the Player can grapple")]
    public int MaxDist;
    [Tooltip("Speed that the player is being pulled")]
    public float speed = 10;
    [Tooltip("Distance from grapple target to stop grappling")]
    public float stopGrapple = 1;
    [Tooltip("'Are you being pulled?'")]
    public bool IsFlying;
    [Tooltip("Location of Target point")]
    public Vector3 loc;
    [Tooltip("Player Camera")]
    public Camera cam;
    [Tooltip("Origin of 'Rope'")]
    public Transform hand;
    [Tooltip("Player Controller")]
    public ThirdPersonController TPC;
    [Tooltip("'Rope'")]
    public LineRenderer LR;
    public RaycastHit hit;
    public Rigidbody rb;

    //public float opac;

    //  Use this for initialization
    void Start()
    {
        rb = TPC.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        TPC.CanMove = true;
        TPC.GetComponent<Rigidbody>().useGravity = true;
        //opac = TPC.GetComponent<Renderer>().material.color.a;

    }

    //  Update is called once per frame
    void Update()
    {
        //  Change opacity of player when looking upward
        /*if (cam.transform.position.z < -4)
        {
            Debug.Log(opac);
            opac = cam.transform.position.z/-4;
        }
        else
        {
            opac = 1;
        }*/

        //  Pull when holding left click (And aiming at target)
        if (Input.GetButtonDown("Fire1"))
            LocateSpot();

        //  If being pulled, pull to target and prevent movement interference
        if (IsFlying)
            Flying();

        //  If not left clicking, end pull
        if (Input.GetButtonUp("Fire1") && IsFlying)
        {
            IsFlying = false;
            TPC.CanMove = true;
            LR.enabled = false;
            TPC.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    //  Check for target
    public void LocateSpot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, MaxDist, TargetLayer))
        {
            IsFlying = true;
            loc = hit.point;
            TPC.CanMove = false;
            LR.enabled = true;
            LR.SetPosition(1, loc);
        }
    }

    //  Weeeeeeeee (Pull to target point)
    public void Flying()
    {
        transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
        rb.AddForce(cam.transform.forward * speed, ForceMode.Acceleration);
        LR.SetPosition(0, hand.position);
        TPC.GetComponent<Rigidbody>().useGravity = false;

        if (Vector3.Distance(transform.position, loc) < stopGrapple)
        {
            IsFlying = false;
            TPC.CanMove = true;
            rb.useGravity = true;
            rb.velocity = transform.position * 0;
        }
    }
}
