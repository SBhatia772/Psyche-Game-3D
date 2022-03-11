using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{

    public Text text;
    public GameObject planet;
    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Speed: " + planet.GetComponent<PlanetBehavior>().shipSpeed.ToString();
    }
}
