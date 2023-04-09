using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : MonoBehaviour
{
    public GraphyNodeGene nodeGene;

    private const float MIN_RANGE = 1.0f;
    private const float MIN_FORCE = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float force = nodeGene.weights[0];
        float range = nodeGene.weights[1];
        float cost = nodeGene.weights[2];
        float like_food = nodeGene.weights[3];
        float like_node = nodeGene.weights[4];
        float hate_node = nodeGene.weights[5];

        // Find targets
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();

        // Determine direction
        Vector2 nodeDir = new Vector2(0,0);
        // GameObject closestFood;
        foreach(GameObject go in gos) {
            if ((transform.position - go.transform.position).magnitude <= range + MIN_RANGE) {
                if (!(go.GetComponent<GraphyNode>() is null) && go.GetComponent<GraphyNode>().id != transform.GetComponent<GraphyNode>().id) {

                }
                // if (!(go.GetComponent<Food>() is null)) {
                //     if (closestFood is null) {
                //         closestFood = go;
                //     } else {
                //         if ((go.transform.position - transform.position).magnitude < ((closestFood as GameObject).transform.position - transform.position).magnitude) {
                //             closestFood = go;
                //         }
                //     }
                // }
            }
        }

        // Move
        transform.GetComponent<Rigidbody2D>().AddForce((force + MIN_FORCE) * Vector2.zero);
    }
}
