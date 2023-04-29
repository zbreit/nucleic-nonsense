using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFoodNodeBehavior : NodeBehavior, OnFixedUpdateNodeBehavior
{
    [SerializeField]
    private float moveFactor = 1.0f;

    [HeaderAttribute("Defaults")]
    [SerializeField]
    private float MIN_RANGE = 30.0f;
    [SerializeField]
    private float MIN_FORCE = 2.0f;
    [SerializeField]
    private float MIN_COST = 1.0f;

    public void OnFixedUpdate() {
        GraphyNode node = transform.GetComponent<GraphyNode>();

        float force     = node.nodeGene.weights[0 + offsetWeightsFactor] + MIN_FORCE;
        float range     = node.nodeGene.weights[1 + offsetWeightsFactor] + MIN_RANGE;
        float cost      = node.nodeGene.weights[3 + offsetWeightsFactor] + MIN_COST;
        float like_food = node.nodeGene.weights[4 + offsetWeightsFactor];

        // Find targets
        GameObject[] gos = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        // Determine direction of food
        Food closestFood = null;
        foreach(Food food in GameObject.FindObjectsOfType<Food>()) {
            // Debug.Log(food);
            if (((Vector2) (food.transform.position - transform.position)).magnitude <= range) {
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
        
        // Add Force
        transform.GetComponent<Rigidbody2D>().AddForce(force * moveFactor * (
            closestFood is null ? 
                Vector2.zero : 
                ((Vector2) ((closestFood as Food).transform.position - transform.position)).normalized
        ));

        // Cost
        transform.GetComponent<GraphyNode>().resource -= cost*Time.fixedDeltaTime;
    }
}
