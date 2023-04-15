using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrefabSelector_ByType : MonoBehaviour, NodePrefabSelector
{
    public GameObject GetPrefab(GameObject[] prefabs, GraphyNodeGene nodeGene) {
        return prefabs[(int) nodeGene.type];
    }
}
