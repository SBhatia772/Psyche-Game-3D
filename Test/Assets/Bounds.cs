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

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        Vector3 positionOfColliderBeforeCollision = shipCollider.transform.position;
        Vector3 positionOfShipBeforeCollision = ship.transform.position;

        if (other.tag.Equals("Player"))
        {
            shipCollider.transform.position = positionOfColliderBeforeCollision;
            ship.transform.position = positionOfShipBeforeCollision;
        }
    }



}
