using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linkage : MonoBehaviour
{
    [SerializeField]
    private GraphyNode left;
    [SerializeField]
    private GraphyNode right;

    public float width = 0.2f;
    public float length = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShape();
        UpdateLocation();
        UpdateOrientation();
    }

    public void SetEnds(GraphyNode left, GraphyNode right) {
        this.left = left;
        this.right = right;
        UpdateShape();
        UpdateLocation();
        UpdateOrientation();
    }

    public void UpdateShape() {
        transform.localScale = new Vector3(width, (this.right.transform.position - this.left.transform.position).magnitude, 1);
    }

    public void UpdateLocation() {
        transform.position = this.left.transform.position + ((this.right.transform.position - this.left.transform.position) / 2);
    }

    public void UpdateOrientation() {
        float sign = (this.right.transform.position.x > this.left.transform.position.x)? -1.0f : 1.0f;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, sign * Vector2.Angle(Vector2.up, (this.right.transform.position - this.left.transform.position)));
    }
}
