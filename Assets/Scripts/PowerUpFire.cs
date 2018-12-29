using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFire : MonoBehaviour {
    public float multiplier;
    public float growth = 1f;
    public float timer = 5f;


    public PowerUp powerUp;
    private SatelliteBehavior sb;
    private CircleCollider2D cc;
    private GameObject player;

    void Start()
    {
        powerUp = FindObjectOfType<PowerUp>();
        player = GameObject.FindGameObjectWithTag("Player");
        sb = player.GetComponent<SatelliteBehavior>();
        cc = GetComponent<CircleCollider2D>();

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