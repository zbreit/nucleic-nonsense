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

    public override string ToString() {
        return "Gene\n" + String.Join('\n', nodes);
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
    public readonly Dictionary<Type, String> typeNameDict = new Dictionary<Type, String> {
        { Type.Structure, "Structure" },
        { Type.Food, "Food" },
        { Type.Attack, "Attack" },
        { Type.Defend, "Defend" },
        { Type.Move, "Move" }
    };

    public const int WEIGHT_LEN = 10;

    public Type type = Type.Structure;
    public int linkageEncoder = -1;
    public List<Codon> codons;
    public List<Single> weights;

    public GraphyNodeGene(Type type, int linkageEncoder, List<Codon> codons) {
        this.type = type;
        this.linkageEncoder = linkageEncoder;
        this.codons = codons;
        this.weights = new List<Single>(WEIGHT_LEN);
    }

    public override string ToString() {
        return String.Format("{0} Node : {1}", typeNameDict[this.type], linkageEncoder);
    }
}
