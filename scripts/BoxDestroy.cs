using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class BoxDestroy : NetworkBehaviour
{   [SyncVar]
    public int box_health;
    // Start is called before the first frame update
    void Start()
    {
        box_health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(box_health <= 0){Destroy(gameObject);}
    }
    public void takeBoxDamage(){
        box_health -= 10;
        Debug.Log("box health: " + box_health);
    }
}
