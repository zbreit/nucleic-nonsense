using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanTestObj : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
