using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowShip : MonoBehaviour
{
    public Transform ship;
    public Rigidbody shipRB;
    public float crashSpeed;
    public Text speedText;
    public Text fuelText;

    // Start is called before the first frame update
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + ship.GetComponent<Rigidbody>().velocity.magnitude;
        fuelText.text = "Fuel: " + ship.GetComponent<VattalusSpaceshipController>().fuel;
        transform.position = ship.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            shipRB.isKinematic = false;
            shipRB.useGravity = true;

            if(shipRB.velocity.magnitude > crashSpeed)
            {
                print("ship crashed");
            }
            


        }
    }
}
