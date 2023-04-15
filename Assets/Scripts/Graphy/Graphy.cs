using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphy : MonoBehaviour
{

    // Graphy logic
    public GraphyGene gene;
    public List<GraphyNode> nodes;
    private static int id;
    private float timeOfBirth = 0f;

    // Spawning logic
    private int nodeIndex = 0;
    public bool spawningComplete = false;

    public GameObject[] nodePrefabs;
    public GameObject linkagePrefab;
    [SerializeField]
    private NodePrefabSelector nodePrefabSelector;

    private static System.Random staticRng;

    public float linkLength = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (gene is null) { Destroy(this); return; }

        if (nodePrefabSelector is null) { nodePrefabSelector = transform.GetComponent<NodePrefabSelector>(); }
        if (nodePrefabSelector is null) { Debug.Log("Bye");Destroy(this); return; }

        staticRng = new System.Random();
        id = staticRng.Next();
        
        timeOfBirth = Time.time;
        nodes = new List<GraphyNode>(gene.nodes.Count);
        StartCoroutine(SpawnNode());
    }

    private IEnumerator SpawnNode() {
        GraphyNodeGene nodeGene = gene.nodes[nodeIndex];
        GraphyNode hostNode = nodeGene.linkageEncoder >= 0 ? nodes[nodeGene.linkageEncoder] : null;
        // Spawn Node
        SpawnNodeGameObject(nodeGene, hostNode);
        // Delay
        yield return new WaitForSeconds(0.5f);
        // Increment and end
        nodeIndex += 1;
        if (nodeIndex < gene.nodes.Count) {
            StartCoroutine(SpawnNode());
        }  else {
            spawningComplete = true;
        }
    }

    private GraphyNode SpawnNodeGameObject(GraphyNodeGene nodeGene, GraphyNode? hostNode) {
        // Setup Node ==========================================================
        // Determine offset angle
        System.Random rng = new System.Random((0xA & 0xD) ^ (nodeGene.codons[0].str+nodeGene.codons[1].str).GetHashCode());
        float rot = (float) rng.NextDouble() * Mathf.PI * 2f;
        Vector3 spawnOffsetVector = (new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0)) * linkLength;
        // Instantiate Node
        GameObject obj = Instantiate(
            nodePrefabSelector.GetPrefab(nodePrefabs, nodeGene),
            (nodeGene.linkageEncoder >= 0 ? (hostNode.transform.position + spawnOffsetVector) : Vector3.zero),
            Quaternion.identity
        );
        // Apply property values
        GraphyNode newNode = obj.GetComponent<GraphyNode>();
        newNode.graphy = this;
        newNode.nodeGene = nodeGene;
        newNode.transform.parent = this.transform;
        // Apply node type
        // switch (nodeGene.type) {
        //     case GraphyNodeGene.Type.Move:
        //         newNode.gameObject.AddComponent<MoveNode>();
        //         newNode.gameObject.GetComponent<MoveNode>().nodeGene = nodeGene;
        //         break;
        //     case GraphyNodeGene.Type.Food:
        //         newNode.gameObject.AddComponent<FoodNode>();
        //         newNode.gameObject.GetComponent<FoodNode>().nodeGene = nodeGene;
        //         break;
        // }
        nodes.Add(newNode);

        // Setup Linkage ==========================================================
        if (nodeGene.linkageEncoder >= 0) {
            obj = Instantiate(
                linkagePrefab,
                hostNode.transform.position + spawnOffsetVector/2,
                Quaternion.identity
            );
            Linkage newLinkage = obj.GetComponent<Linkage>();
            newLinkage.length = linkLength;
            newLinkage.SetEnds(newNode, hostNode);
            newLinkage.transform.parent = this.transform;

            // Connect linkage to nodes
            newNode.linkages.Add(newLinkage);
            hostNode.GetComponent<GraphyNode>().linkages.Add(newLinkage);

            // Setup the linkage joint
            DistanceJoint2D joint = newNode.gameObject.AddComponent<DistanceJoint2D>();
            joint.distance = linkLength;
            joint.connectedBody = hostNode.GetComponent<Rigidbody2D>();
            joint.autoConfigureDistance = false;
        }

        return newNode;
    }
}
