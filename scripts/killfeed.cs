using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class killfeed : NetworkBehaviour
{
    public Text kilfeed;
    string i="i";
void Start()
{
    kilfeed=GameObject.Find("Canvas/Killfeed").GetComponent<Text>();
}
void Update()
{   i+="i";
    kilfeed.text= i;

}

}
