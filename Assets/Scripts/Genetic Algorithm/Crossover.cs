using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossover : MonoBehaviour {

    GameObject male;
    GameObject female;
    GameObject offspring;

    GameObject eventSystem;

    float time = 0;
    float timeUntilNextSelection = 3.1f;

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
            
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeUntilNextSelection)
        {
            male = gameObject.GetComponent<Selection>().male;
            female = gameObject.GetComponent<Selection>().female;
            createOffSpring();
            time = 0;
            
        }
        
        
    }

    void cross()
    {
        int rand;

        rand = Random.Range(0, 2);
        if (rand == 0) { offspring.GetComponent<BunnyTraits>().gender = BunnyTraits.Gender.male; }
        else { offspring.GetComponent<BunnyTraits>().gender = BunnyTraits.Gender.female; }

        rand = Random.Range(0, 2);
        if (rand == 0) { offspring.GetComponent<BunnyTraits>().speed = male.GetComponent<BunnyTraits>().speed; }
        else { offspring.GetComponent<BunnyTraits>().speed = female.GetComponent<BunnyTraits>().speed; }

        rand = Random.Range(0, 2);
        if (rand == 0) { offspring.GetComponent<BunnyTraits>().sigth = male.GetComponent<BunnyTraits>().sigth; }
        else { offspring.GetComponent<BunnyTraits>().sigth = female.GetComponent<BunnyTraits>().sigth; }
    }

    void createOffSpring()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            offspring = eventSystem.GetComponent<RandomSpawn>().spawnBunny("male");
            
        }
        else if(rand == 1){
            offspring = eventSystem.GetComponent<RandomSpawn>().spawnBunny("female");
        }
        if (offspring) { cross(); }
    }
}
