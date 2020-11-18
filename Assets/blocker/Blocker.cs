using UnityEngine;

public class Blocker : MonoBehaviour
{
    private BlockerManager BlockerManager;
    public BlockerName Name;
    public GameObject BlockerObject;
    
    void Start()
    {
        BlockerObject.SetActive(true);
    }

    void OnEnable()
    {
        BlockerManager = FindObjectOfType<BlockerManager>();
        Debug.Log("OnEnable" + BlockerManager.Blockers[Name]);

        if (BlockerManager.Blockers[Name])
        {
            BlockerObject.SetActive(false);
        }
    }
}
