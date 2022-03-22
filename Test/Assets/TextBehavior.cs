using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float wavyness;
    public string pointMultiplierText;
    // Start is called before the first frame update
    void Start()
    {
        pointMultiplierText = FindObjectOfType<TextBehavior>().pointMultiplierText;
        moveSpeed = Random.Range(.1f, .5f);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, moveSpeed, 0f);
        transform.position += new Vector3(Mathf.Sin(Time.time) * wavyness, 0, 0);
    }
}
