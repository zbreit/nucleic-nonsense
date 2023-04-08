using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanTestObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //                -->      <--   -->      <--   -x>
        //                SaaEnnEnnSooIxxSaaEnnEnnSooIxxSaaEnnEnnEnn
        string testStr = "XXXZYZXYZYYYYZZXXXZZYZYXYYXYYXXXZYZYXYXZYX";
        DnaUtil.DecodeDnaString(testStr);
        //Debug.Log(DnaUtil.DecodeDnaString(testStr))
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
