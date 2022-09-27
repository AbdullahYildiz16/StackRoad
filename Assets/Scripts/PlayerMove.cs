using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject[] roadList;
    int roadAmount = 1;
    [SerializeField] float playerSpeed;
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward*playerSpeed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road") || other.CompareTag("CurrentRoad"))
        {
            int i = Random.Range(0, roadList.Length);
            Instantiate(roadList[i], roadList[i].transform.position + Vector3.forward*roadAmount*2, Quaternion.identity);
            roadAmount++;
        }
    }
}
