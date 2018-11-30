using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GA_Population : MonoBehaviour {

    int numberOfBunnies;
    int numberOfFoxes;

    public GameObject[] bunnyArr;
    public GameObject[] foxArr;

    GameObject bunnyGO;
    GameObject foxGO;

    Tilemap spawnArea;

    GameObject bunnyParent;
    GameObject foxParent;

    float time = 3;

    private void Start()
    {
        numberOfBunnies = 2;
        numberOfFoxes = 2;

        spawnArea = Component.FindObjectOfType<Tilemap>();

        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");
        foxParent = GameObject.FindGameObjectWithTag("Fox Parent");

        for (int i = 0; i < numberOfBunnies; i++) { genIndividual("bunny", "random"); }
        for (int i = 0; i < numberOfFoxes; i++) { genIndividual("fox", "random"); }
    }

    private void Update()
    {
        if (Time.time > time)
        {
            getFittest("bunny", "male");
            getFittest("bunny", "female");
            getFittest("fox", "male");
            getFittest("fox", "female");
            time += Time.time;
        }
        
    }

    public GameObject genIndividual(string indiv, string gender)
    {
        if(indiv == "bunny")
        {
            if(gender == "male")
            {
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                bunnyGO = Instantiate(bunnyArr[0], pos, Quaternion.identity, bunnyParent.transform);
                bunnyGO.name = indiv + bunnyParent.transform.childCount;
            }
            else if(gender == "female")
            {
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                bunnyGO = Instantiate(bunnyArr[1], pos, Quaternion.identity, bunnyParent.transform);
                bunnyGO.name = indiv + bunnyParent.transform.childCount;
            }
            else if(gender == "random")
            {
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                int rand = Random.Range(0, 2);
                bunnyGO = Instantiate(bunnyArr[rand], pos, Quaternion.identity, bunnyParent.transform);
                bunnyGO.name = indiv + bunnyParent.transform.childCount;
            }
            return bunnyGO;
        }
        else if (indiv == "fox")
        {
            
            if (gender == "male")
            {
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                foxGO = Instantiate(foxArr[0], pos, Quaternion.identity, foxParent.transform);
                foxGO.name = indiv + foxParent.transform.childCount;
            }
            else if (gender == "female")
            {
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                foxGO = Instantiate(foxArr[1], pos, Quaternion.identity, foxParent.transform);
                foxGO.name = indiv + foxParent.transform.childCount;
            }
            else if (gender == "random")
            {
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                int rand = Random.Range(0, 2);
                foxGO = Instantiate(foxArr[rand], pos, Quaternion.identity, foxParent.transform);
                foxGO.name = indiv + foxParent.transform.childCount;
            }
            return foxGO;
        }
        return null;
    }

    GameObject getFittest(string indiv, string gender)
    {
        GameObject fittest = null;
        float current = 0;
        if(indiv == "bunny")
        {
            if(gender == "male")
            {
                for(int i = 0; i < bunnyParent.transform.childCount; i++)
                {
                    if(bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && bunnyParent.transform.GetChild(i).tag == "male")
                    {
                        current = bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                        fittest = bunnyParent.transform.GetChild(i).gameObject;
                    }
                }
            }
            else if(gender == "female")
            {
                for(int i = 0; i < bunnyParent.transform.childCount; i++)
                {
                    if (bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && bunnyParent.transform.GetChild(i).tag == "female")
                    {
                        current = bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                        fittest = bunnyParent.transform.GetChild(i).gameObject;
                    }
                }
            }
        }
        else if(indiv == "fox")
        {
         
            if (gender == "male")
            {
                
                for (int i = 0; i < foxParent.transform.childCount; i++)
                {
                    
                    if (foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && foxParent.transform.GetChild(i).tag == "male")
                    {
                        current = foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                        fittest = foxParent.transform.GetChild(i).gameObject;
                    }
                }
            }
            else if (gender == "female")
            {
                for (int i = 0; i < foxParent.transform.childCount; i++)
                {
                    if (foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && foxParent.transform.GetChild(i).tag == "female")
                    {
                        current = foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                        fittest = foxParent.transform.GetChild(i).gameObject;
                    }
                }
            }
        }
        return fittest;
    }


}
