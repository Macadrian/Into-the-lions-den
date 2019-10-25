using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/PlayerVisible")]
public class PlayerVisible : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.vision.IsTargetVisible(controller.playerTransform) && controller.timeWaited == 0;
    }
}
