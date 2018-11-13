using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitness : MonoBehaviour {

    GameObject bunnyParent;
    List<BunnyTraits> bunnyTraits;

    float[] speed;
    float[] sigth;
    float[] collect;
    float[] fitness;

    void Start () {
        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");
    }
	
	void Update () {
        speed = new float[bunnyParent.transform.childCount];
        sigth = new float[bunnyParent.transform.childCount];
        collect = new float[bunnyParent.transform.childCount];
        fitness = new float[bunnyParent.transform.childCount];
        for (int i = 0; i < bunnyParent.transform.childCount; i++)
        {
            speed[i] = bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().speed;
            sigth[i] = bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().sigth;
            collect[i] = bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().collectScore;
            fitness[i] = bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().fitness;

        }
        calculateFitness();
	}

    void calculateFitness()
    {
        Debug.Log("Calulating Fitness....");
        for(int i = 0; i < bunnyParent.transform.childCount; i++)
        {
            fitness[i] = (speed[i] + sigth[i] + collect[i]) / 3;
            bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().fitness = fitness[i];
        }
    }
}
