﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Laser : MonoBehaviour
{
    public float Interval = 2f;
    public Respawner Respawner;

    private SpriteRenderer sprite;
    public bool startOn;
    private float timer;

    private bool respawning = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = startOn;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Interval)
        {
            timer = 0f;
            sprite.enabled = !sprite.enabled;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        checkIfCollided(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        checkIfCollided(collision);
    }

    private void checkIfCollided(Collider2D collision)
    {
        if (!respawning && sprite.enabled)
        {
            if (collision.tag == "Player")
            {
                Respawner.Respawn("caught", respawningDone);
                respawning = true;
            }
        }
    }

    private void respawningDone()
    {
        respawning = false;
    }
}