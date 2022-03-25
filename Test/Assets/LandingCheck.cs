using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandingCheck : MonoBehaviour
{
    private int gearsOnGround = 0;
    public GameObject landingGearFront;
    public GameObject landingGearBackLeft;
    public GameObject landingGearBackRight;

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
    public bool landingGearOnGround = false;
    public GameObject explosionPrefab;
    public GameObject textSpawner;
    public List<GameObject> landingGears;
    private bool playerWasGivenPoints = false;

    // Start is called before the first frame update
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody>();
        GameOver = ship.GetComponent<VattalusSpaceshipController>().levelController.GetComponent<LevelController>().gameOver;
        //textSpawner = FindObjectOfType<TextSpawner>().gameObject;
        landingGears = ship.gameObject.GetComponent<VattalusSpaceshipController>().landingGearColliders;
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

        if (GameOver == false && ship.GetComponent<VattalusSpaceshipController>().fuel == 0)
        {
            FindObjectOfType<LevelController>().LoseLevel();
        }
    }

    IEnumerator die()
    {
        points = 0;
        BoxCollider shipCollider = GameObject.Find("Space Ship Collider").GetComponent<BoxCollider>();
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, shipCollider.size.x) + shipRB.position.x, Random.Range(0, shipCollider.size.y) + shipRB.position.y, Random.Range(0, shipCollider.size.z) + shipRB.position.z);
            Instantiate(explosionPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        ship.gameObject.SetActive(false);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3f);
        DontDestroyOnLoad(ship.gameObject);
        Destroy(ship.gameObject.GetComponent<LevelController>());
        ship.gameObject.GetComponent<VattalusSpaceshipController>().DeployRamp();
        SceneManager.LoadScene(2);

    }

    IEnumerator doubleCheck()
    {

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);

            if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
            {
                FindObjectOfType<LevelController>().LoseLevel();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(die());
            }
        }

        
    }

    IEnumerator spawnText(string pointText)
    {
        Debug.Log("Couroutine Started!");
        BoxCollider shipCollider = GameObject.Find("Space Ship Collider").GetComponent<BoxCollider>();

        yield return new WaitForSeconds(.1f);
        Vector3 randomPosition = new Vector3(Random.Range(0, shipCollider.size.x) + shipRB.position.x, Random.Range(0, shipCollider.size.y) + shipRB.position.y, Random.Range(0, shipCollider.size.z) + shipRB.position.z);
        GameObject temp = Instantiate(textSpawner, randomPosition, Quaternion.identity);
        temp.GetComponent<TextSpawner>().pointText = pointText;
        temp.SetActive(true);

        yield return new WaitForSeconds(3f);
        Destroy(temp);
    }

    private bool GearsLanded()
    {
        /*
        foreach(landinGearScript gear in FindObjectsOfType<landinGearScript>())
        {
            if (!gear.landed) return false;
        }*/

        for (int i = 0; i < landingGears.Count; i++)
        {
            Debug.Log("Examining " + landingGears[i].gameObject.name);
            if (landingGears[i].GetComponent<landinGearScript>().landed == false)
            {
                Debug.Log(landingGears[i].gameObject.name.ToString() + "is not on ground!");
                return false;
            }
        }

        Debug.Log("All Gears Landed");
        return true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
            {
                FindObjectOfType<LevelController>().LoseLevel();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(die());
            }
            else
                StartCoroutine(doubleCheck());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ContactPoint[] contact = new ContactPoint[1];
        collision.GetContacts(contact);


        landinGearScript gear = contact[0].thisCollider.GetComponent<landinGearScript>();


        if (gear == null) return;

        //Debug.Log("It is a Gear Collider");
        if (gear.landed == true)
        {
            //Debug.Log("Gear landed false now set to true");
            gearsOnGround--;
            gear.landed = false;

        }

        playerWasGivenPoints = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Landing Site"))
        {
            Debug.Log("Landing Site Detected");
            isLandingSite = true;
            earnablePoints = other.gameObject.GetComponent<LandingSiteScript>().points;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Landing Site"))
        {
            Debug.Log("Landing Site Detected");
            isLandingSite = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Entered!");

        ContactPoint[] contact = new ContactPoint[1];
        collision.GetContacts(contact);


        landinGearScript gear = contact[0].thisCollider.GetComponent<landinGearScript>();


        if (gear == null) return;

        //Debug.Log("It is a Gear Collider");
        if (gear.landed == false)
        {
            //Debug.Log("Gear landed false now set to true");
            gearsOnGround++;
            gear.landed = true;

        }

        

        if (collision.gameObject.CompareTag("Terrain"))
        {
            //Debug.Log("Terrain Detected!");
            if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
            {
                FindObjectOfType<LevelController>().LoseLevel();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(die());
            }
            else if (isLandingSite == false)
            {
                //Debug.Log("Normal Terrain");

                if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
                {
                    FindObjectOfType<LevelController>().LoseLevel();
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                    StartCoroutine(die());
                }
                else
                {
                    if (GearsLanded() && gearsOnGround == 3 && GameOver == false && playerWasGivenPoints == false)
                    {
                        Debug.Log("Touch Down!");
                        print("number of legs on ground " + gearsOnGround.ToString());
                        print("Incrementing 100 points from " + gear.gameObject.name.ToString());
                        points += 100;
                        
                        StartCoroutine(spawnText("1x"));
                        playerWasGivenPoints = true;
                    }

                }

            }
            else
            {
                if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
                {
                    FindObjectOfType<LevelController>().LoseLevel();
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                    StartCoroutine(die());
                }
                else
                {

                    if (GearsLanded() && gearsOnGround == 3 && GameOver == false && playerWasGivenPoints == false)
                    {
                        points += earnablePoints;

                        StartCoroutine(spawnText(earnablePoints.ToString().Replace("00", "x")));
                        playerWasGivenPoints=true;
                        StartCoroutine(ChangeScene());
                    }

                }
            }

        }
    }
}
