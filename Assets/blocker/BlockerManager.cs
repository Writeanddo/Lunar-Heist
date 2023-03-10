using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockerManager : MonoBehaviour
{
    public Dictionary<BlockerName, bool> Blockers = new Dictionary<BlockerName, bool>(){
        { BlockerName.WYNN_DOOR_1, false },
        { BlockerName.PIE_UMBRELLA_1, false },
        { BlockerName.SCOUT_DRAWBRIDGE, false }
    };

    public void FlipBlocker(BlockerName name)
    {
        Blockers[name] = true;
    }
}
