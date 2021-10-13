using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammospawner : MonoBehaviour
{
    public int objectammo;
    public bool taken=false;
    float timerr;
    void Start()
    {
       timerr=10.0f;
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
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,1);
        foreach (Collider2D nearbyObject in colliders)
        {
           if (nearbyObject.tag=="Player")
           {
               nearbyObject.GetComponent<grenade>().totalgammo=4;
               taken=true;
               timerr=0.0f;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
               break;
               }
               }
        }  
    
    
    }
        
}
