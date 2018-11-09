using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomSpawn : MonoBehaviour {

    public float nextActionTime;
    public Tilemap spawnArea;
    public Tile bush;

    public List <Tile> grassArr;

    float time;
    Vector3Int v;

	// Use this for initialization
	void Start () {
        time = 0;
        grassArr = new List<Tile>();
	}
	
	// Update is called once per frame
	void Update () {
       
		if(Time.time > time)
        {
            time += nextActionTime;
            spawnBush();
        }
        
    }

    void spawnBush()
    {
        v = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
        if(spawnArea.GetTile(v) != bush)
        {
            spawnArea.SetTile(v, bush);
            grassArr.Add((Tile)spawnArea.GetTile(v));

        }
        else
        {
            Debug.Log("Grass is there already");
        }
    }
}
