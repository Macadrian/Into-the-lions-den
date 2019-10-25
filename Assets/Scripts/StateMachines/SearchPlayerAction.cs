using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SearchPlayerAction")]
public class SearchPlayerAction : AI_Action
{
    public override void Act(StateController controller)
    {
        SearchPlayer(controller);
    }

    private void SearchPlayer(StateController controller)
    {
        controller.Wait();
        controller.unit.target = controller.unit.transform;
    }
}

