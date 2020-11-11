using Cinemachine;
using UnityEngine;

public class TowerSceneWrapper : MonoBehaviour
{
    public Character Character;
    public bool active;
    public GameObject Player;
    void Start()
    {
        gameObject.SetActive(active);
    }

   

    public void Activate(bool activate)
    {
        this.active = activate;
        gameObject.SetActive(active);
        if (activate)
        {
            FindObjectOfType<CinemachineVirtualCamera>().Follow = Player.transform;
            FindObjectOfType<Tower>().Player = Player;
        }
    }

}
