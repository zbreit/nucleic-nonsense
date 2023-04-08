using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphyGene
{
    public string dnaString;
    public List<GraphyNodeGene> nodes;

    public GraphyGene() {
        nodes = new List<GraphyNodeGene>();
    }
}

public class GraphyNodeGene
{
    public enum Type
    {
        Structure = 0,
        Food = 1,
        Attack = 2,
        Defend = 3,
        Move = 4
    }

    public readonly Dictionary<Type, Int32> weightLenDict = new Dictionary<Type, Int32> {}; // #TODO: Create Mapping

    public Type type = Type.Structure;
    public int linkageEncoder = -1;
    public List<Single> weights;

    public GraphyNodeGene(Type type) {
        this.type = type;
        this.weights = new List<Single>(weightLenDict[type]);
    }
}
