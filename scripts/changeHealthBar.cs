using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;   

public class changeHealthBar : NetworkBehaviour
{
    public string s;
    public string f;
    public Text healthbar;
    public Slider sl;
    int healthParam = 100;
    
    void Start(){
        
        
        
    }
    void Update(){
        
        healthbar.text = "health:" + healthParam;
        
    }
    public void setHealthBar(int health){
        Debug.Log(health.ToString());
        healthParam  = health;
        
        Debug.Log(healthParam.ToString());
        

    }
}
