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
    public Text pointText;
    public int points;
    private int earnablePoints;
    private bool GameOver;
    private bool isLandingSite = false;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody>();
        GameOver = ship.GetComponent<VattalusSpaceshipController>().levelController.GetComponent<LevelController>().gameOver;
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + ship.GetComponent<Rigidbody>().velocity.magnitude * Mathf.Sign(shipRB.velocity.y);
        fuelText.text = "Fuel: " + ship.GetComponent<VattalusSpaceshipController>().fuel;
        pointText.text = "Points: " + points.ToString();
        transform.position = ship.position;
        GameOver = ship.GetComponent<VattalusSpaceshipController>().levelController.GetComponent<LevelController>().gameOver;
        //print(Vector3.Angle(ship.transform.up, Vector3.down));
        //print("Angle between transform up and down: " + Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad));
    }

    IEnumerator die()
    {
        BoxCollider shipCollider = GameObject.Find("Space Ship Collider").GetComponent<BoxCollider>();
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, shipCollider.size.x) + shipRB.position.x, Random.Range(0, shipCollider.size.y) + shipRB.position.y, Random.Range(0, shipCollider.size.z) + shipRB.position.z);
            Instantiate(explosionPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        ship.gameObject.SetActive(false); 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {

            if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
            {
                FindObjectOfType<LevelController>().LoseLevel();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(die());
            }
            else if (isLandingSite == false)
                points += 100;
            else
            {
                points += earnablePoints;
                //Start some couroutine to change scene to first person walking around terrain
            }

        }

        if(other.gameObject.CompareTag("Landing Site"))
        {
            isLandingSite = true;
            earnablePoints = other.gameObject.GetComponent<LandingSiteScript>().points;
        }
    }
}
