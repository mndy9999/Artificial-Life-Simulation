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
    public GameObject bushGO;

    Tilemap spawnArea;

    GameObject bunnyParent;
    GameObject foxParent;

    float time = 0.5f;

    private void Start()
    {
        //how many bunnies and foxes to spawn
        numberOfBunnies = 10;
        numberOfFoxes = 5;

        spawnArea = Component.FindObjectOfType<Tilemap>();

        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");
        foxParent = GameObject.FindGameObjectWithTag("Fox Parent");

        //spawn initial population
        for (int i = 0; i < numberOfBunnies; i++) { genIndividual("bunny", "random"); }
        for (int i = 0; i < numberOfFoxes; i++) { genIndividual("fox", "random"); }
    }

    private void Update()
    {
        //get fittest male and female from each species every 'time' seconds
        time -= Time.deltaTime;
 
        if(time <= 0)
        {
            spawnBush();
            time = 0.5f;
        }
    }
    /// <summary>
    /// Instantiates and returns a new gameobject
    /// </summary>
    /// <param name="indiv">Species</param>
    /// <param name="gender">Gender</param>
    /// <returns></returns>
    public GameObject genIndividual(string indiv, string gender)
    {
        //check species and gender
        if(indiv == "bunny")
        {
            if(gender == "male")
            {
                //initialize new random position on the map
                Vector3Int pos = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
                bunnyGO = Instantiate(bunnyArr[0], pos, Quaternion.identity, bunnyParent.transform);    //instantiate new gameobject
                bunnyGO.name = indiv + bunnyParent.transform.childCount;    //set the name to keep count of the number of individuals
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
    /// <summary>
    /// Return fittest individual
    /// </summary>
    /// <param name="indiv">Species</param>
    /// <param name="gender">Gender</param>
    /// <returns></returns>
    GameObject getFittest(string indiv, string gender)
    {
        GameObject fittest = null;
        float current = 0;

        //check for species and gender
        if(indiv == "bunny")
        {
            if(gender == "male")
            {
                for(int i = 0; i < bunnyParent.transform.childCount; i++)   //go through all the individuals in the population
                {   //if the fitness is higher than the current fitness and the genders are matching
                    if(bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.male)
                    {
                        current = bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;  //set current to the individual's fitness
                        fittest = bunnyParent.transform.GetChild(i).gameObject;                         //set fittest to the current individual
                    }
                }
            }
            else if(gender == "female")
            {
                for(int i = 0; i < bunnyParent.transform.childCount; i++)
                {
                    if (bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.female)
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
                    
                    if (foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && foxParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.male)
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
                    if (foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness > current && foxParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.female)
                    {
                        current = foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                        fittest = foxParent.transform.GetChild(i).gameObject;
                    }
                }
            }
        }
        return fittest;
    }

    public void spawnBush()
    {
        Vector3Int v = new Vector3Int(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2), Random.Range(-spawnArea.size.y / 2, spawnArea.size.y / 2), 0);
        Instantiate(bushGO, v, Quaternion.identity);

    }
}
