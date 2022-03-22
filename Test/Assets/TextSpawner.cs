using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public float range;
    public float frequencyMin;
    public float frequencyMax;
    public GameObject textPrefab;
    public string pointText;
    

    private void Start()
    {
        StartCoroutine("SpawnNumbers");
    }

    IEnumerator SpawnNumbers()
    {
        Vector3 randomPos = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
        randomPos += transform.position;
        GameObject temp = Instantiate(textPrefab, randomPos, Quaternion.identity);
        temp.SetActive(true);
        temp.GetComponent<TextMesh>().text = pointText;
        yield return new WaitForSeconds(Random.Range(frequencyMin, frequencyMax));
        StartCoroutine("SpawnNumbers");
    }
}
