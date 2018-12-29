using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGrow : MonoBehaviour {
    public float growth = 1.3f;
    public float timer = 10f;

    public PowerUp powerUp;
    private SatelliteBehavior sb;
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
            powerUp.Activation(false, false, false, 1, growth, timer);
            sb.setMode(1);
            gameObject.SetActive(false);
        }       
    }
}