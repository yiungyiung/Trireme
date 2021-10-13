using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class meelekill : NetworkBehaviour
{
[SyncVar]
public uint iinstanceIDlocal;
public int pppp=1;
public int daamage = 10;
[SyncVar]
public uint  nnotshooter;

void OnTriggerEnter2D (Collider2D hitinfo)
    {
        Enemy enemy  = hitinfo.GetComponent<Enemy>();
        if(enemy!=null)
        {
            nnotshooter=hitinfo.GetComponent<NetworkIdentity>().netId;
            enemy.TakeDamage(daamage);
            if (hitinfo.GetComponent<Enemy>().health<=0){
                Debug.Log("Entered");
                NetworkIdentity.spawned[iinstanceIDlocal].GetComponent<Enemy>().takepoint(pppp);
            }  
            
            } 
        Destroy(gameObject);
        
        }
   
}
