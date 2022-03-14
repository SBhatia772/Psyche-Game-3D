using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{

    GameObject shipCollider;
    GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Space Ship");
        shipCollider = GameObject.Find("Space Ship Collider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        Vector3 positionBeforeCollision = shipCollider.transform.position;

        if (collision.gameObject.name == shipCollider.name)
        {
            shipCollider.transform.position = positionBeforeCollision;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        Vector3 positionBeforeCollision = shipCollider.transform.position;

        if (other.tag.Equals("Player"))
        {
            shipCollider.transform.position = positionBeforeCollision;
            ship.transform.position = positionBeforeCollision;
        }
    }



}
