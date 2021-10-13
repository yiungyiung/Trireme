using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class healthBar : NetworkBehaviour
{
    GameObject pla;
    [SerializeField]
    public Text textHealthBar;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    
    {   
        pla = GameObject.FindWithTag("Player");
        if(pla == null){
        textHealthBar.text = "Ooh you just died noob" ;
    } 
    else{
        Enemy playerHealth = pla.GetComponent<Enemy>();
        Debug.Log(playerHealth.health);
        textHealthBar.text = "Health: "+playerHealth.health;}
    }
}
