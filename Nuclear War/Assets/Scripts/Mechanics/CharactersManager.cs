using UnityEngine;
using System.Collections;
[System.Serializable]


public enum creatureAttackType { Tank, LowRange, HighRange };
public enum direction { left = -1, right = 1 };
public enum GoodOrEvil { organel, virus }

public class CharactersManager : MechanicsManager {

    //Variables
    public string creatureName, creatureSpecialAttack;
    Transform raycastPos;

    [HideInInspector]
    public direction dir;
    public GoodOrEvil goodOrEvil;
    public creatureAttackType type;
    //[HideInInspector] - João, depois que tu acertar todos os valores, tu tira o comment
    public Vector2 startingPos, endingPos;

    [HideInInspector]
    public bool isAttacking;
    public int creatureCost, creatureLife, creatureAttack, creatureIniciative, creatureAttackRange;
    public float creatureSpeed;

    void Start () {
        raycastPos = this.gameObject.transform.GetChild (0);
       // SettingName ();
        SettingGoodOrEvilType ();
        SettingRayCastSize ();
    }

    public void SettingName () {
        this.name = creatureName;
    }

    public void RayCastingMethod () {
        RaycastHit2D rc = Physics2D.Raycast (transform.position, raycastPos.position);

        if (rc.collider == null) {
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.blue);
        }
        else {
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.red);
            Debug.Log (rc.collider.name);
        }

          if (rc.collider != null && this.creatureIniciative > rc.collider.GetComponent<CharactersManager> ().creatureIniciative) {
            print (rc.collider.GetComponent<CharactersManager> ().name + " foi atacado por " + this.name);
            //Esse cara ataca
        }
    }

    public void SettingRayCastSize () {
        if (creatureAttackRange == 0) {
            switch (this.type) {
                case creatureAttackType.Tank:
                    creatureAttackRange = 5;
                break;
                case creatureAttackType.LowRange:
                    creatureAttackRange = 7;
                break;
                case creatureAttackType.HighRange:
                    creatureAttackRange = 20;
                break;
            }
        }
        raycastPos.position = new Vector2 (this.transform.position.x + creatureAttackRange, this.transform.position.y);
    }


    void Update () {
        Movement ();
        RayCastingMethod ();
    }

    public void SettingGoodOrEvilType () {
        switch (goodOrEvil) {
            case GoodOrEvil.organel:
                this.dir = direction.right;
            break;
            case GoodOrEvil.virus:
                this.dir = direction.left;
            break;
        }
        raycastPos.position = new Vector2 (1 * (int)dir, this.transform.position.y);

    }

    public void Movement () {
        if (!isAttacking) {
            transform.Translate (Vector2.right * creatureSpeed/10f * (int) dir);
        }
    }

}
