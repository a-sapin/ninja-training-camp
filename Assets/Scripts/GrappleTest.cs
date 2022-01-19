using UnityEngine;

public class GrappleTest : MonoBehaviour
{
    [Header("General Refernces:")]
    public GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] [Range(5,100)] [Tooltip("Precision pour le rendering du grapple.")] private int precision = 40;
    [Range(0, 20)] [SerializeField] [Tooltip("Vitesse de realignement du wiggle.")]private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool strightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.positionCount = precision;
        waveSize = StartWaveSize;
        strightLine = false;

        LinePointsToFirePoint();

        m_lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    //precision pour le rendering du grapple. plus petit veut dire laid
    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
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
        if (!strightLine)
        {
            if (m_lineRenderer.GetPosition(precision - 1).x == grapplingGun.grapplePoint.x)
            {
                strightLine = true;
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

                if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }

                DrawRopeNoWaves();
            }
        }
    }

    //fait une corde pour le montant de precision. utilise le ropeAnimationCurve pour gerer le wiggle
    void DrawRopeWaves()
    {
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}
