using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour {

    private Text coinText;
    private SatelliteBehavior sb;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        sb = player.GetComponent<SatelliteBehavior>();
        coinText = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        coinText.text = sb.getCoins().ToString();
	}
}
