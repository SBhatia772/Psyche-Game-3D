using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGearFollowShip : MonoBehaviour
{
    public Transform ship;
    private Vector3 originalPosition;
    private Vector3 difference;
    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<VattalusSpaceshipController>().gameObject.transform;
        originalPosition = transform.position;
        difference = ship.position - originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(difference.x +  ship.position.x, difference.y + ship.position.y, difference.z + ship.position.z);
        Debug.Log("New Position: " + transform.position.ToString());
    }
}
