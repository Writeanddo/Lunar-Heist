using UnityEngine;

public class TowerSceneWrapper : MonoBehaviour
{
    public Character Character;
    public bool active;
    void Start()
    {
        gameObject.SetActive(active);
    }

    public void Activate(bool activate)
    {
        this.active = activate;
        gameObject.SetActive(active);
    }

}
