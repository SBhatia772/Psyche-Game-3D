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
    public int points;
    private bool GameOver;
    public bool landingGearOnGround = false;
    public GameObject explosionPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        shipRB = ship.GetComponent<Rigidbody>();
        GameOver = ship.GetComponent<VattalusSpaceshipController>().levelController.GetComponent<LevelController>().gameOver;
        //textSpawner = FindObjectOfType<TextSpawner>().gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position = ship.position;
        GameOver = ship.GetComponent<VattalusSpaceshipController>().levelController.GetComponent<LevelController>().gameOver;
        
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
    
    /* void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            if (shipRB.velocity.magnitude > crashSpeed || Mathf.Cos(Vector3.Angle(ship.transform.up, Vector3.down) * Mathf.Deg2Rad) >= -0.9f)
            {
                GameOver = false;
                FindObjectOfType<LevelController>().LoseLevel();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                StartCoroutine(die());
            }
        }
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            //Debug.Log("Terrain Detected!");
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
}
