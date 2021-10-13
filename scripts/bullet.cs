using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class bullet : NetworkBehaviour
{
    public Rigidbody2D rb;
    [SyncVar]
    public uint instanceIDlocal;
    public int pp=1;
    public int damage = 10;
    [SyncVar]
    public uint  notshooter;

void OnTriggerEnter2D (Collider2D hitinfo)
    {
        Enemy enemy  = hitinfo.GetComponent<Enemy>();
        if(enemy!=null)
        {
            notshooter=hitinfo.GetComponent<NetworkIdentity>().netId;
            //Debug.Log("wokingrs"+notshooter);
           
            if(notshooter!=instanceIDlocal)
            {
            enemy.TakeDamage(damage);
            Debug.Log("NotEntered"+hitinfo.GetComponent<Enemy>().health);
            if (hitinfo.GetComponent<Enemy>().health<=0){
                Debug.Log("Entered");
                NetworkIdentity.spawned[instanceIDlocal].GetComponent<Enemy>().takepoint(pp);
            }  
            Destroy(gameObject);
            } 
        }
    
        if(hitinfo.tag == "wall")
        {
            Destroy(gameObject);
            BoxDestroy box_destroy = hitinfo.GetComponent<BoxDestroy>();
            if(box_destroy != null){
   
            box_destroy.takeBoxDamage();
            //Debug.Log("box");
        }
             Debug.Log("I just hit a wall");
        }
        if(hitinfo.tag == "box"){
            Debug.Log("box");
            Destroy(gameObject);
            hitinfo.GetComponent<BoxDestroy>().takeBoxDamage();
        }
        
        
    }
}
    
