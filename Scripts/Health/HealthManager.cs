using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Options")] 
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public float health = 100f;
    [Tooltip("Max Health is the maximum amount of health the player can have. NOT the starting health.")]
    public float maxHealth = 100f;

    [Header("Player/Enemy Options")] 
    public bool isPlayer;
    public bool isEnemy;
    
    [Header("Death Options")]
    public GameObject deadPanel;
    
    private bool _isDeadPanelNull; 
    private bool _ishealthTextNull; // Check if healthText is null. If so, don't update the text.
    
    void Start()
    {
        _ishealthTextNull = healthText == null;
        healthBar.fillAmount = health / maxHealth;
        
        if (healthText == null)
        {
            _ishealthTextNull = true;
        }
        else
        {
           UpdateText(); 
           _ishealthTextNull = false;
        }
    }
    
    void Update()
    {
        if (health <= 0f)
        {
            if (isPlayer)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                deadPanel.SetActive(true);
            }
            else if (isEnemy)
            {
                var enemyAI = transform.parent.GetComponent<EnemyAI>();
                var objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
                Destroy(enemyAI.gameObject);
                objectiveManager.UpdateTextOnDestroy();
            }
        }
    }
    
    /// <summary>
    /// This method is used to take damage from the player or enemy.
    /// </summary>
    /// <param name="damage">The damage dealt to health.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        UpdateText();
    }
    
    /// <summary>
    /// This method is used to heal the player or enemy.
    /// </summary>
    /// <param name="healAmount">The amount healed.</param>
    public void Heal(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0f, maxHealth);
        healthBar.fillAmount = health / maxHealth;
        UpdateText();
    }
    
    /// <summary>
    /// This method is used to reset the health of the player or enemy.
    /// </summary>
    private void HealthReset()
    {
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
        UpdateText();
    }
    
    /// <summary>
    /// Only used if healthText is not null. This method is used to update the health text.
    /// </summary>
    private void UpdateText()
    {
        if (!_ishealthTextNull)
        {
            healthText.SetText("Health: " + health + " / " + maxHealth);
        }
    }
}