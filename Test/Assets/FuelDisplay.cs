using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDisplay : MonoBehaviour
{
    public Text myText;
    public GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Space Ship");
    }

    // Update is called once per frame
    void Update()
    { 
        myText.text = "Fuel: " + ship.GetComponent<VattalusSpaceshipController>().fuel.ToString();
    }
}
