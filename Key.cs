using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private string codeKey;
    public void setCodeKey(string codeKey)
    {
        this.codeKey = codeKey;
    }
    public string getCodeKey() { return codeKey; }
}
