using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    [SerializeField]
    private int selected;

    private Image hotbar;

    [SerializeField]
    private Sprite[] hotbar_sprites;

    [SerializeField]
    private Sprite[] wool_sprites;

    [SerializeField]
    private Image wool;

    [SerializeField]
    private TMP_Text iron_text;

    [SerializeField]
    private TMP_Text wool_text;

    // Start is called before the first frame update
    void Start()
    {
        hotbar = GetComponent<Image>();

        selected = 0;
        hotbar.sprite = hotbar_sprites[selected];
    }

    public void UpdateHotBar(int selected)
    {
        if (selected < 0 || selected >= hotbar_sprites.Length)
            return;

        this.selected = selected;
        hotbar.sprite = hotbar_sprites[selected];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWoolColor(string nickname)
    {
        for(int i = 0; i < wool_sprites.Length;  i++)
        {
            if (wool_sprites[i].name.ToLower().Contains(nickname.ToLower()))
            {
                wool.sprite = wool_sprites[i];
            }
        }
    }

    public void UpdateIronAmount(int amount)
    {
        iron_text.text = amount.ToString();
    }
    public void UpdateWoolAmount(int amount)
    {
        wool_text.text = amount.ToString();
    }

    public void UpdateGoldAmount(int amountOfGoldIHave)
    {
        /*throw new NotImplementedException();*/
    }
}
