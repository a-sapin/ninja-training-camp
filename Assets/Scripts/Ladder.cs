using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Ladder : MonoBehaviour
{
    private LineRenderer lr;
    private BoxCollider2D myBox;
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        myBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, top.localPosition);
        lr.SetPosition(1, bottom.localPosition);

        // total height of ladder
        float size_y = top.localPosition.y - bottom.localPosition.y;

        // how high above 0 the center of the collider should be
        // we take half the ladder's height and add it to the bottom position
        float offset_y = (size_y / 2.0f) + bottom.localPosition.y;

        myBox.size = new Vector2(1, size_y);
        myBox.offset = new Vector2(0, offset_y);
    }
}
