using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class shooting : NetworkBehaviour
{
  //variables used instead of using values: rateOfFire, 
    public Transform FIREPOINT;
    [SyncVar]
    public GameObject laserBulletPrefab;
    [SyncVar]
    public GameObject pistolBulletPrefab;
    [SerializeField]
    public float bulletForce=50f;
    public uint instanceIDPlayer;
    public int maxammo=30;
    public int pistolMaxAmmo = 12;
    public float reloadtime=10f;
    public int currentammo;//
    public int currentPistolAmmo;//
    private bool isreload= false;
    public Animator anim;
    public bool canShoot;
    [SyncVar]
    public bool pistolOn = false;
    int x = 1;
    [SyncVar]
    float rateOfFire = 0.10f;
    float nextTimeOfFire= 0;
    public AudioSource reloading_audioSource;
    public AudioSource pistol_sounds;
    public AudioSource ak_sounds;
    [SerializeField]
    int bulletsShot = 0;
    [SerializeField]
    int bulletsRecoil = 0;
    [SerializeField]
    int recoil = 0;
    [SerializeField]
    public Text ReloadingText;
    public int totalpistolammo;//
    public int totalgunammo;//
    public int bshoots=0;//
    public int pbshoots=0;//
    
    void Start()
    {
      totalpistolammo=36;
      totalgunammo=90;
      currentammo=maxammo;
      currentPistolAmmo = pistolMaxAmmo;
      ReloadingText = GameObject.Find("Canvas/reloadingText").GetComponent<Text>();
      
      
    }
    // Update is called once per frame
    public void Update()
    {
      if(!isLocalPlayer){return;}
      //if(!pistolOn){if (totalgunammo<=0){return;}}
      //if(pistolOn){if (totalpistolammo<=0){return;}}
     // Debug.Log("Shooting Instance ID: " + instanceIDPlayer);
      if(isreload) //Updates UI when the player is reloading and the gun is not allowed to shoot
      {
        ReloadingText.text = "Reloading";
        return; //The ammo need not be displayed if the player is reloading
      }
      
      if(Input.GetKey(KeyCode.Alpha2)){
        whenPistol();
        changegunon();
      }
      if(Input.GetKey(KeyCode.Alpha1)){
        whenAR();
        changegunoff();
      }
      
      if(Input.GetMouseButton(0)) //When the mouse button is pressed 
      {
        canShoot = true;
      }
      if(Input.GetMouseButtonUp(0))//when the mouse button is lifted
      {
      canShoot = false;
      recoil = 0;
      bulletsRecoil = 0;
      }
      if(!pistolOn)
      { if(totalgunammo>0){
        ReloadingText.text = "Ammo: " + currentammo+"/"+totalgunammo;}
        else{ReloadingText.text = "Ammo: " + currentammo+"/0";}
        } 
      else{
        if(totalpistolammo>0){
        ReloadingText.text = "Ammo: " + currentPistolAmmo+"/"+totalpistolammo;}
        else{ReloadingText.text = "Ammo: " + currentPistolAmmo+"/0";}
        }
      if(pistolOn){if (currentPistolAmmo<=0 && totalpistolammo<=0){return;}}
      if(!pistolOn){if (currentammo<= 0 && totalgunammo<=0){return;}}
      
      
      if(pistolOn&&totalpistolammo>0&&currentPistolAmmo!=pistolMaxAmmo){
      if (Input.GetKeyDown(KeyCode.R)) //entering the co-routing for reloading if the ammo in the clip is 0
      {
        callreload();
      }}
      else if (!pistolOn&&totalgunammo>0&&currentammo!=maxammo){
        if (Input.GetKeyDown(KeyCode.R)){ callreload();}

      }

      

      if(canShoot && Time.time >= nextTimeOfFire) //we dont want the control to execute shoot if the current ammo is less than 0
      { 
        if(pistolOn && currentPistolAmmo <=0){return;}
        if(!pistolOn && currentammo <= 0){return;}
        nextTimeOfFire = Time.time + rateOfFire;  
        bulletsShot ++;
        if(!pistolOn)
        {
          currentammo--;
        }
        else
        {
            currentPistolAmmo--;
        }
        
         callshoot();//shooting function is being called
        if(!pistolOn){
        bshoots=bshoots+1;} //checking how many bullets have been shot after reloading for both pistol and ar
        else{
          pbshoots=pbshoots+1;
        }
        bulletsRecoil += 1;
        canShoot = false;
        return;
      }
      

       
    }
    void callreload(){
       canShoot = false;
        anim.SetBool("reloadAnim", true); //starting animation
        bulletsShot = 0; //resetting recoil
        StartCoroutine(reload());
        return;

    }
    
    IEnumerator reload()
    {
      isreload=true;
      bulletsRecoil = 0;
      recoil = 0;
      Debug.Log("reloading");
      reloading_audioSource.PlayOneShot(reloading_audioSource.clip, 1.0f);
      yield return new WaitForSeconds(3F);


      if(!pistolOn&&totalgunammo>0)
      {
      
        if(totalgunammo>=maxammo)
          currentammo=maxammo;
        else
        {
          currentammo  += totalgunammo;
        }


          totalgunammo=totalgunammo-bshoots;
          bshoots=0;
      }


      else if(pistolOn && totalpistolammo>0){
        
            if (totalpistolammo>=pistolMaxAmmo){
                currentPistolAmmo = pistolMaxAmmo;}
            else {
                currentPistolAmmo = currentPistolAmmo +  totalpistolammo;
            }
            totalpistolammo=totalpistolammo-pbshoots;
            pbshoots=0;
      }
      isreload=false;
      Debug.Log("reloading over ");
      anim.SetBool("reloadAnim", false);

    }
    public void callshoot(){
      CmdShoot(bulletsRecoil,gameObject.GetComponent<NetworkIdentity>().netId);

    }
    [Command]
    public void CmdShoot(int BRecoil,uint idd)
    {
      if(!isreload){
     
      
       
       recoil = Random.Range((-5 * BRecoil) , (5 * BRecoil));
       GameObject laserBullet;
       if(pistolOn){ laserBullet= Instantiate(pistolBulletPrefab,FIREPOINT.position,FIREPOINT.rotation); 
                      pistol_sounds.PlayOneShot(pistol_sounds.clip , 1.0f);
            }
       else
       {
        laserBullet= Instantiate(laserBulletPrefab,FIREPOINT.position,FIREPOINT.rotation); 
        ak_sounds.PlayOneShot(ak_sounds.clip, 1.0f);
       }
       Rigidbody2D rb=laserBullet.GetComponent<Rigidbody2D>();

       if(FIREPOINT.right.y >=   0.8f && FIREPOINT.right.y <=   1.2f || FIREPOINT.right.y >= -1.2f && FIREPOINT.right.y <= -0.8f ){
         rb.AddForce((FIREPOINT.right +  new Vector3(0,recoil/750f,0)) * bulletForce,ForceMode2D.Impulse);
       }
       else{
        rb.AddForce((FIREPOINT.right +  new Vector3(0, recoil/750f ,0)) * bulletForce,ForceMode2D.Impulse);
       }
       Debug.Log("id"+idd);
       laserBullet.GetComponent<bullet>().instanceIDlocal=idd;
       NetworkServer.Spawn(laserBullet);
       
       
       
       
       
       }

       }

      public void pointUP()
      {
        canShoot = true;
        anim.SetBool("isShooting", true);
      }
      public void pointDOWN()
      {
        anim.SetBool("isShooting", false);
        canShoot = false;
        }
        public void changegunon()
        {
          anim.SetBool("hasPistol", true);
        }
      [Command]  
      public void whenPistol(){

        
        pistolOn = true;
        rateOfFire = 0.25f;

        /*else if(pistolOn){
          anim.SetBool("hasPistol", false);
          pistolOn = false;
          rateOfFire = 0.1f;
         
          
          
        }*/}
      [Command]  
      public void whenAR()
      {
        
          pistolOn = false;
          rateOfFire = 0.1f;
      }
        public void reloadPressed()
        {
          StartCoroutine(reload());
          anim.SetBool("reloadAnim", true);
        }
        public void changegunoff(){
          anim.SetBool("hasPistol", false);

        }

       


    }
    

