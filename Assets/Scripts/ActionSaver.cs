using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSaver : MonoBehaviour
{
    public void SavePlayerPrefsKey(string key)
    {
        PlayerPrefs.SetInt(key, 1);
    }
}
