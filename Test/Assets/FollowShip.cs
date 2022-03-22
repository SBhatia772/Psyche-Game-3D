using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject textSpawner;


    // Start is called before the first frame update
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody>();
        GameOver = ship.GetComponent<VattalusSpaceshipController>().levelController.GetComponent<LevelController>().gameOver;
        textSpawner = FindObjectOfType<TextSpawner>().gameObject;
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
        points = 0;
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

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3f);
        DontDestroyOnLoad(ship.gameObject);
        Destroy(ship.gameObject.GetComponent<LevelController>());
        ship.gameObject.GetComponent<VattalusSpaceshipController>().DeployRamp();
        SceneManager.LoadScene(2);
       
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
            {
                if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
                {
                    FindObjectOfType<LevelController>().LoseLevel();
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                    StartCoroutine(die());
                }
                else
                {
                    if(Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
                    {
                        FindObjectOfType<LevelController>().LoseLevel();
                        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                        StartCoroutine(die());
                    }
                    else
                    {
                        points += 100;
                        StartCoroutine(spawnText("1x"));
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
                    points += earnablePoints;

                    switch (earnablePoints)
                    {
                        case 200:
                            StartCoroutine(spawnText("2x"));
                            break;
                        case 300:
                            StartCoroutine(spawnText("3x"));
                            break;
                        case 400:
                            StartCoroutine(spawnText("4x"));
                            break;
                        case 500:
                            StartCoroutine(spawnText("5x"));
                            break;
                        case 600:
                            StartCoroutine(spawnText("6x"));
                            break;
                        default:
                            StartCoroutine(spawnText("1x"));
                            break;
                    }

                    StartCoroutine(ChangeScene());
                }
            }

        }

        if(other.gameObject.CompareTag("Landing Site"))
        {
            isLandingSite = true;
            earnablePoints = other.gameObject.GetComponent<LandingSiteScript>().points;
        }
    }
}
