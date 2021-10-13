using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickControl : MonoBehaviour
{
    public Joystick joy;
    Vector2 movement;
    public GameObject player_object;
    Rigidbody rb;
    public float movespeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = joy.Horizontal;
        movement.y = joy.Vertical;
        Vector2 pos1 = new Vector2(rb.position.x , rb.position.y);
        rb.MovePosition(pos1+movement*movespeed* Time.deltaTime);
    }
}
