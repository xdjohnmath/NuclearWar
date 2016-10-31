using UnityEngine;
using System.Collections;

public class CharactersManager : MonoBehaviour{

    //All properties
    public int creatureCost { get; set; }
    public int playerLife { get; set; }
    public int creatureLife { get; set; }
    public int creatureAttack { get; set; }
    public float creatureSpeed { get; set; }
    public int playerEnergy { get; set; }
    public int enemyEnergy { get; set; }
    public string creatureName { get; set; }
    public string creatureSpecialAttack { get; set; }
    public enum creatureType {Tank, LowRange, HighRange};
    public int creatureIniciative { get; set; }
    public float energyTime { get; set; }

    //value to be added and how long you must wait
    public void EnergyPlayer (int value, float rate) {
        energyTime -= Time.deltaTime;
        if (energyTime < 0) {
            playerEnergy += value;
            energyTime = rate;
        }
    }

    //value to be added and how long you must wait
    public void EnergyEnemy (int value, float rate) {
        energyTime -= Time.deltaTime;
        if (energyTime < 0) {
            enemyEnergy += value;
            energyTime = rate;
        }
    }


}
