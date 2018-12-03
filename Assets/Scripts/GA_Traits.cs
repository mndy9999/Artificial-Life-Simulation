using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Traits : MonoBehaviour {

    public string individualName;

    public int age = 0;
    public float foodLevel = 0;

    public float speed = 0;     //how far they move
    public float sight = 0;     //how far they see
    public float spec = 0;      //health for rabbits, attack for foxes **not fully implemented**

    public bool isChild = false;

    public float collect = 0;   //how many times they ate

    public int birthTime;
    public float fitness;

    public float[] traitsArray;
    
    public enum Gender { male, female};
    public Gender gender;

    public string foodSource;
    float hunger;   //how much food they lose every frame

    private void Awake()
    {
        birthTime = (int)Time.realtimeSinceStartup;

        genGender();
        setFoodSource();
        setHunger();

        foodLevel = 60;
        traitsArray = new float[3];

        if (!isChild)
        {
            genRandomTrait(0);
            genRandomTrait(1);
            genRandomTrait(2);
        }

        traitsArray[0] = speed;
        traitsArray[1] = sight;
        traitsArray[2] = spec;
 
    }

    private void Update()
    {
        ageUp();
        
        speed = traitsArray[0];
        sight = traitsArray[1];
        spec = traitsArray[2];

        foodLevel -= hunger;
        checkIfDead();
    }

    void ageUp() { age = (int)Time.realtimeSinceStartup - birthTime; }
    void checkIfDead() { if(age > 100 || foodLevel <= 0) { Destroy(gameObject); } }
    
    public bool canMate() { return foodLevel > 50; }

    public void genRandomTrait(int index)
    {
        int rand;
        if (index == 0)
        {
            rand = Random.Range(0, 6);
            speed = rand;
            traitsArray[index] = rand;
        }
        else if (index == 1)
        {
            rand = Random.Range(0, 6);
            sight = rand;
            traitsArray[index] = rand;
        }
        else if (index == 2)
        {
            rand = Random.Range(0, 4);
            spec = rand;
            traitsArray[index] = rand;
        }
    }

    public void genGender()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0) { gender = Gender.male; }
        else { gender = Gender.female; }
    }

    public void setFoodSource()
    {
        if (gameObject.tag == "bunny") { foodSource = "bush"; }
        else if(gameObject.tag == "fox") { foodSource = "bunny"; }
    }

    public void setHunger()
    {
        if (gameObject.tag == "bunny") { hunger = 0.01f; }
        else if (gameObject.tag == "fox") { hunger = 0.05f; }
    }
}
