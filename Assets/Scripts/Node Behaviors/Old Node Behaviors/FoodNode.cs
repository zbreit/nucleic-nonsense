using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNode : MonoBehaviour
{
    public GraphyNodeGene nodeGene;

    [SerializeField]
    private bool drawDebug = false;

    private const float MIN_EAT_RANGE = 6.0f;
    private const float MIN_COST = 1.0f;
    private const float MIN_SPEED = 2.0f;

    private Food targetFood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = nodeGene.weights[0] + MIN_SPEED;
        float eatRange = MIN_EAT_RANGE;
        float cost = nodeGene.weights[3] + MIN_COST;


        // Determine if target is still in range
        if (!(targetFood is null) && (((Vector2) (targetFood.transform.position - transform.position)).magnitude > eatRange)) {
            targetFood = null;
        }
        // if (!(targetFood is null)) {
        //     Debug.Log(((Vector2) (targetFood.transform.position - transform.position)).magnitude);
        //     Debug.Log(eatRange);
        //     Debug.Log(((Vector2) (targetFood.transform.position - transform.position)).magnitude <= eatRange);
        // }

        FoodNode[] foodNodes = GameObject.FindObjectsOfType<FoodNode>();
        List<FoodNode> foodNodesInGraphy = new List<FoodNode>();
        foreach(FoodNode node in foodNodes) {
            if (node.transform.GetComponent<GraphyNode>().graphy == transform.GetComponent<GraphyNode>().graphy) { foodNodesInGraphy.Add(node); }
        }

        if (targetFood is null) {
            // We should make food 
            foreach(Food food in GameObject.FindObjectsOfType<Food>()) {
                if (((Vector2) (food.transform.position - transform.position)).magnitude <= eatRange) {
                    bool verdict = true;
                    foreach(FoodNode node in foodNodesInGraphy) { if (food == node.targetFood) { verdict = false; break; } }
                    if (verdict) {
                        targetFood = food;
                        break;
                    }
                }
            }
        }

    }

    void OnDrawGizmos() {
        if (drawDebug && !(targetFood is null)) {
            Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
            Gizmos.DrawSphere(targetFood.transform.position, 1);
        }
    }
}
