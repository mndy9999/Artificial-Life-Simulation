using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Crossover : MonoBehaviour {


    GA_Population population;

    GameObject selectedMaleBunny;
    GameObject selectedFemaleBunny;
    GameObject selectedMaleFox;
    GameObject selectedFemaleFox;

    GameObject newBunnyObject;
    GameObject newFoxObject;

    float time = 0;
    float timeUntilNextSelection = 3;

    private void Start()
    {
        population = gameObject.GetComponent<GA_Population>();

    }

    private void Update()
    {
        selectedMaleBunny = gameObject.GetComponent<GA_Selection>().selectedMaleBunny;
        selectedFemaleBunny = gameObject.GetComponent<GA_Selection>().selectedFemaleBunny;
        selectedMaleFox = gameObject.GetComponent<GA_Selection>().selectedMaleFox;
        selectedFemaleFox = gameObject.GetComponent<GA_Selection>().selectedFemaleFox;

        time += Time.deltaTime;
        if (time > timeUntilNextSelection)
        {
            if (selectedMaleBunny && selectedFemaleBunny) { cross("bunny"); }
            if (selectedMaleFox && selectedFemaleFox) { cross("fox"); }
            timeUntilNextSelection += time;
        }
    }

    void cross(string indiv)
    {
        if(indiv == "bunny")
        {
            newBunnyObject = population.genIndividual("bunny", "random");
            int random;

            random = Random.Range(0, 2);
            if (random == 0) { newBunnyObject.GetComponent<BunnyTraits>().speed = selectedMaleBunny.GetComponent<BunnyTraits>().speed; }
            else if (random == 1) { newBunnyObject.GetComponent<BunnyTraits>().speed = selectedFemaleBunny.GetComponent<BunnyTraits>().speed; }

            random = Random.Range(0, 2);
            if (random == 0) { newBunnyObject.GetComponent<BunnyTraits>().sigth = selectedMaleBunny.GetComponent<BunnyTraits>().sigth; }
            else if (random == 1) { newBunnyObject.GetComponent<BunnyTraits>().sigth = selectedFemaleBunny.GetComponent<BunnyTraits>().sigth; }

            random = Random.Range(0, 2);
            if (random == 0) { newBunnyObject.GetComponent<BunnyTraits>().health = selectedMaleBunny.GetComponent<BunnyTraits>().health; }
            else if (random == 1) { newBunnyObject.GetComponent<BunnyTraits>().health = selectedFemaleBunny.GetComponent<BunnyTraits>().health; }

        }
        else if (indiv == "fox")
        {
            newFoxObject = population.genIndividual("fox", "random");
            int random;


            random = Random.Range(0, 2);
            if (random == 0) { newFoxObject.GetComponent<FoxTraits>().speed = selectedMaleFox.GetComponent<FoxTraits>().speed; }
            else if (random == 1) { newFoxObject.GetComponent<FoxTraits>().speed = selectedFemaleFox.GetComponent<FoxTraits>().speed; }

            random = Random.Range(0, 2);
            if (random == 0) { newFoxObject.GetComponent<FoxTraits>().sight = selectedMaleFox.GetComponent<FoxTraits>().sight; }
            else if (random == 1) { newFoxObject.GetComponent<FoxTraits>().sight = selectedFemaleFox.GetComponent<FoxTraits>().sight; }

            random = Random.Range(0, 2);
            if (random == 0) { newFoxObject.GetComponent<FoxTraits>().attack = selectedMaleFox.GetComponent<FoxTraits>().attack; }
            else if (random == 1) { newFoxObject.GetComponent<FoxTraits>().attack = selectedFemaleFox.GetComponent<FoxTraits>().attack; }

        }

    }

}
