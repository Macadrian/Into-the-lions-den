using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Eye_WakePhantomsAction")]
public class Eye_WakePhantoms : AI_Action
{
    public override void Act(StateController controller)
    {
        WakePhantoms(controller);
    }

    private void WakePhantoms(StateController controller)
    {
        controller.gameManager.awakeGhosts = true;
    }
}
