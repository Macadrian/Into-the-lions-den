﻿using UnityEngine;

public abstract class AI_Action : ScriptableObject
{
    public abstract void Act(StateController controller);
}
