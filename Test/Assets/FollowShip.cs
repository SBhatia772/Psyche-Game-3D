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
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + ship.GetComponent<Rigidbody>().velocity.magnitude * Mathf.Sign(shipRB.velocity.y);
        fuelText.text = "Fuel: " + ship.GetComponent<VattalusSpaceshipController>().fuel;
        transform.position = ship.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            shipRB.isKinematic = false;
            shipRB.useGravity = true;

            if(shipRB.velocity.magnitude > crashSpeed || Vector3.Dot(ship.transform.up, Vector3.down) > 0.2f)
            {
                //print("ship crashed");
                FindObjectOfType<LevelController>().LoseLevel();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            


        }
    }
}
