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

    GameObject newBunnyObject;
    GameObject newFoxObject;
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
        selectedMaleBunny = gameObject.GetComponent<GA_Selection>().selectedMaleBunny;
        selectedFemaleBunny = gameObject.GetComponent<GA_Selection>().selectedFemaleBunny;
        selectedMaleFox = gameObject.GetComponent<GA_Selection>().selectedMaleFox;
        selectedFemaleFox = gameObject.GetComponent<GA_Selection>().selectedFemaleFox;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            if (selectedMaleBunny && selectedFemaleBunny) { crossover("bunny"); }
            if (selectedMaleFox && selectedFemaleFox) { crossover("fox"); }
            time += 3;
        }
    }

    void cross(string indiv)
    {
        if (indiv == "bunny")
        {
            newBunnyObject = population.genIndividual("bunny", "random");
            int random;

            random = Random.Range(0, (100 + mutationRate));
            Debug.Log(random);
            if (random < 50) { newBunnyObject.GetComponent<GA_Traits>().speed = selectedMaleBunny.GetComponent<GA_Traits>().speed; }
            else if (random >= 50 && random < 100) { newBunnyObject.GetComponent<GA_Traits>().speed = selectedFemaleBunny.GetComponent<GA_Traits>().speed; }
            else { newBunnyObject.GetComponent<GA_Traits>().speed = Random.Range(0, 10); Debug.Log("Mutation" + random); }

            random = Random.Range(0, (100 + mutationRate));
            Debug.Log(random);
            if (random < 50) { newBunnyObject.GetComponent<GA_Traits>().sight = selectedMaleBunny.GetComponent<GA_Traits>().sight; }
            else if (random >= 50 && random < 100) { newBunnyObject.GetComponent<GA_Traits>().sight = selectedFemaleBunny.GetComponent<GA_Traits>().sight; }
            else if (random >= 100) { newBunnyObject.GetComponent<GA_Traits>().sight = Random.Range(0, 6); Debug.Log("Mutation" + random); }

            random = Random.Range(0, (100 + mutationRate));
            Debug.Log(random);
            if (random < 50) { newBunnyObject.GetComponent<GA_Traits>().spec = selectedMaleBunny.GetComponent<GA_Traits>().spec; }
            else if (random >= 50 && random < 100) { newBunnyObject.GetComponent<GA_Traits>().spec = selectedFemaleBunny.GetComponent<GA_Traits>().spec; }
            else { newBunnyObject.GetComponent<GA_Traits>().spec = Random.Range(0, 5); Debug.Log("Mutation" + random); }

        }
        else if (indiv == "fox")
        {
            newFoxObject = population.genIndividual("fox", "random");
            int random;

            random = Random.Range(0, (100 + mutationRate));
            Debug.Log(random);
            if (random < 50) { newFoxObject.GetComponent<GA_Traits>().speed = selectedMaleFox.GetComponent<GA_Traits>().speed; }
            else if (random >= 50 && random < 100) { newFoxObject.GetComponent<GA_Traits>().speed = selectedFemaleFox.GetComponent<GA_Traits>().speed; }
            else { newBunnyObject.GetComponent<GA_Traits>().speed = Random.Range(0, 10); Debug.Log("Mutation" + random); }

            random = Random.Range(0, (100 + mutationRate));
            Debug.Log(random);
            if (random < 50) { newFoxObject.GetComponent<GA_Traits>().sight = selectedMaleFox.GetComponent<GA_Traits>().sight; }
            else if (random > 50 && random < 100) { newFoxObject.GetComponent<GA_Traits>().sight = selectedFemaleFox.GetComponent<GA_Traits>().sight; }
            else { newBunnyObject.GetComponent<GA_Traits>().sight = Random.Range(0, 6); Debug.Log("Mutation" + random); }

            random = Random.Range(0, (100 + mutationRate));
            Debug.Log(random);
            if (random < 50) { newFoxObject.GetComponent<GA_Traits>().spec = selectedMaleFox.GetComponent<GA_Traits>().spec; }
            else if (random >= 50 && random < 100) { newFoxObject.GetComponent<GA_Traits>().spec = selectedFemaleFox.GetComponent<GA_Traits>().spec; }
            else { newBunnyObject.GetComponent<GA_Traits>().spec = Random.Range(0, 5); Debug.Log("Mutation" + random); }
        }
    }

    void crossover(string indiv)
    {
        newObj = population.genIndividual(indiv, "random");
        newObj.GetComponent<GA_Traits>().isChild = true;

        GameObject selectedMale = ((indiv == "bunny") ? gameObject.GetComponent<GA_Selection>().selectedMaleBunny : gameObject.GetComponent<GA_Selection>().selectedMaleFox);
        GameObject selectedFemale = ((indiv == "bunny") ? gameObject.GetComponent<GA_Selection>().selectedFemaleBunny : gameObject.GetComponent<GA_Selection>().selectedFemaleFox);

         Debug.Log("Parents: " + selectedMale + " + " + selectedFemale);
        int random;
        for(int i = 0; i < newObj.GetComponent<GA_Traits>().traitsArray.Length; i++)
        {

            random = Random.Range(0, (100 + mutationRate));
            if (random < 50) { newObj.GetComponent<GA_Traits>().traitsArray[i] = selectedMale.GetComponent<GA_Traits>().traitsArray[i]; Debug.Log("Trait"+i+" from father: " + selectedMale.GetComponent<GA_Traits>().traitsArray[i] + " " + newObj.GetComponent<GA_Traits>().traitsArray[i]); }
            else if (random >= 50 && random < 100) { newObj.GetComponent<GA_Traits>().traitsArray[i] = selectedFemale.GetComponent<GA_Traits>().traitsArray[i]; Debug.Log("Trait" + i + " from mother: " + selectedFemale.GetComponent<GA_Traits>().traitsArray[i] + " " + newObj.GetComponent<GA_Traits>().traitsArray[i]); }
            else { newObj.GetComponent<GA_Traits>().genRandomTrait(i); Debug.Log("Muatation" + i); }
        }
    }
}

