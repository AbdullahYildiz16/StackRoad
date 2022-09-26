using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject road;
    int roadAmount = 1;
    [SerializeField] float playerSpeed;
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward*playerSpeed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            Instantiate(road, road.transform.position + Vector3.forward*roadAmount*2, Quaternion.identity);
            roadAmount++;
        }
    }
}
