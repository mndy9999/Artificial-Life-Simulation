using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCheck : MonoBehaviour {

    public Tilemap tileMap;
    public GameObject player;

    public Tile groundGreen;
    public Tile bush;

    Vector3Int playerPos;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        playerPos = new Vector3Int(Mathf.FloorToInt(player.transform.position.x), Mathf.FloorToInt(player.transform.position.y), 0);
        Debug.Log(tileMap.GetTile(playerPos).name);

        if (tileMap.GetTile(playerPos) == bush)
        {
            tileMap.SetTile(playerPos, null);
        }
	}
}
