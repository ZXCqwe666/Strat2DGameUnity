using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Building : MonoBehaviour
{
    public float efficiency = 1f;
    public BuildingData buildingdata;
    public LayerMask building;

    private void Start()
    {
        ChangeEfficiency();
    }
    private void ChangeEfficiency()
    {
        Collider2D[] areaColliders = Physics2D.OverlapCircleAll(transform.position, buildingdata.impactRadius, building);
        foreach (Collider2D areaCollider in areaColliders)
        {
            if (areaCollider.transform.position == transform.position) 
                continue;
            Building building = areaCollider.GetComponent<Building>();
            int id = building.buildingdata.id;
            if (buildingdata.ids.Contains(id)) 
                building.SetEfficiency(buildingdata.efficiencyImpacts[Array.IndexOf(buildingdata.ids, id)]);
        }
    }
    public void SetEfficiency(float increment)
    {
        efficiency += increment;
        Mathf.Clamp(efficiency, 0.25f, 2f);
    }
}