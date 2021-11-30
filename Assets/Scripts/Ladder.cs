using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        float size_y = top.localPosition.y - bottom.localPosition.y;
        float offset_y = size_y / 2.0f;

        myBox.size = new Vector2(1, size_y);
        myBox.offset = new Vector2(0, offset_y);
    }
}
