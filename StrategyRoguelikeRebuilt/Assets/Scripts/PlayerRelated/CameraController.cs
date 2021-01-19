using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Camera mainCam;
    private Transform player;

    private Vector3 target, velocity;
    private const float smoothTime = 0.05f;
    

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainCam = Camera.main;
        player = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        UpdateCameraPosition();
    }
    private void UpdateCameraPosition()
    {
        target = player.position;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
