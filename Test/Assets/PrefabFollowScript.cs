using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFollowScript : MonoBehaviour
{
    public Transform shipTransform;
    // Start is called before the first frame update
    void Start()
    {
        shipTransform = FindObjectOfType<VattalusSpaceshipController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = shipTransform.position;
    }
}
