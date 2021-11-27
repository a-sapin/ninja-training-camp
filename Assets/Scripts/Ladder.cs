using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, top.position);
        lr.SetPosition(1, bottom.position);
    }
}
