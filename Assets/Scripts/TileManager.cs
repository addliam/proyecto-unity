using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float tileLength = 3.5f;
    public int numberOfTiles = 3;
    public int totalNumOfTiles = 3;
    public float zSpawn = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    private int previousIndex;

    private int getRandomNumberTile(){
        // omitir tile 0 por ser sin obstaculo.
        return Random.Range(1, tilePrefabs.Length);
    }
    void Start()
    {;
        SpawnTile(0);
        // Saltearse contador de 0. que es la pista base sin nada
        for (int i = 1; i < numberOfTiles; i++) {
            SpawnTile(getRandomNumberTile());
        }
    }
    
    void Update()
    {
        if (playerTransform.position.z - 30 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(getRandomNumberTile());
            DeleteTile();
        }
    }

    public void SpawnTile(int index = 0)
    {
        GameObject go = Instantiate(tilePrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn+=tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}