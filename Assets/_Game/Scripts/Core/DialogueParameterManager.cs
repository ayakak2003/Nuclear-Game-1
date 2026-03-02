using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParameterManager : MonoBehaviour
{
    public static DialogueParameterManager Instance;
    public string[] paramNames;
    public bool[] parameters; // The permanent memory

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
