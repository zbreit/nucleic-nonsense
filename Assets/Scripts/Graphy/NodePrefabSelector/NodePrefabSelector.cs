using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NodePrefabSelector
{
    GameObject GetPrefab(GameObject[] prefabs, GraphyNodeGene nodeGene);
}
