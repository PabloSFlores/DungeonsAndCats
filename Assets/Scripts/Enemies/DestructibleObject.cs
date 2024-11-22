using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int maxHealth = 3;
    public SpriteRenderer spriteRenderer;
    public Sprite[] damageSprites;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateSprite();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {
        int spriteIndex = Mathf.Clamp(maxHealth - currentHealth, 0, damageSprites.Length - 1);
        spriteRenderer.sprite = damageSprites[spriteIndex];
    }
}

