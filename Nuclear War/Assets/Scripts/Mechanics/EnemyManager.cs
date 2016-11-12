using UnityEngine;
using System.Collections;

public class EnemyManager : MechanicsManager {

    public int[] yPos = new int[4];

    //Singleton
    public static EnemyManager instance = null;

    void Awake () {

        //Singleton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy (gameObject);
        }
        //EndOfSingleton
    }


    void Update () {

    }
}
