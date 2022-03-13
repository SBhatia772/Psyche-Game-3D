using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public Rigidbody shipRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Collided with "+ collision.gameObject.name);
        LandingMode();
    }

    void LandingMode()
    {
        shipRB.isKinematic = false;
        shipRB.useGravity = true;
        //GetComponent<BoxCollider>().isTrigger = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
