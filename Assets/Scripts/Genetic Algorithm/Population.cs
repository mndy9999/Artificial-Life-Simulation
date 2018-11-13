using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population : MonoBehaviour {

    public int numberOfBunnies;
    public GameObject[] bunnyModels;

    GameObject bunnyGO;
    GameObject bunnyParent;
    Tilemap spawnArea;

    GameObject fittest;



    private void Start()
    {

        spawnArea = Component.FindObjectOfType<Tilemap>();
        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");
        createInitialPopulation();
    }


    void createInitialPopulation()
    {
        for(int i = 0; i < numberOfBunnies; i++)
        {
            int rand = Random.Range(0, bunnyModels.Length);
            Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
            bunnyGO = Instantiate(bunnyModels[rand], pos, Quaternion.identity, bunnyParent.transform);
            bunnyGO.name = "Offspring" + bunnyParent.transform.childCount;
        }
    }



    GameObject getFittest()
    {
        float current = 0;
        for(int i = 0; i < bunnyParent.transform.childCount; i++)
        {
            if (bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().fitness > current)
            {
                current = bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().fitness;
                fittest = bunnyParent.transform.GetChild(i).gameObject;
            }
        }
        
        return fittest;
    }
}
