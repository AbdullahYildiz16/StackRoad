using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCodes : MonoBehaviour
{
    GameObject currentRoad;
    float xDistance = 3;
    float xSide;
    float hangover;
    public float roadSpeed;
    bool canMove = true;


    void Start()
    {
        currentRoad = GameObject.FindGameObjectWithTag("CurrentRoad");
       xSide  = Random.Range(0,2);
        if (xSide == 1f)
        {
            xDistance = -xDistance;
        }

        transform.position = transform.position + Vector3.right * xDistance;
    }

    
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            canMove = false;
            hangover = currentRoad.transform.position.x - gameObject.transform.position.x;

        }
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * -xDistance, roadSpeed * Time.deltaTime);
        }
        
    }
   /* void SplitRoad(float hangover)
    {
        float newXSize = transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = currentRoad.transform.localScale.x - newXSize;

        float newXPosition = transform.position.x + (hangover / 2);
    } */
}
