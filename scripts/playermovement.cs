using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class playermovement : NetworkBehaviour
{
    public float movespeed= 5f;
    public Animator anim;
    public Rigidbody2D rb;
    public AudioSource audioSource;
    Vector2 movement;
    Vector2 mousePos;
    float time_played;
    float speed_player;
    public override void OnStartLocalPlayer()
     {
         Camera.main.GetComponent<Camerafollow>().setTarget(gameObject.transform);
         }
    void Start(){
        
    }
     
    /*void Start(){
        if(isLocalPlayer){
            Enemy en = gameObject.GetComponent<Enemy>();
            en.playerID=gameObject.GetComponent<NetworkIdentity>().netId;
        }
    }*/

    void FixedUpdate()
    {
        if(isLocalPlayer){
    //Debug.Log("Instance ID (pm): " + gameObject.GetInstanceID());
    movement.x=Input.GetAxisRaw("Horizontal");
    movement.y=Input.GetAxisRaw("Vertical");
    mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if(Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
    {
        anim.SetFloat("speed", Mathf.Abs(movement.x));
        speed_player = Mathf.Abs(movement.x);
    }
    else if(Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
    {
      anim.SetFloat("speed", Mathf.Abs(movement.y));  
      speed_player = Mathf.Abs(movement.y);
    }
    if((Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0) && Time.time > (time_played + (0.675f) / (speed_player*2))){
        audioSource.PlayOneShot(audioSource.clip, 1.0f);
        time_played = Time.time;
    }
    

    Vector2 lookdir=mousePos-rb.position;
    rb.MovePosition(rb.position+movement*movespeed *Time.fixedDeltaTime);
        
    float angle =Mathf.Atan2(lookdir.y,lookdir.x)*Mathf.Rad2Deg;
    rb.rotation=angle;
    
    }
    }
    
}