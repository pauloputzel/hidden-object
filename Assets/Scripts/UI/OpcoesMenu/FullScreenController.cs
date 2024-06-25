using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenController : MonoBehaviour
{
    public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("change screen mode");
    }
}
