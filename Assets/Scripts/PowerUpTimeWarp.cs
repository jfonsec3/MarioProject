﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimeWarp : MonoBehaviour
{
    public int multiplier = 5;
    public float timer = 10f;

    public PowerUp powerUp;
    private SatelliteBehavior sb;
    private CircleCollider2D cc;
    private GameObject player;

    void Start()
    {
        powerUp = FindObjectOfType<PowerUp>();
        player = GameObject.FindGameObjectWithTag("Player");
        sb = player.GetComponent<SatelliteBehavior>();

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            powerUp.Activation(false, false, false, multiplier, 1f, timer);
            sb.setMode(1);
            gameObject.SetActive(false);
        }
    }
}
