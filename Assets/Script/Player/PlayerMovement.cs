using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float maxSpeed;
    private float currentSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    public float attackRate;
    private float lastAttackTime;
    

    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {   

        //move player depend our currentSpeed    
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized * currentSpeed;
        //Set value of value of rb
        anim.SetFloat("MoveX",rb.velocity.x);
        anim.SetFloat("MoveY",rb.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("LastX",Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("LastY",Input.GetAxisRaw("Vertical"));
        }

        if(Input.GetMouseButtonDown(0) &&Time.time - lastAttackTime > attackRate && EquipManager.instance.currentEquip != null)
        {
            //Set lastattackTime to the current time of game
            lastAttackTime = Time.time;
            //activate attack animation
            anim.SetTrigger("Attack");
        }

    }
    
     // Set Event for animation attack
    public void StopMoving()
    {
        currentSpeed = 0;
    }

    public void CanMove()
    {
        currentSpeed = maxSpeed;
    }

   
}
