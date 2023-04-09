using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanTestObj : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject spawnerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //                   MOV               FOD            DEF                  ATK                                   
        //                -->         <--   -->      <--   -->         <--      -->            <--                  -x>   <x-      -x>         
        //                SaaEnnEnnEnnSooIxxSaaEnnEnnSooIxxSaaEnnEnnEnnSoo
        string testStr = "XXXZYZYXZXYZYYYYZZXXYXXYZYXYYXYYXXXZYZYXYXZYXYYYXYZYZYXXXYXXXYZXYZXYXYYZXYYZXYYXYZYXYZXYYXXXXYYXYYZYXYYZXXXYXZXZZXZXXZX";
        //DnaUtil.DecodeDnaString(testStr);
        Debug.Log(DnaUtil.DecodeDnaString(testStr));
        string newStr = DnaUtil.Mutate(testStr);
        Debug.Log(newStr);
        Debug.Log(DnaUtil.DecodeDnaString(newStr));


        // GameObject testNode = Instantiate(nodePrefab);
        // testNode.GetComponent<GraphyNode>().gene = DnaUtil.DecodeDnaString(testStr);
        // testNode.GetComponent<GraphyNode>().nodeGene = DnaUtil.DecodeDnaString(testStr).nodes[0];
        GameObject graphy = Instantiate(spawnerPrefab);
        graphy.GetComponent<Graphy>().gene = DnaUtil.DecodeDnaString(testStr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
