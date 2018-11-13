using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Roulette wheel selection
public class Selection : MonoBehaviour {

    GameObject bunnyParent;
    GameObject male;
    GameObject female;

    List<GameObject> maleBunny;
    List<GameObject> femaleBunny;

    float sum;

    float time = 0.0f;
    public float timeUntilNextSelection = 3.0f;

    private void Start()
    {
        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");

    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time>timeUntilNextSelection)
        {
            calculateSum();
            selectMale();
            selectFemale();
            Debug.Log("Male Selected: " + male.name + "  Female Selected: " + female.name);
            time = 0;
        }

        male = null;
        female = null;
    }

    void calculateSum()
    {
        sum = 0;
        for (int i = 0; i < bunnyParent.transform.childCount; i++)
        {
            sum += bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().fitness;
        }

    }

    GameObject select()
    {
        float rand = Random.Range(0, sum);
        float s = 0;

        for (int i = 0; i < bunnyParent.transform.childCount; i++)
        {
            s += bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>().fitness;
            if (rand < s) { return bunnyParent.transform.GetChild(i).gameObject; }
        }
        return null;
    }

    void selectMale()
    {

        if (GameObject.FindGameObjectsWithTag("male").Length != 0)
        {
            while (!male)
            {
                GameObject selection = select();
                if (selection.tag == "male") { male = selection; }
            }
        }
        else { Debug.Log("All males are dead"); }
    }

    void selectFemale()
    {
        if (GameObject.FindGameObjectsWithTag("female").Length != 0)
        {
            while (!female)
            {
                GameObject selection = select();
                if (selection.tag == "female") { female = selection; }
            }
        }
        else { Debug.Log("All females are dead"); }
    }

    public GameObject getMale() { return male; }
    public GameObject getFemale() { return female; }
}
