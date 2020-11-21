﻿using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterSwitcherController : MonoBehaviour
{
    public AudioSource warpSound;

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
    public Light2D SceneLight;

    void Start()
    {
        Application.targetFrameRate = 30;
        SelectedCharacter = (SelectedCharacter == null) ? Characters[0] : SelectedCharacter;
        UpdateSlots();

        LoadScene("MenuOverlay");
        LoadScene(Characters[0].SceneName, true);
        LoadScene(Characters[1].SceneName, true);
        LoadScene(Characters[2].SceneName, true);

        Vector2 v2 = bounds.TopLeftScreen() + new Vector2(Offset, -Offset);
        transform.position = new Vector3(v2.x, v2.y, transform.position.z);
        SceneLight.color = SelectedCharacter.ScreenLightColour;

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
        warpSound.Play();
        CloseSwitcher();
        UpdateSlots();
        character.audioSnapshot.TransitionTo(0.005f);
        SceneLight.color = character.ScreenLightColour;
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
