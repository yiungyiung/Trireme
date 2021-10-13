using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class meelee : NetworkBehaviour
{
    public Animator anim1;
    public float nextTimeOffire;
    public float radm= 5f;
    public Transform meeleePoint;
    public GameObject meeleeprefab;

    

    public int mdamage=35;
    void Start(){
        nextTimeOffire = Time.time;
    }
    void  Update(){
        if(!isLocalPlayer)return;
        
        if(Input.GetKeyDown(KeyCode.E) && Time.time >= nextTimeOffire)
        {
            getknifed();
            
            nextTimeOffire = Time.time + 2;
            
        }
    }
   public void getknifed()
   {
       anim1.SetBool("isKnifed", true);
       knifed(gameObject.GetComponent<NetworkIdentity>().netId);
        Invoke("endreload", 0.25f);
   }
       [Command]
       public void knifed(uint aid)
       {   Debug.Log("woker");
           GameObject knifeobject;
           knifeobject=Instantiate(meeleeprefab,meeleePoint.position,meeleePoint.rotation);
           knifeobject.GetComponent<meelekill>().iinstanceIDlocal=aid;
           NetworkServer.Spawn(knifeobject);
           
        
        
}
void endreload(){
    anim1.SetBool("isKnifed", false);

}
}
