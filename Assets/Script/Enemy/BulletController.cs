using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeedHigh;
    public float bulletSpeedLow;
    public float bulletAngle;
    Rigidbody2D bulletRB;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
         bulletRB.AddForce(new Vector2(Random.Range(-bulletAngle, bulletAngle), Random.Range(bulletSpeedLow, bulletSpeedHigh)), ForceMode2D.Impulse);
        //bulletRB.velocity = transform.right * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
