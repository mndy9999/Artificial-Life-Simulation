using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxTraits : MonoBehaviour {

    public string name;

    int age = 0;
    public float foodLevel = 0;

    public float speed = 0;
    public float sight = 0;
    public float attack = 0;
    public float collect = 0;

    public int birthTime;

    public float fitness;

    private void Start()
    {
        birthTime = (int)Time.realtimeSinceStartup;
        if (name == null) { name = gameObject.name; }

        fitness = 0;

        age = 0;
        foodLevel = 50;
        collect = 0;

        genRandomTraits();

    }

    private void Update()
    {
        ageUp();
    }


    void ageUp()
    {
        age = (int)Time.realtimeSinceStartup - birthTime;
    }

    void genRandomTraits()
    {
        int rand;

        rand = Random.Range(0, 10);
        speed = rand;

        rand = Random.Range(0, 6);
        sight = rand;

        rand = Random.Range(0, 4);
        attack = rand;
    }
}
