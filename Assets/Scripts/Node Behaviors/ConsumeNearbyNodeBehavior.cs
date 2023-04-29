using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeNearbyNodeBehavior : NodeBehavior, OnFixedUpdateNodeBehavior
{

    [SerializeField]
    private float speedFactor = 1.0f;

    [HeaderAttribute("Defaults")]   
    [SerializeField]
    private float DEF_RANGE = 6.0f;
    [SerializeField]
    private float DEF_SPEED = 5.0f;
    [SerializeField]
    private float DEF_EFFICIENCY = 0.2f;
    [SerializeField]
    private float DEF_COST = 1.0f;
    
    [System.NonSerialized]
    private Food targetFood;

    private GraphyNode node;

    public void OnFixedUpdate() {
        if (!targetFood) {targetFood = null;}
        
        node = transform.GetComponent<GraphyNode>();

        float speed         = node.nodeGene.weights[0 + offsetWeightsFactor] + DEF_SPEED;
        float range         = node.nodeGene.weights[1 + offsetWeightsFactor] + DEF_RANGE;
        float cost          = node.nodeGene.weights[3 + offsetWeightsFactor] + DEF_COST;
        float efficiency    = node.nodeGene.weights[4 + offsetWeightsFactor] + DEF_EFFICIENCY;


        // Determine if target is still in range
        if ((targetFood is null) || (((Vector2) (targetFood?.transform.position - transform.position)).magnitude > range)) {
            targetFood = null;
        }

        // Find closest food
        foreach(Food food in GameObject.FindObjectsOfType<Food>()) {
            float dist = ((Vector2) (food.transform.position - transform.position)).magnitude;
            if (dist <= range && ((targetFood is null) || dist < ((Vector2) (targetFood.transform.position - transform.position)).magnitude)) {
                targetFood = food; 
            }
        }

        if (!(targetFood is null)) {
            
            node.Consume(targetFood.Consume(speed * Time.fixedDeltaTime) * efficiency, targetFood);
        }
    }

    void OnDrawGizmos() {
        if (!targetFood) {targetFood = null;}
        if (!(node is null) && node.drawDebug && !(targetFood is null)) {
            Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
            Gizmos.DrawSphere(targetFood.transform.position, 1);
        }
    }
}
