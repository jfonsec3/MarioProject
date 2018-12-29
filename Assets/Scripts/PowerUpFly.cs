using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFly : MonoBehaviour {

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
            powerUp.Activation(true, false, false, 1, 1f, 5f);
            gameObject.SetActive(false);
        }
    }
}
