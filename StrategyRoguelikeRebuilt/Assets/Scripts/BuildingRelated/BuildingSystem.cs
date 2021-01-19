using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    private Camera mainCam;
    private GameObject buildingPrefab;
    private Transform buildingBlueprint;
    private SpriteRenderer blueprintRenderer;

    private BuildingData selectedBuilding;
    private Vector2 cellPosition, previousCellPosition, cellOffset;
    private bool blueprintActive, canPlaceBuilding;

    public LayerMask requiredGroundLayer, placingBlockerLayer;

    private void Start()
    {
        InitializeBuildingSystem();
    }
    private void Update()
    {
        if(blueprintActive)
            UpdateBlueprintPosition();

        if (Input.GetMouseButtonDown(0)) // TEMPORARY CONTROLS
        {
            SpawnBuilding(cellPosition);
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) // // TEMPORARY CONTROLS
        {
            ExitBuildingMode();
        }
    }
    private void SpawnBuilding(Vector2 position)
    {
        if (blueprintActive && canPlaceBuilding)
        {
            GameObject building = Instantiate(buildingPrefab, position + new Vector2(0f, -0.25f), Quaternion.identity, transform); // adding Vector2(0f, -0.25f)
            building.GetComponent<SpriteRenderer>().sprite = selectedBuilding.buildingSprite;
            building.GetComponent<BoxCollider2D>().size = selectedBuilding.buildingSize;
            building.GetComponent<Building>().buildingdata = selectedBuilding;

            if (Input.GetKey(KeyCode.LeftShift) == false) // TEMPORARY CONTROLS
                ExitBuildingMode();
        }
    }
    private void UpdateBlueprintPosition()
    {
        Vector2 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        cellPosition = new Vector2(Mathf.FloorToInt(mousePosition.x / 1f), Mathf.FloorToInt(mousePosition.y / 1f));
        cellPosition += cellOffset;
        buildingBlueprint.position = cellPosition;

        if (cellPosition != previousCellPosition)
            CheckPlacing();
        previousCellPosition = cellPosition;
    }
    private void CheckPlacing()
    {
        canPlaceBuilding = true;

        if (Physics2D.OverlapArea(cellPosition - cellOffset + Vector2.one * 0.1f, cellPosition + cellOffset - Vector2.one * 0.1f, placingBlockerLayer))
        {
            canPlaceBuilding = false;
        }
        for (int i = 0; i < selectedBuilding.buildingSize.x; i++)
        {
            if (Physics2D.OverlapPoint(cellPosition - cellOffset + new Vector2(0.5f + i, -0.75f), requiredGroundLayer) == false)
            {
                canPlaceBuilding = false;
            }
        }
        if (canPlaceBuilding)
            blueprintRenderer.color = new Color(0f, 0.5f, 0f);
        else blueprintRenderer.color = new Color(0.5f, 0f, 0f);
    }
    public void UpdateBlueprint(int index) // HOOK IT UP WITH UI SYSTEM   CALL IT FROM INSTANCE
    {
        selectedBuilding = Database.instance.buildings[index];
        cellOffset = new Vector2(selectedBuilding.buildingSize.x / 2, selectedBuilding.buildingSize.y / 2);

        blueprintRenderer.sprite = selectedBuilding.buildingSprite;
        blueprintActive = true;
    }
    private void ExitBuildingMode()
    {
        blueprintRenderer.sprite = null;
        blueprintActive = false;
    }
    private void InitializeBuildingSystem()
    {
        mainCam = Camera.main;
        buildingPrefab = Resources.Load<GameObject>("building");
        buildingBlueprint = transform.Find("buildingBlueprint");
        blueprintRenderer = buildingBlueprint.GetComponent<SpriteRenderer>();
    }
}
