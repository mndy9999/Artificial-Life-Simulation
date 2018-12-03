using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Crossover : MonoBehaviour
{
    GA_Population population;

    GameObject selectedMaleBunny;
    GameObject selectedFemaleBunny;
    GameObject selectedMaleFox;
    GameObject selectedFemaleFox;

    GameObject newObj;

    float time = 3;
    public int mutationRate = 30;

    private void Start()
    {
        population = gameObject.GetComponent<GA_Population>();
        mutationRate = 30;
    }

    private void Update()
    {
        //get the selected parents for each species from the Selection script
        selectedMaleBunny = gameObject.GetComponent<GA_Selection>().selectedMaleBunny;
        selectedFemaleBunny = gameObject.GetComponent<GA_Selection>().selectedFemaleBunny;
        selectedMaleFox = gameObject.GetComponent<GA_Selection>().selectedMaleFox;
        selectedFemaleFox = gameObject.GetComponent<GA_Selection>().selectedFemaleFox;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            //if the parents are not null, create offspring through crossover
           // if (selectedMaleBunny && selectedFemaleBunny) { crossover("bunny"); }
           // if (selectedMaleFox && selectedFemaleFox) { crossover("fox"); }
            time = 3;
        }
    }

    void crossover(string indiv)
    {
        newObj = population.genIndividual(indiv, "random");     //create new individual
        newObj.GetComponent<GA_Traits>().isChild = true;        //set is to be a child

        //set male and female parents
        GameObject selectedMale = ((indiv == "bunny") ? gameObject.GetComponent<GA_Selection>().selectedMaleBunny : gameObject.GetComponent<GA_Selection>().selectedMaleFox);
        GameObject selectedFemale = ((indiv == "bunny") ? gameObject.GetComponent<GA_Selection>().selectedFemaleBunny : gameObject.GetComponent<GA_Selection>().selectedFemaleFox);

        int random;
        for(int i = 0; i < newObj.GetComponent<GA_Traits>().traitsArray.Length; i++)
        {
            random = Random.Range(0, (100 + mutationRate));     //generate a random number
            if (random < 50) { newObj.GetComponent<GA_Traits>().traitsArray[i] = selectedMale.GetComponent<GA_Traits>().traitsArray[i]; }       //take trait from the male parent
            else if (random >= 50 && random < 100) { newObj.GetComponent<GA_Traits>().traitsArray[i] = selectedFemale.GetComponent<GA_Traits>().traitsArray[i];  }      //take trait from the female parent
            else { newObj.GetComponent<GA_Traits>().genRandomTrait(i); }       //mutate the trait
        }
    }
}

