using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData data;
    public Transform target;
    public float maxSpeed;
    private float currentSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    public float attackRate;
    private float lastAttackTime;
    public float chaseRadius;
    public float attackRadius;
    [SerializeField] public Transform waypointL, waypointR;
    public Transform stayPoint;

    public bool canMoveL, canMoveR;
    public bool canPatrol;

    private void Awake()
    {
        Patrol();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMoveL = false;
        canMoveR = true;
        canPatrol = true;
        //put current Speed value to whatever is maxSpeedValue
        currentSpeed = maxSpeed;
        //get component
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //Target player 
        target = GameObject.FindObjectOfType<PlayerMovement>().transform;
        //target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            canPatrol = false;
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
            anim.SetBool("Walk", true);
            ChangeAnim(temp - transform.position);
            rb.MovePosition(temp);
            CanMove();
            if (Vector3.Distance(target.position, transform.position) <= attackRadius) 
            { //StopMoving(); 

                anim.SetBool("Walk", true); 
                if (Time.time - lastAttackTime > attackRate)
                {
                    //Set lastattackTime to the current time of game
                    lastAttackTime = Time.time;
                    //activate attack animation
                    anim.SetTrigger("Attack");
                }
                canPatrol = false;
            }
        }
        else
        {
            canPatrol = true;
            CanMove();
            SetAnimFloat(transform.position);
            anim.SetBool("Walk", true);
        }
    }

    public void StopMoving()
    {
        currentSpeed = 0;
    }

    public void CanMove()
    {
        currentSpeed = maxSpeed;
    }

    public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);

        if (setVector.x == 1 || setVector.x == -1 || setVector.y == 1 || setVector.y == -1)
        {
            anim.SetFloat("LastX", setVector.x);
            anim.SetFloat("LastY", setVector.y);
        }
    }

    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }

        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    public void Patrol()
    {

        if (canPatrol)
        {
            if (canMoveR && !canMoveL)
            {
                MoveToRWayPoint();     
            }
            else
            {
                MoveToLWayPoint();
            }
        }

    }


    public void Flip()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (canMoveR && !canMoveL)
        {
            spriteRenderer.flipX = false;
        }
        else { spriteRenderer.flipX = true; }
    }

    public void MoveToLWayPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointL.position, maxSpeed * Time.deltaTime);
        if (transform.position == waypointL.position)
        {
            canMoveR = true;
            canMoveL = false;
        }
        Flip();
    }
    public void MoveToRWayPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointR.position, maxSpeed * Time.deltaTime);
        if (transform.position == waypointR.position)
        {
            canMoveL = true;
            canMoveR = false;
        }
        Flip();
    }
    

}
