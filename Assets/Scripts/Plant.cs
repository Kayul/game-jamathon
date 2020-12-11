using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public string plantName;
    public int plantHealthRestoreValue;
    public float plantGrowthTimePerState;
    public int plantAdultGrowthState = 3; // when the plant is ready to be harvested

    private float plantCurrentGrowthTime = 0;
    private int plantCurrentGrowthState = 0;
    private bool growing = true;

    public List<Sprite> plantSpriteStages;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = plantSpriteStages[0];
    }

    void Update()
    {
        if (growing)
        {
            plantCurrentGrowthTime += Time.deltaTime;
            if (plantCurrentGrowthTime > plantGrowthTimePerState)
            {
                plantCurrentGrowthState++;
                plantCurrentGrowthTime = 0;
                OnGrow();
                Debug.Log($"Plant grown by 1 state. Now state: {plantCurrentGrowthState}");
                if (plantCurrentGrowthState == plantAdultGrowthState)
                {
                    // Stop growing when the current state is the same as adult state
                    growing = false;
                }
            }
        }
    }

    private void OnGrow()
    {
        // Change sprite here
        GetComponent<SpriteRenderer>().sprite = plantSpriteStages[plantCurrentGrowthState];
    }

    public bool isReadyForHarvest()
    {
        return !growing;
    }
}
