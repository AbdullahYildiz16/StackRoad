using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCodes : MonoBehaviour
{
    RoadCodes roadCodesScript;
    Material roadMaterial;
    GameObject currentRoad;
    float xDistance = 3;
    float xSide;
    public float roadSpeed;



    void Start()
    {
        roadMaterial = gameObject.GetComponent<MeshRenderer>().material;
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
            transform.localScale = new Vector3(currentRoad.transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
            if (Mathf.Abs(hangover) >= (transform.localScale.x + currentRoad.transform.localScale.x) / 2)
            {
                gameObject.SetActive(false);
            }
            else
            {
                SplitRoad(hangover, direction);
            }

            

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

        GameObject extraRoad = GameObject.CreatePrimitive(PrimitiveType.Cube);
        extraRoad.transform.position = new Vector3(fallingBlockXPosition, transform.position.y, transform.position.z);
        extraRoad.transform.localScale = new Vector3(fallingBlockXSize, transform.localScale.y, transform.localScale.z);
        extraRoad.GetComponent<MeshRenderer>().material = roadMaterial;
        extraRoad.AddComponent<Rigidbody>();
        extraRoad.transform.GetComponent<Rigidbody>().useGravity = true;
        extraRoad.transform.GetComponent<Rigidbody>().isKinematic = false;

    }
}
