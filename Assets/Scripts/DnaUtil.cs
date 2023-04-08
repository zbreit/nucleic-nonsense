using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DnaUtil
{
    public static readonly List<Codon> startCodons = new List<Codon> {new Codon("XXX"), new Codon("XXY"), new Codon("XXZ")};
    public static readonly List<Codon> stopCodons = new List<Codon> {new Codon("YYX"), new Codon("YYY"), new Codon("YYZ")};
    public static readonly Dictionary<Codon,GraphyNodeGene.Type> typeCodonDict = new Dictionary<Codon,GraphyNodeGene.Type>
    {
        {new Codon("XXX"), GraphyNodeGene.Type.Food},
        {new Codon("XXY"), GraphyNodeGene.Type.Food},
        {new Codon("XXZ"), GraphyNodeGene.Type.Food},
        {new Codon("XYX"), GraphyNodeGene.Type.Food},
        {new Codon("XYY"), GraphyNodeGene.Type.Food},
        {new Codon("XYZ"), GraphyNodeGene.Type.Food},
        {new Codon("XZX"), GraphyNodeGene.Type.Food},
        {new Codon("XZY"), GraphyNodeGene.Type.Food},
        {new Codon("XZZ"), GraphyNodeGene.Type.Food},
        {new Codon("YXX"), GraphyNodeGene.Type.Attack},
        {new Codon("YXY"), GraphyNodeGene.Type.Attack},
        {new Codon("YXZ"), GraphyNodeGene.Type.Attack},
        {new Codon("YYX"), GraphyNodeGene.Type.Attack},
        {new Codon("YYY"), GraphyNodeGene.Type.Attack},
        {new Codon("YYZ"), GraphyNodeGene.Type.Attack},
        {new Codon("YZX"), GraphyNodeGene.Type.Defend},
        {new Codon("YZY"), GraphyNodeGene.Type.Defend},
        {new Codon("YZZ"), GraphyNodeGene.Type.Defend},
        {new Codon("ZXX"), GraphyNodeGene.Type.Defend},
        {new Codon("ZXY"), GraphyNodeGene.Type.Defend},
        {new Codon("ZXZ"), GraphyNodeGene.Type.Defend},
        {new Codon("ZYX"), GraphyNodeGene.Type.Move},
        {new Codon("ZYY"), GraphyNodeGene.Type.Move},
        {new Codon("ZYZ"), GraphyNodeGene.Type.Move},
        {new Codon("ZZX"), GraphyNodeGene.Type.Move},
        {new Codon("ZZY"), GraphyNodeGene.Type.Move},
        {new Codon("ZZZ"), GraphyNodeGene.Type.Move},
    };

    public static GraphyGene DecodeDnaString(string dnaString) {
        GraphyGene gene = new GraphyGene();
        List<Codon> codons = SplitIntoCodons(dnaString);
        List<Exon> exons = ExtractExons(codons);
        foreach(Exon exon in exons) {
            // Make Node Gene
            GraphyNodeGene nodeGene = new GraphyNodeGene(GraphyNodeGene.Type.Structure);
            switch (typeCodonDict[exon[1]])
            {
                case GraphyNodeGene.Type.Food:
                    break;
                case GraphyNodeGene.Type.Attack:
                    break;
                case GraphyNodeGene.Type.Defend:
                    break;
                case GraphyNodeGene.Type.Move:
                    break;
                default:
                    break;
            }
            // Add Node Gene to GraphyGene nodes
            gene.nodes.Add(nodeGene);
        }
        // foreach(Exon exon in exons) { 
        //     foreach(Codon codon in exon) { Debug.Log(codon.str); }
        // }
        return null;
    }

    private static List<Exon> ExtractExons(List<Codon> codons) {
        List<Exon> exonList = new List<Exon>();
        bool inExon = false;
        Exon exon = new Exon();
        foreach(Codon codon in codons) {
            if (inExon) {
                if (codon.isIn(stopCodons)) {
                    exonList.Add(exon);
                    inExon = false;
                } else {
                    exon.Add(codon);
                }
            } else if (!inExon && codon.isIn(startCodons)) {
                inExon = true;
                exon = new Exon();
            }
            
        }
        return exonList;
    }

    private static List<Codon> SplitIntoCodons(string dnaString) {
        List<Codon> codons = new List<Codon>();
        int i = 0;
        while (i + 3 < dnaString.Length) {
            codons.Add(new Codon(dnaString.Substring(i,3)));
            i += 3;
        }
        return codons;
    }

    public static readonly Regex rg = new Regex(@"^[XYZ]*$");

    public static bool ValidateDnaString(string dnaString) {
        return rg.IsMatch(dnaString);
    }
}

public class Codon
{
    public readonly string str;

    public static readonly Regex rg = new Regex(@"^[XYZ]{3}$");

    public Codon(string codonString) {
        this.str = codonString;
    }

    public bool ValidateCodonString(string codonString) {
        return rg.IsMatch(codonString);
    }

    public bool Validate() {
        return rg.IsMatch(this.str);
    }

    public string toString() { return str; }

    public bool isIn(List<Codon> codons) {
        foreach (Codon codon in codons) {
            if (codon.str == this.str) { return true; }
        }
        return false;
    }
}

public class Exon : List<Codon> {
    public bool Validate() {
        if (this.Count < 2) return false;
        foreach(Codon codon in this) { if (!codon.Validate()) return false; }
        return true;
    }
}