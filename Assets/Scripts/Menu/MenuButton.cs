using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

	[SerializeField] GameManager manager;
	[SerializeField] GameObject otherMenu;
	[SerializeField] GameObject selfMenu;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1){
				animator.SetBool ("pressed", true);
			}else if (animator.GetBool ("pressed"))
            {
                animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
                switch (menuButtonController.index)
                {
                    case 0: //New game
                        manager.CambiarEscena();
                        break;

                    case 1: //Options
                        otherMenu.SetActive(true);
                        selfMenu.SetActive(false);
                        break;

                    case 2://Quit
                        manager.Salir();
                        break;

                    case 3: //Back
                        otherMenu.SetActive(true);
                        selfMenu.SetActive(false);
                        break;
                }
            }
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
