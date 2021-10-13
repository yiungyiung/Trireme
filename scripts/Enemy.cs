using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class Enemy : NetworkBehaviour
{
    public bool hasDied = false;
    GameObject pla;
    [SerializeField]
    public Text healthBar;
    [SyncVar]
    public int health=100;
    [SerializeField]
    public Text alertBox;
    [SerializeField]
    public Text Deaathcounter;
    [SerializeField]
    public Text killbox;
    GameObject RespawnpointGulag;
    [SyncVar]
    float rtime = 4.0f;
    private NetworkStartPosition[] spawnposition;
    [SyncVar]
    public  int pointt=0;
    [SyncVar]
    public int deatth=0;



    void Start(){
        healthBar = GameObject.Find("Canvas/healthText").GetComponent<Text>();
        alertBox = GameObject.Find("Canvas/AlertBox").GetComponent<Text>();
        Deaathcounter=GameObject.Find("Canvas/deathcounter").GetComponent<Text>();
        killbox=GameObject.Find("Canvas/KillText").GetComponent<Text>();
        killbox.text="Kills "+pointt.ToString();
        alertBox.text = "";
        Deaathcounter.text="Deaths "+deatth.ToString();
        RespawnpointGulag = GameObject.FindWithTag("respawn_point");
        spawnposition = FindObjectsOfType<NetworkStartPosition>();
        
    }
    void Update(){
        if(!isLocalPlayer){return;}
        if(hasDied){
        changehealth();
        alertBox.text = "Respawning: " + (Mathf.Round(rtime));
        rtime -= Time.deltaTime;
         if(rtime <= 0){
             Respawn();
             alertBox.text = "";
             hasDied = false;
             deatth+=1;
         }
        }
    
        pla = gameObject;
        if(pla != null){
            healthBar.text = "Health: " + health;
            killbox.text="Kills"+pointt.ToString();
            Deaathcounter.text="Deaths "+deatth.ToString();
           
        }
        
        if(health <= 0 && !hasDied){
            
            healthBar.text = "Oh noob you just died";
            //alertBox.text = "You just experienced our exclusive first hand experience which we like to call Death";
            CDie();
            hasDied = true;
            gameObject.GetComponent<shooting>().enabled = false;
            gameObject.GetComponent<meelee>().enabled = false;
            gameObject.GetComponent<grenade>().enabled = false;
            
        }
        
    }
    public void changehealth()
    {
        Sdie();
    }
    public void takepoint(int ppooiint){
        pointt+=ppooiint;
    }

    public void TakeDamage(int damage)
    {
        health-=damage;
        
    }
    void Respawn(){
        
        int random_number = Random.Range(0, spawnposition.Length );
        gameObject.GetComponent<shooting>().enabled = true;
            gameObject.GetComponent<meelee>().enabled = true;
            gameObject.GetComponent<grenade>().enabled = true;
            gameObject.GetComponent<shooting>().currentammo = 30;
            gameObject.GetComponent<shooting>().currentPistolAmmo = 12;
            gameObject.GetComponent<shooting>().totalpistolammo = 36;
            gameObject.GetComponent<shooting>().totalgunammo = 90;
            gameObject.GetComponent<shooting>().bshoots = 0;
            gameObject.GetComponent<shooting>().pbshoots = 0;
            gameObject.GetComponent<grenade>().currentgammo = 1;
            gameObject.GetComponent<grenade>().totalgammo = 4;
        transform.position = spawnposition[random_number].transform.position;
        rtime = 4.0f;

    }
    [Command]
    void Sdie()
    {
        health=100;
    }
    void CDie(){
    //transform.position = spawnposition[random_number].transform.position;
    transform.position = RespawnpointGulag.transform.position;
    alertBox.text = "";
    

    }
}
