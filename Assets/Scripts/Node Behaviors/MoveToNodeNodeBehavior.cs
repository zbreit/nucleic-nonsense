using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNodeNodeBehavior : NodeBehavior, OnFixedUpdateNodeBehavior
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
        float like_node = node.nodeGene.weights[4 + offsetWeightsFactor];
        float hate_node = node.nodeGene.weights[4 + offsetWeightsFactor];
        
        // Find targets
        GameObject[] gos = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        // Determine direction
        Vector2 nodeDir = new Vector2(0,0);
        foreach(GraphyNode nodeIter in GameObject.FindObjectsOfType<GraphyNode>()) {
            if (
                (nodeIter.graphy != transform.GetComponent<GraphyNode>().graphy) &&
                ((Vector2) (nodeIter.transform.position - transform.position)).magnitude <= range
            ) {
                nodeDir += (Vector2) (nodeIter.transform.position - transform.position).normalized;
            }
        }

        // Apply force
        transform.GetComponent<Rigidbody2D>().AddForce(force * moveFactor * ((like_node - hate_node)*nodeDir));

        // Cost
        transform.GetComponent<GraphyNode>().resource -= cost*Time.fixedDeltaTime;
    }
}
