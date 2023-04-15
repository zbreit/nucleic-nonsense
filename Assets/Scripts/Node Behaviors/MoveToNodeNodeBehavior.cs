using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNodeNodeBehavior : NodeBehavior, OnFixedUpdateNodeBehavior
{
    [SerializeField]
    private float moveFactor = 1.0f;

    private const float MIN_RANGE = 30.0f;
    private const float MIN_FORCE = 2.0f;
    private const float MIN_COST = 1.0f;
    

    public void OnFixedUpdate() {
        GraphyNode node = transform.GetComponent<GraphyNode>();

        float force     = node.nodeGene.weights[0 + offsetWeightsFactor] + MIN_FORCE;
        float range     = node.nodeGene.weights[1 + offsetWeightsFactor] + MIN_RANGE;
        float cost      = node.nodeGene.weights[3 + offsetWeightsFactor] + MIN_COST;
        float like_food = node.nodeGene.weights[4 + offsetWeightsFactor];
    }
}
