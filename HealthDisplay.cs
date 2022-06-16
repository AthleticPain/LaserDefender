using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    int currentHealth;
    int maxHealth;
    int healthPercentile;
    Player player;
    TextMeshProUGUI healthScoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        currentHealth = player.GetHealth();
        maxHealth = currentHealth;
        healthScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.GetHealth();
        healthPercentile = (100*currentHealth) / maxHealth;
        if(healthPercentile < 0) { healthPercentile = 0; }
        healthScoreText.text = healthPercentile.ToString() + "%";

    }
}
