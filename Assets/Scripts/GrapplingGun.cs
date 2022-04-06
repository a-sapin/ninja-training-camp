using State_Machine;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")] public GrappleBase grappleRope;
    public PlayerManager playerRef;

    [Header("Layers Settings:")] [SerializeField]
    private bool grappleToAll;

    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")] public Camera mCamera;

    [Header("Transform Ref:")] public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")] public SpringJoint2D mSpringJoint2D;
    public Rigidbody2D mRigidbody;

    [Header("Rotation:")] private bool rotateOverTime = true;
    private float rotationSpeed = 4;

    [Header("Distance:")] [SerializeField] private bool hasMaxDistance;
    [SerializeField] private float maxDistnace = 20;

    [HideInInspector] public bool isGrapplingWithPad;
    [HideInInspector] public GameObject grappleTarget;
    private VFXManager vfxManager;

    private enum LaunchType
    {
        TransformLaunch,
        PhysicsLaunch
    }

    [Header("Launching:")] [SerializeField]
    private bool launchToPoint = true;

    [SerializeField] private LaunchType launchType = LaunchType.PhysicsLaunch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")] [SerializeField]
    private bool autoConfigureDistance;

    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        grappleRope.enabled = false;
        mSpringJoint2D.enabled = false;


        vfxManager = FindObjectOfType<VFXManager>();
    }

    private void Update()
    {
        if (playerRef.IsActionable() && playerRef.GetState()!=State.ladderClimb && playerRef.GetState()!=State.ladderGrab)
        {
            //event appeler lorsque le joueur utilise clic gauche pour la grapple
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerRef.CanGrapple())
            {
                SetGrapplePoint(Input.mousePosition);
            }
            else if ((Input.GetKey(KeyCode.Mouse0) || isGrapplingWithPad) &&
                     playerRef.CanGrapple()) //appeler lorsque le joueur maintien le clic gauche
            {
                Vector2 mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true); //utiliser pour avoir le vecteur joueur-clic

                if (launchToPoint && grappleRope.isGrappling)
                {
                    if (launchType == LaunchType.TransformLaunch)
                    {
                        Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                        Vector2 targetPos = grapplePoint - firePointDistnace;
                        gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) &&
                     playerRef.CanGrapple()) //appeler lorsque le joueur relache le clic gauche
            {
                StopGrappling();
            }
            else
            {
                Vector2 mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }
    }

    public void StopGrappling()
    {
        playerRef.SetBoolGrapple(false);
        isGrapplingWithPad = false;
        grappleRope.enabled = false;
        mSpringJoint2D.enabled = false;
        mRigidbody.gravityScale = 0; // return gravity to normal after grappling
    }

    public Vector2 GetNearestTargetPos(Vector2 origin)
    {
        float minDistance = float.PositiveInfinity;
        Vector2 nearest = Vector2.zero;
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("GrappleTarget"))
        {
            float newDist = Vector2.Distance(origin, target.transform.position);
            if (newDist < minDistance)
            {
                grappleTarget = target;
                minDistance = newDist;
                nearest = target.transform.position;
            }
        }

        return nearest;
    }

    //fait une rotation a l'objet. pourrait utiliser une fleche pour indiquer la direction
    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward),
                Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    //utiliser pour voir si le clic a toucher une cible a grapple (si negatif alors aucun grapple, si positif alors un grapple)
    public void SetGrapplePoint(Vector2 point, bool isPad = false)
    {
        isGrapplingWithPad = isPad;
        if (!isPad)
        {
            Vector2 distanceVector = mCamera.ScreenToWorldPoint(point) - gunPivot.position;
            if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
                if (hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
                {
                    if (Vector2.Distance(hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                    {
                        playerRef.SetBoolGrapple(true);
                        grapplePoint = hit.transform.position;
                        grappleTarget = hit.transform.gameObject;
                        grappleDistanceVector = grapplePoint - (Vector2) gunPivot.position;
                        grappleRope.enabled = true;
                        vfxManager.Play("Grapple");
                    }
                }
            }
        }
        else
        {
            playerRef.SetBoolGrapple(true);
            grapplePoint = point;
            grappleDistanceVector = grapplePoint - (Vector2) gunPivot.position;
            grappleRope.enabled = true;
            vfxManager.Play("Grapple");
        }
    }

//physique du grapple. utilisation du sprint joint pour le mouvement. Unity gere le mouvement et la collision avec le spring joint
    public void Grapple()
    {
        mRigidbody.gravityScale = 1f; // increase gravity during grapple for better swinging
        mSpringJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            mSpringJoint2D.distance = targetDistance;
            mSpringJoint2D.frequency = targetFrequncy;
        }

        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                mSpringJoint2D.autoConfigureDistance = true;
                mSpringJoint2D.frequency = 0;
            }

            mSpringJoint2D.connectedAnchor = grapplePoint;
            mSpringJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.PhysicsLaunch:
                    mSpringJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    mSpringJoint2D.distance = distanceVector.magnitude;
                    mSpringJoint2D.frequency = launchSpeed;
                    mSpringJoint2D.enabled = true;
                    break;
                case LaunchType.TransformLaunch:
                    mRigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

//gizmo pour voir le range du grapple
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }
}