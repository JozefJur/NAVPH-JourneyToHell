using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpNumberController : MonoBehaviour
{

    public TextMeshProUGUI imgText;


    public void SetJumpNumber(int num)
    {
        imgText.SetText(num + "");
    }

}
