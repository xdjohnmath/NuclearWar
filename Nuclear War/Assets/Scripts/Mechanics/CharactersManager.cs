using UnityEngine;
using System.Collections;
[System.Serializable]


//O Script tá comentado em ingles e portugues... I'm not even sorry ;) That's how I roll.. Get uset to it
//Enums
public enum creatureAttackType { Tank, LowRange, HighRange };
public enum direction { left = -1, right = 1 };
public enum GoodOrEvil { organel, virus }

public class CharactersManager : MechanicsManager {

    //Variables
    public string creatureName, creatureSpecialAttack;
    Transform raycastPos;
    RaycastHit2D rc;

   [HideInInspector]
    public direction dir;
    public GoodOrEvil goodOrEvil;
    public creatureAttackType type;
    //[HideInInspector] - João, depois que tu acertar todos os valores, tu tira o comment
    public Vector2 startingPos, endingPos;

    [HideInInspector]
    public bool isAttacking, beingAttacked;
    public int creatureCost, creatureLife, creatureAttack, creatureIniciative, creatureAttackRange;
    public float creatureSpeed, pushedBackForce;

    void Start () {
        //pega o transform do objeto filho
        raycastPos = this.gameObject.transform.GetChild (0);
        SettingName ();
        SettingGoodOrEvilType ();
    }

    //Muda o nome do game object pro nome da criatura
    public void SettingName () {
        this.name = creatureName;
    }

    //Detecta as colisões e se deve atacar ou ser atacado
    public void RayCastingMethod () {

        //Chama a função default que setta o tamanho do raycast 
        SettingRayCastSize ();

        //Ele cria uma linha imaginária que pode detectar collider e que começa no centro do objeto e vai até o raycastPos. (que é o objeto vazio filho do character)
        rc = Physics2D.Linecast (transform.position, raycastPos.position);

        //Se for organela e o raycast não tiver pegando em nada, fica azul
        if (rc.collider == null && this.goodOrEvil == GoodOrEvil.organel) {
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.blue);
        }

        //Se for virus e o raycast não tiver pegando em nada, fica cyano
        else if (rc.collider == null && this.goodOrEvil == GoodOrEvil.virus) {
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.cyan);
        }

        //Se o raycast achou um collider e este objeto tá atacando, ele diz quem ataca e muda a cor da linha
        if (rc.collider != null && Attacking()) {
            print (rc.collider.GetComponent<CharactersManager> ().name + " foi atacado por " + this.name);
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.red);
            //Esse cara ataca
        }

        //Se o raycast achou um collider e este objeto tá sendo atacado, ele diz quem tá atacando e muda a cor da linha
        else if (rc.collider != null && BeingAttacked()) {
            print (this.name + " foi atacado por " + rc.collider.GetComponent<CharactersManager> ().name);
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.red);
            //Esse cara foi atacado
        }
    }

    //Default values according to different creature attack type - If game designer doesn't type in an specific value;
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

        //The raycast position and direction will change according to it's size and if it's an organel or virus
        raycastPos.position = new Vector2 ((this.transform.position.x + creatureAttackRange) * (int)dir, this.transform.position.y);
    }

    //Attacking method. Returns true if it's attacking
    public bool Attacking () {

        //Check who has the iniciative, the one whom have it in a bigger value attacks
        if (this.creatureIniciative >= rc.collider.GetComponent<CharactersManager>().creatureIniciative) {
            print (rc.collider.GetComponent<CharactersManager> ().name + " tem " + rc.collider.GetComponent<CharactersManager> ().creatureLife + " de vida");
            //this variable tells if this character moves or not after attack
            this.isAttacking = true;
            //Call Animation - Each different character calls a Different attack in the animation
            return true;
        }
        else {
            //false so it's no longer attacking
            return false;
        }
    }

    //BeingAttacking method. Returns true if it's being attacked
    public bool BeingAttacked () {
        if (rc.collider.GetComponent<CharactersManager>().creatureIniciative >= this.creatureIniciative) {
            //this variavle will help in the animations, if not, just delete it
            this.beingAttacked = true;
            //Pushes back the guy whos being attacked 
            PushedBack ();
            //Call Animation
            return true;
        }
        else {
            //false so it's no longer being attacked
            return false;
        }
    }

    //If the pushes back force it's different than 0, it pushes it back 
    public void PushedBack () {
        if (pushedBackForce != 0) {
            transform.Translate (Vector2.right * pushedBackForce / 2f * (int)dir * -1f);
        }
    }

    //If it's low range, when the animation hits the enemy it decreases the enemy's life in this creature's attack
    public void LowRangeAttack () {
        rc.collider.GetComponent<CharactersManager> ().creatureLife -= this.creatureAttack;
        print (rc.collider.GetComponent<CharactersManager> ().creatureLife);

    }

    public void HighRangeAttack () {
        //instantiate the projectiles - After they collide, subtract life; Animation;
    }

    void Update () {
        //Makes them move
        Movement ();
        //Checks rayCast
        RayCastingMethod ();
    }

    //If this caracter is an organel, he's gonna have different properties than the virus. The direction they move it's an example
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

    //Tell them to move if they're not attacking
    public void Movement () {
        if (!isAttacking || !beingAttacked) {
            transform.Translate (Vector2.right * creatureSpeed/10f * (int) dir);
        }
    }

}
