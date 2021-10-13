using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class grenade : NetworkBehaviour
{

   
    public Transform FIREPOINT;
    [SerializeField]
    public GameObject GrenadePrefab;
    [SerializeField]
    float grenadeForce=5f; //is mahashay ke upr serialize field kon dalega
    [SerializeField]
    public int currentgammo;
    [SerializeField]
    public int totalgammo;
    [SerializeField]
    public Text grenadeText;
    int showgammo;


    private int maxgammo =1;
    [SerializeField]
    private bool isgreload=false;
    public bool grenadeButtonPressed = false;
    [SerializeField]
    public GameObject grenade1;
     
     void Start()
    {
      totalgammo=4;
      currentgammo=maxgammo;
      grenadeText=GameObject.Find("Canvas/granda").GetComponent<Text>();
      
    }
    // Update is called once per frame
    
    void Update()
    {
      if(!isLocalPlayer){return;}
      if (totalgammo<=0)
      {
      grenadeText.text="Grenade:0/0";
      return;
      }
      if(isgreload)
      {
        grenadeText.text="Grenade:"+currentgammo+"/"+totalgammo;
        return;
      }
      showgammo=totalgammo-1;
      grenadeText.text="Grenade:"+currentgammo+"/"+showgammo;
      if (currentgammo<=0 && totalgammo!=0)
      {
        totalgammo--;
        StartCoroutine(Reload());
        return;
      }
      if(Input.GetButton("Fire2")){
        grenadeForce += Time.deltaTime * 10f;
        
        grenadeForce  = (grenadeForce > 50 ? 50f : grenadeForce);
        
      }
      if(Input.GetButtonUp("Fire2") && grenadeForce > 5f){
        currentgammo-- ;
        
        callshootg();
        
        grenadeForce = 5f;
        
      }   
    }
 
    public void callshootg(){
      ShootGrenade(grenadeForce,gameObject.GetComponent<NetworkIdentity>().netId);
    }
      
    
      [Command]
       public void ShootGrenade(float gfuel,uint ddi){
         if(!isgreload){
         
           grenade1= Instantiate(GrenadePrefab,FIREPOINT.position,FIREPOINT.rotation); 
           Rigidbody2D rb=grenade1.GetComponent<Rigidbody2D>();
          
           rb.AddForce(FIREPOINT.right*gfuel*2,ForceMode2D.Impulse);
           grenade1.GetComponent<explosion>().insstanceidlocal=ddi;
           NetworkServer.Spawn(grenade1);
           
           }

           

       }
       
       IEnumerator Reload()
       {
         
         
           isgreload=true;
           Debug.Log("greloading");
           yield return new WaitForSeconds(2F);
           currentgammo=maxgammo;
           isgreload=false;
           Debug.Log("greloading over ");
           
        }
        [Command]
        public void whenPressed(){
          grenadeButtonPressed = true;
        }
        [Command]
        public void whenReleased(){
          grenadeButtonPressed = false;
          
        }
}
    
      

