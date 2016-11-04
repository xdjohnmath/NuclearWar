using UnityEngine;
using System.Collections;

public class EnemyManager : MechanicsManager {


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
