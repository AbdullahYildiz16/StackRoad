using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraSpeed;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, cameraSpeed*Time.deltaTime);
    }
}
