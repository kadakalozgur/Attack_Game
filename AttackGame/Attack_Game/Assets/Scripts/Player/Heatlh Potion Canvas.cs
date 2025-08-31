using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeatlhPotionCanvas : MonoBehaviour
{
    public TMP_Text potionNumberText;
 
    public void changeText(int newNumber)
    {

        potionNumberText.text = newNumber.ToString();

    }
}
