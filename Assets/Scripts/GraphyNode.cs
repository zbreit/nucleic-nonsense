using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphyNode : MonoBehaviour
{
    public Graphy graphy;
    public GraphyNodeGene nodeGene;

    public float resource = 50.0f;

    public SpriteRenderer spriteRenderer;

    [System.NonSerialized]
    public List<Linkage> linkages;

    public bool drawDebug = false;

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
        linkages = new List<Linkage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nodeGene is null || graphy is null) { Destroy(transform.gameObject); } else {
            spriteRenderer.color = colorDict[nodeGene.type];
        }

    }

    void OnDestroy() {
        foreach(Linkage linkObj in linkages) {
            if (!(linkObj is null)) {
                Destroy(linkObj.gameObject);
            }
        }
    }

    void FixedUpdate() {
        if (graphy.spawningComplete) {
            OnFixedUpdateNodeBehavior[] behaviors = transform.GetComponents<OnFixedUpdateNodeBehavior>();
            foreach(OnFixedUpdateNodeBehavior b in behaviors) {
                b.OnFixedUpdate();
            }
        }
    }

    public void InteractReceive(Interaction interaction) {
        if (graphy.spawningComplete) {
            OnInteractReceiveNodeBehavior[] behaviors = transform.GetComponents<OnInteractReceiveNodeBehavior>();
            foreach(OnInteractReceiveNodeBehavior b in behaviors) {
                b.OnInteractReceive(interaction);
            }
        }
    }

    public void InteractSend(Interaction interaction) {
        if (graphy.spawningComplete) {
            OnInteractSendNodeBehavior[] behaviors = transform.GetComponents<OnInteractSendNodeBehavior>();
            foreach(OnInteractSendNodeBehavior b in behaviors) {
                b.OnInteractSend(interaction);
            }
        }
    }

    public void Consume(float amount, Food food) {
        if (graphy.spawningComplete) {
            OnConsumeNodeBehavior[] behaviors = transform.GetComponents<OnConsumeNodeBehavior>();
            foreach(OnConsumeNodeBehavior b in behaviors) {
                b.OnConsume(amount, food);
            }
            resource += food.Consume(amount);
        }
    }

    public class Interaction {}
}
