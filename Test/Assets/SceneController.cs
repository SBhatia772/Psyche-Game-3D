using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    public GameObject ship;
    public GameObject playerPrefab;
    public Canvas distanceToShip;
    private GameObject prefab;
    private TextMeshProUGUI distanceToShipText;
    private TextMeshProUGUI shipPositionText;
    private TextMeshProUGUI playerPositionText;

    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<VattalusSpaceshipController>().gameObject;
        Vector3 playerPosition = new Vector3(ship.transform.position.x+10,ship.transform.position.y, ship.transform.position.z);
        prefab = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        prefab.SetActive(true);
        Destroy(ship.GetComponent<VattalusSpaceshipController>());
        ship.GetComponent<Rigidbody>().isKinematic = true;
        //distanceToShip = FindObjectOfType<Canvas>();
        distanceToShipText = GameObject.Find("Distance From Ship Text").GetComponent<TextMeshProUGUI>();
        shipPositionText = GameObject.Find("Ship Postion Text").GetComponent<TextMeshProUGUI>();
        playerPositionText = GameObject.Find("Player Position Text").GetComponent<TextMeshProUGUI>();


        
    }

    // Update is called once per frame
    void Update()
    {
        distanceToShipText.SetText("Distance From Ship: " + Vector3.Distance(ship.transform.position, prefab.transform.position).ToString());
        shipPositionText.SetText("Ship Position: " + ship.transform.position.ToString());
        playerPositionText.SetText("Player Position: " + prefab.transform.position.ToString());
        
    }
}
