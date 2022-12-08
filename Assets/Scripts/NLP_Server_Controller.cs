using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NLP_Server_Controller : MonoBehaviour
{
    public string GetServerIP(string _ModelName)
    {
        string ServerIP = "http://127.0.0.1:5000/ai?model=" + _ModelName;
        return ServerIP;
    }
}
