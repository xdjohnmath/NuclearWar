using UnityEngine;
using System.Collections;
[System.Serializable]

public class MechanicsManager : MonoBehaviour{

    //constructor
    public MechanicsManager () {
        this.energyTime = 1;
    }

    //All properties
    public int life { get; set; }
    public int energy { get; set; }
    public float energyTime { get; set; }

    //value to be added and how long you must wait - Will be overriden in the other classes
    public void Energy (int objectEnergy, int value, float rate) {
        energyTime -= Time.deltaTime;
        if (energyTime < 0) {
            objectEnergy += value;
            energyTime = rate;
        }
    }



   

    

}
