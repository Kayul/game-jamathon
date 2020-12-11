using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject currentPlantObject;
    private Vector3 tileOrigin;
    private bool tilled = false;

    public Sprite tilledSprite;
    public Sprite defaultSprite;

    public bool isOccupied() { return currentPlantObject != null; }
    public bool isTilled() { return tilled; }

    void Start()
    {
        tileOrigin = transform.position;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        
    }

    public void HarvestPlant()
    {
        GameObject.Find("TileHandler").GetComponent<GameHandler>().health += currentPlantObject.GetComponent<Plant>().plantHealthRestoreValue;
        SetEmpty();
    }

    public bool ReadyToHarvest()
    {
        return currentPlantObject.GetComponent<Plant>().isReadyForHarvest();
    }

    public void SetPlant(GameObject newPlant)
    {
        currentPlantObject = GameObject.Instantiate(newPlant, tileOrigin + new Vector3(0, 0, 0.01f), Quaternion.identity);
        tilled = false;
    }

    public void SetTilled()
    {
        tilled = true;
        GetComponent<SpriteRenderer>().sprite = tilledSprite;
    }

    public void SetEmpty()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        if (currentPlantObject != null)
            Destroy(currentPlantObject);
    }
}
