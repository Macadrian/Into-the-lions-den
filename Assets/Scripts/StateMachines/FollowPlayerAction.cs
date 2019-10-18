using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FollowPlayerAction")]
public class FollowPlayerAction : AI_Action
{
    public override void Act(StateController controller)
    {
        FollowPlayer(controller);
    }

    private void FollowPlayer(StateController controller)
    {
        var unit = controller.unit;
        unit.target = controller.playerTransform;

    }
}
