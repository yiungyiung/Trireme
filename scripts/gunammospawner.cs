using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunammospawner : MonoBehaviour
{
    public int objectammo;
    public bool taken=false;
    float timerr;
    SpriteRenderer spr;
    void Start()
    {
       timerr=10.0f;
       spr = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (taken){
            timerr+=Time.deltaTime;
            //Debug.Log(timerr+"time");
        }
        if (timerr<10.0f){return;}
        else{
        taken=false;
        spr.enabled = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,1);
        foreach (Collider2D nearbyObject in colliders)
        {
           if (nearbyObject.tag=="Player")
           {
               nearbyObject.GetComponent<shooting>().totalpistolammo=36;
               nearbyObject.GetComponent<shooting>().totalgunammo=90;
               taken=true;
               spr.enabled = false;
               timerr=0.0f;

               break;
               }
               }
        }  
    
    
    }
}
