using UnityEngine;
using System.Collections;

public class PlayerContorller : MonoBehaviour {

	private Animation _animation = null;

	void Awake () {

		_animation = this.GetComponent<Animation> ();
	}

	// Use this for initialization
	void Start () {
	
		_animation.wrapMode = WrapMode.Loop;
		_animation.Play ("run");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
