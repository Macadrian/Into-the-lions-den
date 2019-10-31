using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Eye_SleepPhantomsAction")]
public class Eye_SleepPhantoms : AI_Action
{
    public override void Act(StateController controller)
    {
        SleepPhantoms(controller);
    }

    private void SleepPhantoms(StateController controller)
    {
        controller.gameManager.awakeGhosts = false;
    }
}
