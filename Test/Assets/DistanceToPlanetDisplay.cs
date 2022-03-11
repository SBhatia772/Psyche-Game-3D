using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToPlanetDisplay : MonoBehaviour
{

    public Text myText;
    public GameObject planet;
    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Planet");
    }

    // Update is called once per frame
    void Update()
    {

        myText.text = "Distance To Planet: " + planet.GetComponent<PlanetBehavior>().shipToPlanetDistance.ToString();
    }
}
