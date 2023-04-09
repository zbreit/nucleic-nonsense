using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphyNode : MonoBehaviour
{
    public GraphyGene gene;
    public GraphyNodeGene nodeGene;

    public float resource = 50.0f;

    public SpriteRenderer spriteRenderer;

    public List<GameObject> linkages;

    public readonly Dictionary<GraphyNodeGene.Type, Color> colorDict = new Dictionary<GraphyNodeGene.Type, Color> {
        { GraphyNodeGene.Type.Structure,   new Color(  0.8f,       0.8f,       0.8f,       1.0f) },
        { GraphyNodeGene.Type.Food,        new Color(  0.8f,       0.8f,       0.2f,       1.0f) },
        { GraphyNodeGene.Type.Attack,      new Color(  0.8f,       0.2f,       0.2f,       1.0f) },
        { GraphyNodeGene.Type.Defend,      new Color(  0.2f,       0.5f,       0.8f,       1.0f) },
        { GraphyNodeGene.Type.Move,        new Color(  0.5f,       0.8f,       0.2f,       1.0f) }
    };

    // Start is called before the first frame update
    void Awake()
    {
        linkages = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nodeGene is null || gene is null) { Destroy(transform.gameObject); } else {
            spriteRenderer.color = colorDict[nodeGene.type];
        }
    }

    void FixedUpdate() {

    }

    void OnDestroy() {
        foreach(GameObject linkObj in linkages) { if (!(linkObj.transform is null)) Destroy(linkObj.transform); }
    }
}
