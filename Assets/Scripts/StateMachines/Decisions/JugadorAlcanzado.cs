using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/JugadorAlcanzado")]
public class JugadorAlcanzado : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.unit.jugadorAlcanzado;
    }
}
