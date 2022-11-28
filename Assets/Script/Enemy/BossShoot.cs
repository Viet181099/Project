using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject theBullet;
    public Transform shootForm;
    public float shootTime;

    float nextShoot = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Time.time > nextShoot)
        {
            
            nextShoot = Time.time + shootTime;
            Instantiate(theBullet, shootForm.position, Quaternion.identity);
                      
        }
    }

    
}
