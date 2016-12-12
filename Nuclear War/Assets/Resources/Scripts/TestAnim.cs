using UnityEngine;
using System.Collections;

public class TestAnim : MonoBehaviour {

	private Animator a;

	void Start () {
		a = GetComponent<Animator> ();
	}
	

	void Update () {
	
		if (Input.GetKey(KeyCode.Space)){
			a.SetInteger ("component", 1);
		}

		else if (Input.GetKey(KeyCode.B)){
			a.SetInteger ("component", 3);
		}

	}

	public void Walking () {
		a.SetInteger ("component", 2);
	}

	public void Attacking () {
		a.SetInteger ("component", 4);
	}
}
