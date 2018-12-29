using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private bool powerUpActive;
    private bool grown;
    private bool playerFlying;
    private bool playerInvincible;
    private bool playerShoot;
    private int pointMultiplier;
    private float playerGrowth;
    private float powerUpTimer;
    private float standardSize;

    private SatelliteBehavior cs;

    // Use this for initialization
    void Start()
    {
        cs = FindObjectOfType<SatelliteBehavior>();
        grown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                cs.size((1 / playerGrowth));
                powerUpActive = false;
                cs.multiplier = 1;
                cs.numberOfJumps = 2;

            }
        }
    }

    public void Activation(bool flying, bool invincible, bool shooting, int multiplier, float growth, float time)
    {
        if (growth > 1)
        {
            grown = true;
            cs.size(growth);
            cs.setMode(1);
        }
        else
        {
            if (!powerUpActive)
            {
                if (flying)
                    cs.numberOfJumps = 5;
                else if (multiplier > 1)
                    cs.multiplier = pointMultiplier;

                powerUpActive = true;
            }
        }
    }

}

