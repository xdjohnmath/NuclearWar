using UnityEngine;
using System.Collections;
[System.Serializable]

public class MechanicsManager : MonoBehaviour{

    //All properties
    public int life { get; set; }
    public int energy { get; set; }
    public float energyTime { get; set; }


    //value to be added and how long you must wait
    public int Energy (int value, float rate) {
        if (energyTime >= rate) {
            this.energy += value;
            energyTime = 0;
            return this.energy;
        }
        else {
            energyTime += Time.deltaTime;
            return this.energy;
        }
    }

   

    

}
