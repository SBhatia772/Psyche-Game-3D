using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject ship;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<VattalusSpaceshipController>().gameObject;
        Vector3 playerPosition = new Vector3(ship.transform.position.x+10,ship.transform.position.y, ship.transform.position.z);
        GameObject prefab = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        prefab.SetActive(true);
        Destroy(ship.GetComponent<VattalusSpaceshipController>());
        ship.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
