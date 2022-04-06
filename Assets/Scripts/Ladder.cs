using UnityEngine;

[ExecuteAlways]
public class Ladder : MonoBehaviour
{
    private LineRenderer lr;
    private BoxCollider2D myBox;
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;
    [SerializeField] Collider2D topPlat;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        myBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 topPosition = top.localPosition;
        Vector3 bottomPosition = bottom.localPosition;
        lr.SetPosition(0, topPosition);
        lr.SetPosition(1, bottomPosition);

        // total height of ladder
        float sizeY = topPosition.y - bottomPosition.y;

        // how high above 0 the center of the collider should be
        // we take half the ladder's height and add it to the bottom position
        float offsetY = (sizeY / 2.0f) + bottomPosition.y;

        myBox.size = new Vector2(1, sizeY);
        myBox.offset = new Vector2(0, offsetY);
    }

    public void DisableTopPlatform()
    {
        topPlat.enabled = false;
    }

    public void EnableTopPlatform()
    {
        topPlat.enabled = true;
    }
}
