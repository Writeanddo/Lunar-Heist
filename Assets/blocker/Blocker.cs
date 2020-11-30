using UnityEngine;

public class Blocker : MonoBehaviour
{
    private BlockerManager BlockerManager;
    public BlockerName Name;
    public GameObject BlockerObject;
    public bool SetActiveOnUnblocked;
    
    void Start()
    {
        BlockerObject.SetActive(!SetActiveOnUnblocked);
    }

    void OnEnable()
    {
        BlockerManager = FindObjectOfType<BlockerManager>();
        if (BlockerManager.Blockers[Name])
        {
            BlockerObject.SetActive(SetActiveOnUnblocked);
        }
    }
}
