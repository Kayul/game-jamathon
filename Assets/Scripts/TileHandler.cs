using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    public GameObject tileObject;
    public Vector3 worldOrigin = new Vector3(0,0,0);

    public int gridX = 2;
    public int gridY = 2;

    public List<TileData> tileList;
    public List<PlantData> plantList;

    void Start()
    {
        Debug.Log(TryPlantAt(0, 3, "carrot"));
        Debug.Log(TryPlantAt(0, 2, ""));
        Debug.Log(TryPlantAt(1, 1, "carrot"));
        Debug.Log(TryTillAt(1, 1));
        Debug.Log(TryPlantAt(1, 1, "carrot"));
        Debug.Log(TryTillAt(1, 1));
        Debug.Log(TryTillAt(1, 0));
        Debug.Log(TryTillAt(2, 2));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log(TryHarvestAt(1, 1));
    }

    private PlantData GetPlantData(string plantName)
    {
        foreach (PlantData plant in plantList)
        {
            foreach (string name in plant.plantNames)
            {
                if (name.ToLower().Equals(plantName.ToLower()))
                {
                    return plant;
                }
            }
        }
        return new PlantData() { plant = null };
    }

    private TileData GetTileAt(int x, int y)
    {
        foreach (TileData t in tileList)
        {
            if (t.tileX == x && t.tileY == y)
                return t;
        }
        return new TileData() { tileX = -1, tileY = -1, tileObject = null };
    }

    public int TryPlantAt(int x, int y, string seedType)
    {
        PlantData plantData = GetPlantData(seedType);

        if (!isCoordValid(x, y)) return -1;     // Bad coord

        Tile tile = GetTileAt(x, y).tileObject.GetComponent<Tile>();
        if (plantData.plant == null) return -2; // Bad seed
        if (tile.isOccupied()) return -3;       // Tile already occupied
        if (!tile.isTilled()) return -4;        // Tile is not tilled

        // Set tile to plant
        tile.SetPlant(plantData.plant);

        return 0;
    }


    public int TryHarvestAt(int x, int y)
    {
        if (!isCoordValid(x, y)) return -1;     // Bad coord

        Tile tile = GetTileAt(x, y).tileObject.GetComponent<Tile>();
        if (!tile.isOccupied()) return -2;      // Tile not occupied
        if (!tile.ReadyToHarvest()) return -3;  // Tile not ready for harvest

        tile.HarvestPlant();

        return 0;
    }

    public int TryTillAt(int x, int y)
    {
        if (!isCoordValid(x, y)) return -1; // Bad coord
        Tile tile = GetTileAt(x, y).tileObject.GetComponent<Tile>();
        if (tile.isOccupied()) return -2; // Tile already occupied
        if (tile.isTilled()) return -3;   // Tile is already tilled

        tile.SetTilled();

        return 0;
    }

    public int TryDigAt(int x, int y)
    {
        if (!isCoordValid(x, y)) return -1; // Bad coord
        Tile tile = GetTileAt(x, y).tileObject.GetComponent<Tile>();
        if (!tile.isOccupied()) return -2; // Tile not occupied

        tile.SetEmpty();

        return 0;
    }

    private bool isCoordValid(int x, int y)
    {
        if (x > gridX || x < 0) return false; // Bad coord
        if (y > gridY || y < 0) return false; // Bad coord
        return true;
    }

    [System.Serializable]
    public struct TileData
    {
        public int tileX;
        public int tileY;
        public GameObject tileObject;
    }

    [System.Serializable]
    public struct PlantData
    {
        public List<string> plantNames;
        public GameObject plant;
    }
}
