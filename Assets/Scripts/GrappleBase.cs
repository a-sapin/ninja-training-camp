using System;
using UnityEngine;

public class GrappleBase : MonoBehaviour
{
    [Header("General Refernces:")] public GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;
    private VFXManager vfxManager;
    public GameObject grappleParticle;

    [Header("General Settings:")] [SerializeField] [Range(5, 100)] [Tooltip("Nombre de points qui composent la corde")]
    private int resolution = 40;

    [Range(0, 20)] [SerializeField] [Tooltip("Vitesse de realignement du wiggle.")]
    private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")] public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")] public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    [Header("Wiggle Simulation Variables")]
    public float damper = 14f;

    public float strength = 800f;
    public float initialWiggleVelocity = 15f;
    private float currentWiggleVelocity = 0f; // used in animating the rope wiggle
    public AnimationCurve ropeWhipCurve;
    private float wiggleModifier = 0f;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool straightLine = true;

    public void Start()
    {
        vfxManager = FindObjectOfType<VFXManager>();
    }

    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.positionCount = resolution;
        waveSize = StartWaveSize;
        straightLine = false;

        LinePointsToFirePoint();

        m_lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        wiggleModifier = 0f; // reset wiggle animation
        currentWiggleVelocity = initialWiggleVelocity;
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    //precision pour le rendering du grapple. plus petit veut dire laid
    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < resolution; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    //appeler pour le rending du grapple. Est appeler a chaquer frames
    void DrawRope()
    {
        if (!straightLine)
        {
            if (m_lineRenderer.GetPosition(resolution - 1).x == grapplingGun.grapplePoint.x)
            {
                straightLine = true;
                // a grappelé 
                grapplingGun.grappleTarget.GetComponentInChildren<Animator>().SetTrigger("hit");
                vfxManager.Stop("Anvil");
                vfxManager.Play("Anvil");
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if (!isGrappling)
            {
                
                grapplingGun.Grapple();
                isGrappling = true;
            }

            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;

                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (m_lineRenderer.positionCount != 2)
                {
                    m_lineRenderer.positionCount = 2;
                }

                DrawRopeNoWaves();
            }
        }
    }

    //fait une corde pour le montant de precision. utilise le ropeAnimationCurve pour gerer le wiggle
    void DrawRopeWaves()
    {
        WiggleSimulation(Time.deltaTime); // update wiggleModifier

        for (int i = 0; i < resolution; i++)
        {
            float delta = (float) i / ((float) resolution - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized *
                             ropeAnimationCurve.Evaluate(delta)
                             * waveSize * wiggleModifier * ropeWhipCurve.Evaluate(delta);

            Vector3 firePointPos = updateFirePointPosition();
            
            Vector2 targetPosition = Vector2.Lerp(firePointPos, grapplingGun.grapplePoint, delta) +
                                     offset;
            Vector2 currentPosition = Vector2.Lerp(firePointPos, targetPosition,
                ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }

    // Make wiggleModifier vary like a step response in a second order system
    // bruh
    // Basically a sine wave that starts with high amplitude and quickly (relative to damper) 
    // drops to a low amplitude
    private void WiggleSimulation(float deltaTime)
    {
        var direction = wiggleModifier < 0 ? 1f : -1f; // whether the wave is flipped or not
        var force = Mathf.Abs(wiggleModifier) * strength; // how strong the wiggle will be
        currentWiggleVelocity += (force * direction - currentWiggleVelocity * damper) * deltaTime;
        wiggleModifier += currentWiggleVelocity * deltaTime;
    }

    void DrawRopeNoWaves()
    {
        Vector3 firePointPos = updateFirePointPosition();

        m_lineRenderer.SetPosition(0, firePointPos);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }

    private Vector3 updateFirePointPosition()
    {
        Vector3 firePointPos = grapplingGun.firePoint.position;
        if (grapplingGun.playerRef.isSpriteFlipped())
        {
            firePointPos = grapplingGun.firePoint.position + new Vector3(-0.2f, 0.7f, 0f);
        }
        else
        {
            firePointPos = grapplingGun.firePoint.position + new Vector3(0.2f, 0.7f, 0f);
        }

        return firePointPos;
    }
}