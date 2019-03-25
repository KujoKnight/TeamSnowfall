using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    //  Public Variables
    [Header("Grappling Time!")]
    [Tooltip("Layer to Target")]
    public LayerMask TargetLayer;
    [Tooltip("The max distance at which the Player can grapple")]
    public int MaxDist;
    [Tooltip("Speed that the player is being pulled")]
    public float speed = 10;
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

    //  Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        TPC.CanMove = true;
        TPC.GetComponent<Rigidbody>().useGravity = true;
	}
	
	//  Update is called once per frame
	void Update () {
        //  Pull when holding left click (And aiming at target)
        if (Input.GetMouseButtonDown(0))
            LocateSpot();

        //  If being pulled, pull to target and prevent movement interference
        if (IsFlying)
            Flying();

        //  If not left clicking, end pull
        if (Input.GetMouseButtonUp(0) && IsFlying)
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
        LR.SetPosition(0, hand.position);
        TPC.GetComponent<Rigidbody>().useGravity = false;

        if (Vector3.Distance(transform.position, loc) < 1.25f)
        {
           IsFlying = false;
           TPC.CanMove = true;
           TPC.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
