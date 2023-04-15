using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : MonoBehaviour
{
    public GraphyNodeGene nodeGene;

    private const float MIN_FOOD_RANGE = 30.0f;
    private const float MIN_NODE_RANGE = 6.0f;
    private const float MIN_FORCE = 2.0f;
    private const float MIN_COST = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float force = nodeGene.weights[0] + MIN_FORCE;
        float foodRange = nodeGene.weights[1] + MIN_FOOD_RANGE;
        float nodeRange = nodeGene.weights[2] + MIN_NODE_RANGE;
        float cost = nodeGene.weights[3] + MIN_COST;
        float like_food = nodeGene.weights[4] + 2;
        float like_node = nodeGene.weights[5];
        float hate_node = nodeGene.weights[6] + 4.0f;

        // Find targets
        GameObject[] gos = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        // Determine direction
        Vector2 nodeDir = new Vector2(0,0);
        Food closestFood = null;
        foreach(GraphyNode node in GameObject.FindObjectsOfType<GraphyNode>()) {
            if (
                (node.graphyID != transform.GetComponent<GraphyNode>().graphyID) &&
                ((Vector2) (node.transform.position - transform.position)).magnitude <= nodeRange
            ) {
                // Debug.Log(node);
                // Debug.Log(node.graphyID);
                // Debug.Log(transform.GetComponent<GraphyNode>().graphyID);
                nodeDir += (Vector2) (node.transform.position - transform.position);
            }
        }
        foreach(Food food in GameObject.FindObjectsOfType<Food>()) {
            // Debug.Log(food);
            if (((Vector2) (food.transform.position - transform.position)).magnitude <= foodRange) {
                // Debug.Log(food);
                if (closestFood is null) {
                    closestFood = food;
                } else {
                    if (
                        ((Vector2) (food.transform.position - transform.position)).magnitude
                        < 
                        ((Vector2) ((closestFood as Food).transform.position - transform.position)).magnitude
                    ) {
                        closestFood = food;
                    }
                }
            }
        }
        // foreach(GameObject go in gos) {
        //     Debug.Log(go);
        //     Debug.Log(go.transform.GetComponent<GraphyNode>());
        //     Debug.Log(go.transform.GetComponent<Food>());
        //     if ((transform.position - go.transform.position).magnitude <= range + MIN_RANGE) {
        //         if (!(go.transform.GetComponent<GraphyNode>() is null) && go.transform.GetComponent<GraphyNode>().id != transform.GetComponent<GraphyNode>().id) {
        //             nodeDir += (Vector2) (go.transform.position - transform.position);
        //         }
        //         if (!(go.transform.GetComponent<Food>() is null)) {
        //             if ((closestFood as GameObject) is null) {
        //                 closestFood = go;
        //             } else {
        //                 if ((go.transform.position - transform.position).magnitude < ((closestFood as GameObject).transform.position - transform.position).magnitude) {
        //                     closestFood = go;
        //                 }
        //             }
        //         }
        //     }
        // }

        // Move
        // Debug.Log(closestFood);
        // Debug.Log(nodeDir);
        // Debug.Log((force) * ((like_node - hate_node)*nodeDir + (closestFood is null ? Vector2.zero : like_food*((Vector2) ((closestFood as Food).transform.position - transform.position)))));
        transform.GetComponent<Rigidbody2D>().AddForce(force * ((like_node - hate_node)*nodeDir + (closestFood is null ? Vector2.zero : like_food*((Vector2) ((closestFood as Food).transform.position - transform.position)))));

        // Cost
        transform.GetComponent<GraphyNode>().resource -= cost*Time.fixedDeltaTime;
    }
}
