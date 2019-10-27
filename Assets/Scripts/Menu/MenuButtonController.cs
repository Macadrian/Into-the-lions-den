using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int startIndex;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis ("Vertical") != 0)
        {
            if (!keyDown){
				if (Input.GetAxis ("Vertical") < 0) {
					if(index < maxIndex){
						index++;
					}else{
						index = startIndex;
					}
				} else if(Input.GetAxis ("Vertical") > 0){
					if(index > startIndex){
						index --; 
					}else{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}else{
			keyDown = false;
		}
	}

}
