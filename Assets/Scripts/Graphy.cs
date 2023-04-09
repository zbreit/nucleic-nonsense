using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphy : MonoBehaviour
{
    private float startTime = 0f;

    public GraphyGene gene;
    public List<GameObject> nodeObjects;
    private int nodeIndex = 0;

    public GameObject nodePrefab;
    public GameObject linkagePrefab;

    private static System.Random staticRng;

    public float linkLength = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (gene is null) { Destroy(transform.gameObject); }

        staticRng = new System.Random();
        
        startTime = Time.time;
        nodeObjects = new List<GameObject>(gene.nodes.Count);
        StartCoroutine(SpawnNode());
    }

    private IEnumerator SpawnNode() {
        GraphyNodeGene nodeGene = gene.nodes[nodeIndex];
        GameObject hostNode = nodeGene.linkageEncoder >= 0 ? nodeObjects[nodeGene.linkageEncoder] : null;
        // Determine spawn Offset
        System.Random rng = new System.Random((0xA & 0xD) ^ (nodeGene.codons[0].str+nodeGene.codons[1].str).GetHashCode());
        float rot = (float) rng.NextDouble() * Mathf.PI * 2f;
        Vector3 spawnOffsetVector = (new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0)) * linkLength;
        // Initiate Node
        GameObject newNode = Instantiate(
            nodePrefab,
            (nodeGene.linkageEncoder >= 0 ? (hostNode.transform.position + spawnOffsetVector) : Vector3.zero),
            Quaternion.identity
        );
        newNode.GetComponent<GraphyNode>().gene = gene;
        newNode.GetComponent<GraphyNode>().nodeGene = nodeGene;
        newNode.GetComponent<GraphyNode>().id = staticRng.Next();
        nodeObjects.Add(newNode);
        // Initiate Linkage
        if (nodeGene.linkageEncoder >= 0) {
            GameObject newLinkage = Instantiate(
                linkagePrefab,
                hostNode.transform.position + spawnOffsetVector/2,
                Quaternion.identity
            );
            newLinkage.GetComponent<Linkage>().length = linkLength;
            newLinkage.GetComponent<Linkage>().SetEnds(newNode, hostNode);
            newNode.GetComponent<GraphyNode>().linkages.Add(hostNode);
            hostNode.GetComponent<GraphyNode>().linkages.Add(newNode);

            newNode.AddComponent<DistanceJoint2D>();
            DistanceJoint2D joint = newNode.GetComponent<DistanceJoint2D>();
            joint.distance = linkLength;
            joint.connectedBody = hostNode.GetComponent<Rigidbody2D>();
            joint.autoConfigureDistance = false;
        }
        // Delay
        yield return new WaitForSeconds(0.5f);
        // Increment and end
        nodeIndex += 1;
        if (nodeIndex < gene.nodes.Count) {
            StartCoroutine(SpawnNode());
        } else { Destroy(transform.gameObject); }
    }
}
