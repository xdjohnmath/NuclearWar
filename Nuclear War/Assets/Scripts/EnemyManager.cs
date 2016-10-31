using UnityEngine;
using System.Collections;

public class EnemyManager : CharactersManager {

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

        //Initialing variable. The "instance." is used to say we're referring to this object instead of the CharactersManager class.
        instance.energyTime = 1;

    }

    void Update () {
        EnergyPlayer (1, 1);

    }
}
