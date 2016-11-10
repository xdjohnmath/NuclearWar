using UnityEngine;
using System.Collections;

public class CallEventAnimation : MonoBehaviour {

    Animator anim;

    void Awake () {
        anim = GetComponent<Animator> ();
        anim.SetInteger ("life", gameObject.GetComponentInChildren<CharactersManager> ().creatureLife);
        anim.SetBool ("shot", false);
        anim.SetBool ("spawned", false);
    }

    void Update () {

        switch (gameObject.GetComponentInChildren<CharactersManager>().type) {
            case creatureAttackType.Tank:
                anim.SetInteger ("type", 2);
            break;
            case creatureAttackType.LowRange:
                anim.SetInteger ("type", 0);
            break;
            case creatureAttackType.HighRange:
                anim.SetInteger ("type", 1);
            break;
        }
    }

    public void Dead () {
        Destroy (gameObject);
    }

    public void ResetAnimationLowRange () {
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("move", true);
        anim.SetBool ("beingAttacked", false);
        anim.SetBool ("shot", false);
        gameObject.GetComponentInChildren<CharactersManager> ().move = true;
    }

    public void ResetAnimationHighRange () {
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("beingAttacked", false);
        anim.SetBool ("shot", false);
        gameObject.GetComponentInChildren<CharactersManager> ().move = true;
    }

    public void ResetMove () {
        if (anim.GetComponentInChildren<CharactersManager> ().move == false) {
            anim.GetComponentInChildren<CharactersManager> ().move = true;         
        }
    }

    public void LowRange () {
        gameObject.GetComponentInChildren<CharactersManager> ().LowRangeAttack ();

    }

    public void HighRange () {
        gameObject.GetComponentInChildren<CharactersManager> ().HighRangeAttack ();
        anim.SetBool ("shot", true);
    }
}
