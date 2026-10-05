using System;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    [SerializeField]
    private NPC_Interaction _currentNPC;

    public NPC_Interaction GetCurrentNPC()
    {
        return _currentNPC;
    }

    public void SetCurrentNPC(NPC_Interaction npc)
    {
        _currentNPC = npc;
    }

    public void Interact_NPC()
    {
        _currentNPC.Interact();
    }
}
