using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private Camera mainCam;
    private Transform toolHolder;
    private SpriteRenderer toolSprite;
    private Vector3 mouseViewportPos;

    private void Start()
    {
        IntializeToolManager();
    }
    private void IntializeToolManager() 
    {
        mainCam = Camera.main;
        toolHolder = transform.Find("toolHolder");
        toolSprite = toolHolder.transform.Find("tool").GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        mouseViewportPos = mainCam.ScreenToViewportPoint(Input.mousePosition) * 2f - Vector3.one;
        if (mouseViewportPos.x > 0f)
            toolSprite.flipY = false;
        else
            toolSprite.flipY = true;
        Vector2 aimDirection = (mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        toolHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
