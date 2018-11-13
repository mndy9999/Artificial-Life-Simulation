using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossover : MonoBehaviour {

    GameObject male;
    GameObject female;
    GameObject offspring;

    float time = 0;
    float timeUntilNextSelection = 3.1f;

    private void Start()
    {
        male = GetComponent<Selection>().getMale();
        female = GetComponent<Selection>().getFemale();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeUntilNextSelection)
        {
           // cross();
            createOffSpring();
            Debug.Log("Crossover");
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
            GetComponent<RandomSpawn>().spawnBunny("male", offspring);
            
        }
        else{
            GetComponent<RandomSpawn>().spawnBunny("female", offspring);
        }
        cross();
    }
}
