using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    // may not be needed
    [SerializeField]
    private int health;
    
    // these are in order from left to right
    public Image[] hearts;

    // first is full heart, then half heart, then no heart.
    public Sprite[] heart_images;

    public Image crosshair;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(20);
    }

    public void ResetHealth() { UpdateHealth(20); }

    public void DisapearHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].enabled = false;

        crosshair.enabled = false;

    }

    public void UpdateHealth(int health) // 0 <= health <=20 <- must be!
    {
        if (health < 0 || health > 20)
            return; //end it lolszdwjnf i think??

        /*
         
         how it will work:

        full heart = 2 hp
        half heart = 1 hp
        no heart = 0 hp
         
         */

        int fullHearts = health / 2;
        int halfHearts = health % 2;
        int emptyHearts = 10 - fullHearts - halfHearts;

        for (int i = 0; i < hearts.Length; i++)
            hearts[i].sprite = heart_images[2];

        for (int i = 0; i < fullHearts; i++)
            hearts[i].sprite = heart_images[0];

        if (halfHearts == 1)
            hearts[fullHearts].sprite = heart_images[1];
    }
}
