using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landinGearScript : MonoBehaviour
{

    public bool landed = false;
    public Transform ship;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<VattalusSpaceshipController>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //ship.position = new Vector3(ship.position.x, ship.position.y + 1f, ship.position.z); 

        Debug.Log("Some Kind of Collision detected from: " + gameObject.name);

        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Collided Into Terrain");
            landed = true;
        }
        else
            landed = false;
    }


}
