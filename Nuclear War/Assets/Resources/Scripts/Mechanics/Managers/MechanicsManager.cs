using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]

public class MechanicsManager : MonoBehaviour {

    //All properties
    public int life { get; set; }
    public int energy { get; set; }
    public float energyTime { get; set; }
    public GameObject[] prefabsArray { get; set; }
    public float startingPos { get; set; }
    public float endingPos { get; set;}
    public GameObject selectedCharacter { get; set; }
    [HideInInspector]
    public int[] yPosition = new int[] { 39, 23, 7, -9, -25 };

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

    public float PinningThemInTheYAxis () {
        float temp = 0;
        for (int i = 0; i < yPosition.Length; i++) {
            if (this.transform.position.y == yPosition[i]) {
                temp = yPosition[i];
            }
        }
        return temp;
    }

    public void CheckingEnergyToButtons (Button[] b) {
        for (int i = 0; i < prefabsArray.Length; i++) {
            if (prefabsArray[i].GetComponentInChildren<CharactersManager> ().creatureCost > this.energy) {
                b[i].interactable = false;
            }
            else {
                b[i].interactable = true;
            }
        }
    }

    public virtual void SelectingCharacter () {
       
    }

    public virtual void SelectingStartingPosition () {

    }
}
