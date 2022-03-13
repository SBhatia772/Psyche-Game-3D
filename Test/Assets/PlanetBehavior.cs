using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetBehavior : MonoBehaviour
{
    public GameObject ship;

    private GameObject debrisGrid;
    private GameObject atmosphere;

    bool movingToPlanet;

    public float shipSpeed;
    public float shipToPlanetDistance;

    public void StartMovingToPlanet()
    {
        movingToPlanet = true;
    }

    public bool isMovingToPlanet()
    {
        return movingToPlanet;
    }

    //this shoud trigger the transition of fading into one coloer and loading the planet scene
    void StartEnteringAtmosphere()
    {
        //probably a transition coroutine
        print("Entering Atmosphere!");
    }

    private void Start()
    {
        atmosphere = GameObject.Find("Atmosphere");
        ship = GameObject.Find("Space Ship");

        atmosphere.SetActive(false);    
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Landing Scene");
    }

    private void Update()
    {
        shipSpeed = ship.GetComponent<VattalusSpaceshipController>().speed; //tie this to your ships speed
        shipToPlanetDistance = Vector3.Distance(transform.position, ship.transform.position);


        if(shipToPlanetDistance < 600 && ship.GetComponent<VattalusSpaceshipController>().fuel > 0)
        {
            //change scene probably using a couroutine
        }


        if(shipToPlanetDistance <= 2000)
        {
            StartEnteringAtmosphere();
        }
        if (shipToPlanetDistance < 3000)
        {

            //atmosphere.SetActive(true);
            atmosphere.SetActive(true);
            //start triggering particles and cloud effects. beeping etc
        }
        if (shipToPlanetDistance < 4000)
        {
            //initial particles and shaking, start to make sounds
        }
    }
}