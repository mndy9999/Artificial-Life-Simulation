using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomSpawn : MonoBehaviour {

    public float nextActionTime;
    public int numberOfBunnies;
    public Tilemap spawnArea;
    public Tile bushTile;
    public GameObject bushGO;
    public GameObject bunnyGO;

    public List <Tile> grassArr;

    float time;
    Vector3Int v;

	// Use this for initialization
	void Start () {
        time = 0;
        grassArr = new List<Tile>();
    
        for(int i = 0; i < numberOfBunnies; i++)
        {
            spawnBunny();
        }

    }
	
	// Update is called once per frame
	void Update () {
       
		if(Time.time > time)
        {
            time += nextActionTime;
            spawnBush();
        }
        
    }

    void spawnBunny()
    {
        v = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
        Instantiate(bunnyGO, v, Quaternion.identity);
    }

    void spawnBush()
    {
        v = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
        Instantiate(bushGO, v, Quaternion.identity);

    }
}
