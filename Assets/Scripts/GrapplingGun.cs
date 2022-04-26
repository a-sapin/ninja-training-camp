using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")] public GrappleBase grappleRope;
    public PlayerManager playerRef;

    [Header("Layers Settings:")] [SerializeField]
    private bool grappleToAll = false;
    [SerializeField] LayerMask grappleTargetLayer;
    [SerializeField] LayerMask blockGrappleLayer;

    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")] public Camera m_camera;

    [Header("Transform Ref:")] public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")] public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")] private bool rotateOverTime = true;
    private float rotationSpeed = 4;

    [Header("Distance:")] [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    [HideInInspector] public bool isGrapplingWithPad = false;
    [HideInInspector] public GameObject grappleTarget;
    private VFXManager vfxManager;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")] [SerializeField]
    private bool launchToPoint = true;

    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")] [SerializeField]
    private bool autoConfigureDistance = false;

    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;


        vfxManager = FindObjectOfType<VFXManager>();
    }

    private void Update()
    {
        GetBestTargetPos(transform.position);
        ReadInputAndActivateGrapple(Time.deltaTime);
    }

    private void ReadInputAndActivateGrapple(float delta)
    {
        //event appeler lorsque le joueur utilise clic gauche pour la grapple
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isGrapplingWithPad && playerRef.CanGrapple())
        {
            SetGrapplePoint(Input.mousePosition);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && !isGrapplingWithPad 
            && playerRef.CanGrapple()) //appeler lorsque le joueur maintien le clic gauche
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true); //utiliser pour avoir le vecteur joueur-clic

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, delta * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && !isGrapplingWithPad
           && playerRef.CanGrapple()) //appeler lorsque le joueur relache le clic gauche
        {
            StopGrappling();
        }
        else if (Input.GetAxis("Grapple") > 0 && !isGrapplingWithPad && playerRef.CanGrapple())
        {
            Vector2 nearestTarget = GetBestTargetPos(transform.position);

            if (Vector2.Distance(nearestTarget, transform.position) < maxDistnace)
            {
                //LANCER LE GRAPPIN
                Debug.Log("GrapplingGun.cs || Grappling");
                SetGrapplePoint(nearestTarget, true);
            }
        }
        else if (Input.GetAxis("Grapple") <= 0 && isGrapplingWithPad)
        {
            StopGrappling();
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    public void StopGrappling()
    {
        playerRef.SetBoolGrapple(false);
        isGrapplingWithPad = false;
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 0; // return gravity to normal after grappling
    }

    private Vector2 GetBestTargetPos(Vector2 origin)
    {
        Vector2 castDir = new Vector2(m_rigidbody.velocity.x, 0f);
        RaycastHit2D hit = Physics2D.CircleCast(origin, maxDistnace/2f,
            castDir, maxDistnace, grappleTargetLayer);

        if (hit.collider != null && Vector2.Distance(origin, hit.point) <= maxDistnace)
        {
            RaycastHit2D hitGround = Physics2D.CircleCast(origin, 0.49f,
            hit.point - origin, Vector2.Distance(origin, hit.point), blockGrappleLayer);

            if(hitGround.collider == null || Vector2.Distance(origin, hitGround.point) > maxDistnace) // make sure we dont grapple through the ground
            {
                Debug.DrawRay(origin, hit.point - origin, Color.yellow, 0.1f); 
                return hit.point;
            }
        }

        return GetNearestTargetPos(origin);
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

        RaycastHit2D hitGround = Physics2D.CircleCast(origin, 0.49f, nearest - origin,
             Vector2.Distance(origin, nearest), blockGrappleLayer);

        if (hitGround.collider != null && hitGround.distance <= maxDistnace) return new Vector2(99999f, 99999f); // return invalid distance when obstacle is in the way

        if (minDistance <= maxDistnace) Debug.DrawRay(origin, nearest - origin, Color.white, 0.1f);
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
            Vector2 distanceVector = m_camera.ScreenToWorldPoint(point) - gunPivot.position;
            if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
            {
                RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
                if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
                {
                    if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                    {
                        playerRef.SetBoolGrapple(true);
                        grapplePoint = _hit.transform.position;
                        grappleTarget = _hit.transform.gameObject;
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
        m_rigidbody.gravityScale = 1f; // increase gravity during grapple for better swinging
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }

        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.velocity = Vector2.zero;
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