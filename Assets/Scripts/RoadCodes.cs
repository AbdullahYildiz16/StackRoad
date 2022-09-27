using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCodes : MonoBehaviour
{
    RoadCodes roadCodesScript;
    GameObject currentRoad;
    float xDistance = 3;
    float xSide;
    public float roadSpeed;



    void Start()
    {
        
        roadCodesScript = gameObject.GetComponent<RoadCodes>();
        currentRoad = GameObject.FindGameObjectWithTag("CurrentRoad");
       xSide  = Random.Range(0,2);
        if (xSide == 1f)
        {
            // 0 --> Cube is going right to left
            // 1 --> Cube is going left to right
            xDistance = -xDistance;
        }
        if (gameObject.CompareTag("Road"))
        {
            transform.position = transform.position + Vector3.right * xDistance;
        }
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            roadSpeed = 0;

            float hangover = transform.position.x - currentRoad.transform.position.x;// hangover = |2x - y|  
            float direction = hangover > 0 ? 1f : -1f;

            SplitRoad(hangover, direction);

            currentRoad.tag = "Road";
            gameObject.tag = "CurrentRoad";

            roadCodesScript.enabled = false;

        }
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * -xDistance, roadSpeed * Time.deltaTime);
        
        
    }
    void SplitRoad(float hangover, float direction)
    {
        float newXSize = currentRoad.transform.localScale.x - Mathf.Abs(hangover); // newXSize = y
        float fallingBlockXSize = transform.localScale.x - newXSize;

        float newXPosition = currentRoad.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        
        float fallingBlockXPosition = transform.position.x + (newXSize / 2f * direction) + (fallingBlockXSize / 2f * direction);
        
        SpawnExtraRoad(fallingBlockXSize, fallingBlockXPosition);

    } 
   
    
    
    void SpawnExtraRoad(float fallingBlockXSize, float fallingBlockXPosition)
    {
        
        
        GameObject original = Instantiate(gameObject, new Vector3(fallingBlockXPosition, transform.position.y, transform.position.z),
            Quaternion.identity);
        original.gameObject.tag = "ExtraRoad";
        original.transform.localScale = new Vector3(fallingBlockXSize, transform.localScale.y, transform.localScale.z);
        original.transform.GetComponent<Rigidbody>().useGravity = true;
        original.transform.GetComponent<Rigidbody>().isKinematic = false;

    }
}
