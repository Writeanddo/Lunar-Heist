using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class CharacterSwitcherController : MonoBehaviour
{
    public CharacterSlot Slot1;
    public CharacterSlot Slot2;
    public CharacterSlot Slot3;
    private ScreenBounds bounds = new ScreenBounds();

    public Character SelectedCharacter;
    public List<Character> Characters;

    public float Offset = 1;

    public Color disabledColour;

    private bool switcherOpen;
    private List<TowerSceneWrapper> wrappers = new List<TowerSceneWrapper>();

    void Start()
    {
        SelectedCharacter = (SelectedCharacter == null) ? Characters[0] : SelectedCharacter;
        UpdateSlots();

       LoadScene("MenuOverlay");
       LoadScene(Characters[0].SceneName, true);
       LoadScene(Characters[1].SceneName, true);
       LoadScene(Characters[2].SceneName, true);

    }

    private void LoadScene(string name, bool isTowerScene = false)
    {
        Scene scene = SceneManager.GetSceneByName(name);
        if (!scene.isLoaded)
        {
            StartCoroutine(LoadAndWait(name, isTowerScene));
        }else
        {
            if (isTowerScene){
                initialiseTowerScene(name);
            }
        }
    }

    IEnumerator LoadAndWait(string name, bool isTowerScene)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        Debug.Log("Scene loaded"+ name);
        if (isTowerScene)
        {
            initialiseTowerScene(name);
        }
    }

    private void initialiseTowerScene(String name)
    {
        var w = SceneManager.GetSceneByName(name).GetRootGameObjects()[0].GetComponent<TowerSceneWrapper>();
        wrappers.Add(w);
        w.Activate(w.Character == SelectedCharacter);
    }

    void Update()
    {
        Vector2 v2 = bounds.TopLeftScreen() + new Vector2(Offset, -Offset);
        transform.position = new Vector3(v2.x, v2.y, transform.position.z);
    }

    public void SelectCharacter(Character character)
    {
        if (character == SelectedCharacter)
        {
            if (switcherOpen)
            {
                CloseSwitcher();
            }
            else
            {
                OpenSwitcher();
            }
        }
        else
        {
            Debug.Log("switch to " + character.SceneName);
            wrappers.ForEach(w => w.Activate(character == w.Character));
            SelectedCharacter = character;
            CloseSwitcher();
            UpdateSlots();
        }
    }

    private void CloseSwitcher()
    {
        Slot1.ActivateSelected(disabledColour);
        Slot2.Hide();
        Slot3.Hide();
        switcherOpen = false;
    }

    private void OpenSwitcher()
    {
        Slot1.Preview();
        Slot2.Preview();
        Slot3.Preview();
        switcherOpen = true;
    }

    private void UpdateSlots()
    {
        var OtherCharacters = Characters.FindAll(c => c != SelectedCharacter);
        Slot1.Setup(SelectedCharacter, this, true, disabledColour);
        Slot2.Setup(OtherCharacters[0], this, false, disabledColour);
        Slot3.Setup(OtherCharacters[1], this, false, disabledColour);
    }
    
}
