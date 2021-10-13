using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class explosion : NetworkBehaviour
{
    public float delay=2f;
    public float rad=10f;
    public float force=0f;
    //public GameObject hitEffect;
    public Animator anim;
    public AudioSource grenade_audioSource;
    public int ppp=1;
    float countdown;
    public bool hasexploded=false;
    public int gdamage=10;
    [SyncVar]
    public uint insstanceidlocal;
    [SyncVar]
    public uint notthrower;
    

    // Start is called before the first frame update
    void Start()
    {
     countdown=delay;

    }

    // Update is called once per frame
    void Update()
    {
       countdown-=Time.deltaTime;
       if(countdown<=0f&&!hasexploded)
       {
           
           explode();

           hasexploded=true;

       } 
    }



       public void explode()
       {
           anim.SetBool("isExploding", true);
            grenade_audioSource.PlayOneShot(grenade_audioSource.clip, 1.0f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,7);
        foreach (Collider2D nearbyObject in colliders)
        {
           Enemy enemy1 = nearbyObject.GetComponent<Enemy>();
           if(enemy1!=null)
           {
               enemy1.TakeDamage(gdamage);
               if (enemy1.health<=0 && enemy1.GetComponent<NetworkIdentity>().netId!=insstanceidlocal){
                   Debug.Log("Entered in g");
                   NetworkIdentity.spawned[insstanceidlocal].GetComponent<Enemy>().takepoint(ppp);
               }
           }

           
             
        }        
        
        Destroy(gameObject, 1f);      
       }
}



     
        
       

    

       
    




