using UnityEngine;
using System.Collections;

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
    public GameObject shot;

    public direction dir;
    public GoodOrEvil goodOrEvil;
    public creatureAttackType type;

    public bool isAttacking, beingAttacked, move;
    public int creatureCost, creatureLife, creatureAttack, creatureIniciative, creatureAttackRange;
    public float creatureSpeed, pushedBackForce, shotSpeed;

    void Awake () {
        //pega o transform do objeto filho
        try {
            isAttacking = false;
            raycastPos = this.gameObject.transform.GetChild (0);
            SettingName ();
            SettingGoodOrEvilType ();
            SettingGoodOrEvilPositions ();
            SettingTypeCharacteristics ();
            RandomizingValuesOffset ();

        }
        catch (System.Exception e){
            Debug.LogException (e, this);
        }

    }

    public void SettingGoodOrEvilPositions () {
        if (this.goodOrEvil == GoodOrEvil.organel) {
            this.startingPos = 89;
            this.endingPos = this.startingPos * -1;
            this.transform.position = new Vector2 (this.startingPos, this.transform.position.y);
            this.transform.position = new Vector2 (this.endingPos, this.transform.position.y);
        }
        else {
            this.startingPos = -89;
            this.endingPos = this.startingPos * -1;
            this.transform.position = new Vector2 (this.startingPos, this.transform.position.y);
            this.transform.position = new Vector2 (this.endingPos, this.transform.position.y);
        }
    }

    public void RandomizingValuesOffset () {
        this.creatureAttack = Random.Range ((int)(this.creatureAttack / 2f + 1), this.creatureAttack + 1);
        this.creatureIniciative = Random.Range ((int)(this.creatureIniciative / 2f + 1), this.creatureIniciative + 1);
        this.creatureLife = Random.Range ((int)(this.creatureLife / 2f + 1), this.creatureLife + 1);
        this.creatureSpeed = Random.Range (this.creatureSpeed / 2f + 1, this.creatureSpeed + 1);
        this.pushedBackForce = Random.Range (this.pushedBackForce / 2f + 1, this.pushedBackForce + 1);
        this.shotSpeed = Random.Range (this.shotSpeed / 2f + 1, this.shotSpeed + 1);

    }

    public void SettingTypeCharacteristics () {
        switch (this.type) {
            case creatureAttackType.Tank:
            break;
            case creatureAttackType.LowRange:
            break;
            case creatureAttackType.HighRange:
                ShotHighRangeProperties ();
            break;
            default:
            break;
        }
    }

    //Muda o nome do game object pro nome da criatura
    public void SettingName () {
        creatureName = this.name;
    }

    //Detecta as colisões e se deve atacar ou ser atacado
    public void RayCastingMethod () {

        //Chama a função default que setta o tamanho do raycast 
        SettingRayCastSize ();

        //Ele cria uma linha imaginária que pode detectar collider e que começa no centro do objeto e vai até o raycastPos. (que é o objeto vazio filho do character)
        rc = Physics2D.Linecast (transform.position, raycastPos.position);

        //Se for organela e o raycast não tiver pegando em nada, fica azul
        if (rc.collider == null && this.goodOrEvil == GoodOrEvil.organel) {
            move = true;
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.blue);
        }

        if (rc.collider == null && this.goodOrEvil == GoodOrEvil.virus) {
            move = true;
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.blue);
        }

        if (rc.collider != null && this.goodOrEvil == rc.collider.GetComponent<CharactersManager>().goodOrEvil) {
            move = true;
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.cyan);
        }

        //Se o raycast achou um collider e este objeto tá atacando, ele diz quem ataca e muda a cor da linha
        if (rc.collider != null && this.goodOrEvil != rc.collider.GetComponent<CharactersManager> ().goodOrEvil) {
            Attacking ();
            rc.collider.GetComponent<CharactersManager> ().BeingAttacked ();
            print (rc.collider.GetComponent<CharactersManager> ().name + " atacou " + this.name);
            Debug.DrawLine (this.transform.position, raycastPos.position, Color.red);
            //Esse cara ataca
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
                creatureAttackRange = 10;
                break;
            }
        }

        //The raycast position and direction will change according to it's size and if it's an organel or virus
        if (this.goodOrEvil == GoodOrEvil.organel) {
            raycastPos.position = new Vector2 (this.transform.position.x + creatureAttackRange, this.transform.position.y);
        }
        else {
            raycastPos.position = new Vector2 (this.transform.position.x - creatureAttackRange, this.transform.position.y);
        }
    }

    //Attacking method. Returns true if it's attacking
    public void Attacking () {
        move = false;
        switch (this.type) {
            case creatureAttackType.LowRange:
                if (this.creatureIniciative > rc.collider.GetComponent<CharactersManager> ().creatureIniciative) {
                    this.isAttacking = true;
                    rc.collider.GetComponent<CharactersManager> ().beingAttacked = true;
                }
                else if (this.creatureIniciative == rc.collider.GetComponent<CharactersManager> ().creatureIniciative && this.goodOrEvil == GoodOrEvil.organel) {
                    this.isAttacking = true;
                    rc.collider.GetComponent<CharactersManager> ().beingAttacked = true;
                }
                else if (this.creatureIniciative < rc.collider.GetComponent<CharactersManager> ().creatureIniciative) {
                    this.beingAttacked = true;
                    rc.collider.GetComponent<CharactersManager> ().isAttacking = true;
                }
            break;
            case creatureAttackType.HighRange:
                this.isAttacking = true;
                rc.collider.GetComponent<CharactersManager> ().beingAttacked = true;
            break;
        }
    }

    //BeingAttacking method. Returns true if it's being attacked
    public bool BeingAttacked () {
        move = false;
        if (this.beingAttacked == true) {
            //Call Animation
            PushedBack ();
            return true;
        }
        else {
            //false so it's no longer being attacked
            this.beingAttacked = false;
            return false;
        }
    }

    //If the pushes back force it's different than 0, it pushes it back 
    public void PushedBack () {
        if (this.pushedBackForce != 0) {
            transform.Translate (Vector2.right * this.pushedBackForce / 2f * (int)this.dir * -1f);
        }
    }

    //If it's low range, when the animation hits the enemy it decreases the enemy's life in this creature's attack
    public void LowRangeAttack () {
        rc.collider.GetComponent<CharactersManager> ().creatureLife -= this.creatureAttack;
        print (rc.collider.GetComponent<CharactersManager> ().creatureLife);

    }

    public void HighRangeAttack () {
        //instantiate the projectiles - After they collide, subtract life; Animation;
        Instantiate (shot, this.gameObject.transform.position, Quaternion.identity);

    }

    public void TankAttack () {

    }

    public void ShotHighRangeProperties () {
        shot.gameObject.GetComponent<ShotsManager> ().shotSpeed = this.shotSpeed;
        shot.gameObject.GetComponent<ShotsManager> ().shotAttack = this.creatureAttack;
        shot.gameObject.GetComponent<ShotsManager> ().shotDir = this.dir;
    }

    void Update () {
        //Makes them move
        Movement ();
        //Checks rayCast
        RayCastingMethod ();
        DestroyCharacter ();
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
       raycastPos.position = new Vector2 (raycastPos.position.x *(int)this.dir, this.transform.position.y);

    }

    public void DestroyCharacter () {
        if (this.creatureLife <= 0) {
           Destroy (this.gameObject);
        }
        if (this.goodOrEvil == GoodOrEvil.organel && this.gameObject.transform.position.x > 89) {
            PlayerManager.instance.life += this.creatureCost;
            Destroy (this.gameObject);
        }
        else if (this.goodOrEvil == GoodOrEvil.virus && this.gameObject.transform.position.x < -89) {
            EnemyManager.instance.life += this.creatureCost;
            Destroy (this.gameObject);
        }
    }

    //Tell them to move if they're not attacking
    public void Movement () {
        if (move) {
            transform.Translate (Vector2.right * creatureSpeed * .01f * (int)this.dir);
        }
    }

}
