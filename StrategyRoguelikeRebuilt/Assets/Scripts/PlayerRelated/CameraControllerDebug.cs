using UnityEngine;

public class CameraControllerDebug : MonoBehaviour
{
    public static CameraControllerDebug instance;
    private Camera mainCam;

    private Vector3 target, velocity;
    private const float smoothTime = 0.05f;
    private readonly float cameraSpeed = 24f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        UpdateCameraPosition();
    }
    private void UpdateCameraPosition()
    {
        Vector3 mousePosition = mainCam.ScreenToViewportPoint(Input.mousePosition) * 2f - Vector3.one;
        float cameraMovement = cameraSpeed * Time.deltaTime;

        if (mousePosition.x > 0.95f || Input.GetKey(KeyCode.RightArrow))
            target += Vector3.right * cameraMovement;
        else if (mousePosition.x < -0.95f || Input.GetKey(KeyCode.LeftArrow))
            target += Vector3.left * cameraMovement;

        if (mousePosition.y > 0.95f || Input.GetKey(KeyCode.UpArrow))
            target += Vector3.up * cameraMovement;
        else if (mousePosition.y < -0.95f || Input.GetKey(KeyCode.DownArrow))
            target += Vector3.down * cameraMovement;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
