using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterSwitcherController : MonoBehaviour
{
    public AudioSource warpSoundSource;
    public AudioClip[] warpSound;


    public CharacterSlot Slot1;
    public CharacterSlot Slot2;
    public CharacterSlot Slot3;
    private ScreenBounds bounds = new ScreenBounds();

    public Character SelectedCharacter;
    public List<Character> Characters;
    public Dictionary<Character, bool> characterStatus = new Dictionary<Character, bool>();

    public float Offset = 1;

    public Color disabledColour;

    private bool switcherOpen;
    private List<TowerSceneWrapper> wrappers = new List<TowerSceneWrapper>();
    public Light2D SceneLight;
    public Map Map;

    void Awake()
    {

        characterStatus.Add(Characters[0], true);
        characterStatus.Add(Characters[1], true);
        characterStatus.Add(Characters[2], true);
        Debug.Log("awake"+characterStatus.Count);
    }

    void Start()
    {
        SelectedCharacter = (SelectedCharacter == null) ? Characters[0] : SelectedCharacter;
        UpdateSlots();

        LoadScene("MenuOverlay");
        LoadScene(Characters[0].SceneName, true);
        LoadScene(Characters[1].SceneName, true);
        LoadScene(Characters[2].SceneName, true);

        Vector2 v2 = bounds.TopLeftScreen() + new Vector2(Offset, -Offset);
        transform.position = new Vector3(v2.x, v2.y, transform.position.z);
        SceneLight.color = SelectedCharacter.ScreenLightColour;

        Debug.Log(characterStatus.Count);
    }

    private void LoadScene(string name, bool isTowerScene = false)
    {
        Scene scene = SceneManager.GetSceneByName(name);
        if (!scene.isLoaded)
        {
            StartCoroutine(LoadAndWait(name, isTowerScene));
        }
        else
        {
            initialiseScene(name, isTowerScene);
        }
    }

    IEnumerator LoadAndWait(string name, bool isTowerScene)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }

        initialiseScene(name, isTowerScene);

    }

    private void initialiseScene(String name, Boolean isTowerScene)
    {
        if (isTowerScene)
        {
            var wrapper = SceneManager.GetSceneByName(name).GetRootGameObjects()[0].GetComponent<TowerSceneWrapper>();
            wrappers.Add(wrapper);
            wrapper.Activate(wrapper.Character == SelectedCharacter);
        }
        else
        {
            SelectedCharacter.audioSnapshot.TransitionTo(0.1f);
        }
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
            SwitchToCharacter(character);
        }
    }

    private void SwitchToCharacter(Character character)
    {
        wrappers.ForEach(w => w.Activate(character == w.Character));
        SelectedCharacter = character;
        warpSoundSource.PlayOneShot(warpSound[0]);
        CloseSwitcher();
        UpdateSlots();
        character.audioSnapshot.TransitionTo(0.005f);
        SceneLight.color = character.ScreenLightColour;
        Map.RelocateTower();
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
        var count = characterStatus.Values.ToList().FindAll(v => v == true).Count;

        Slot1.Preview();
        if (count >= 2)
        {
            Slot2.Preview();
        }

        if (count == 3)
        {
            Slot3.Preview();
        }
        switcherOpen = true;
    }

    private void UpdateSlots()
    {
        var OtherCharacters = Characters.FindAll(c => c != SelectedCharacter);
        Slot1.Setup(SelectedCharacter, this, true, disabledColour);

        var count = characterStatus.Values.ToList().FindAll(v => v == true).Count;

        if (count == 3)
        {

            Slot2.Setup(OtherCharacters[0], this, false, disabledColour);
            Slot3.Setup(OtherCharacters[1], this, false, disabledColour);
        }


        if (count == 2)
        {
            if (characterStatus[OtherCharacters[0]])
            {
                Slot2.Setup(OtherCharacters[0], this, false, disabledColour);
            }

            if (characterStatus[OtherCharacters[1]])
            {
                Slot2.Setup(OtherCharacters[1], this, false, disabledColour);
            }

            Slot3.gameObject.SetActive(false);
        }

        if (count == 1)
        {
            Slot2.gameObject.SetActive(false);
        }
    }

    public void SetCharacterComplete(Character character)
    {
        characterStatus[character] = false;
       
        // if all characters are done load scene
        if (characterStatus.Values.All(v => !v))
        {
            SceneManager.LoadScene("EndingCutscene");
        }else
        {
            foreach (var key in characterStatus.Keys)
            {
                if (characterStatus[key])
                {
                    SwitchToCharacter(key);
                    return;
                }
            }
        }
    }

}
