using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveText : MonoBehaviour {

    public Text textToModify1;
    public Text textToModify2;

    public void OnClick()
    {
            textToModify1.text = textToModify2.text;
    }
}
