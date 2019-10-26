using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ResetAction")]
public class ResetAction : AI_Action
{
    public override void Act(StateController controller)
    {
        ResetGame(controller);
    }

    private void ResetGame(StateController controller)
    {
        controller.gameManager.ResetLevel();
        controller.unit.jugadorAlcanzado = false;
    }
}
