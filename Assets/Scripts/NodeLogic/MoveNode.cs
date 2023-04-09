using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : MonoBehaviour
{
    public GraphyNodeGene nodeGene;

    private const float MIN_RANGE = 7.0f;
    private const float MIN_FORCE = 4.0f;
    private const float MIN_COST = 1.0f;

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
        GameObject[] gos = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        Debug.Log(gos.Length);

        // Determine direction
        Vector2 nodeDir = new Vector2(0,0);
        GameObject closestFood = null;
        foreach(GameObject go in gos) {
            if ((transform.position - go.transform.position).magnitude <= range + MIN_RANGE) {
                if (!(go.GetComponent<GraphyNode>() is null) && go.GetComponent<GraphyNode>().id != transform.GetComponent<GraphyNode>().id) {
                    nodeDir += (Vector2) (go.transform.position - transform.position);
                }
                if (!(go.GetComponent<Food>() is null)) {
                    if ((closestFood as GameObject) is null) {
                        closestFood = go;
                    } else {
                        if ((go.transform.position - transform.position).magnitude < ((closestFood as GameObject).transform.position - transform.position).magnitude) {
                            closestFood = go;
                        }
                    }
                }
            }
        }

        // Move
        Debug.Log(closestFood is null);
        Debug.Log((force + MIN_FORCE) * ((like_node - hate_node)*nodeDir + (closestFood is null ? Vector2.zero : like_food*((Vector2) ((closestFood as GameObject).transform.position - transform.position)))));
        transform.GetComponent<Rigidbody2D>().AddForce((force + MIN_FORCE) * ((like_node - hate_node)*nodeDir + (closestFood is null ? Vector2.zero : like_food*((Vector2) ((closestFood as GameObject).transform.position - transform.position)))));

        // Cost
        transform.GetComponent<GraphyNode>().resource -= (cost + MIN_COST)*Time.fixedDeltaTime;
    }
}
