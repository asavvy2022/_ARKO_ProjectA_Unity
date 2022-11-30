using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NLP_Server_Controller : MonoBehaviour
{
    Dictionary<string, string> ServerAddr;
    public Dictionary<string, string> Ports;
    string Port = "";

    private void Start()
    {
        Ports = new Dictionary<string, string>()
        {
             {"mask", "22950"},                     //NUC
             {"nli", "22950"},                      //NUC
             {"qa", "22950"},                       //NUC
             {"sentiment", "22950"},                //NUC
             {"similarity", "22950"},               //NUC
             {"summarizationA", "22950"},           //NUC
             {"summarizationB", "22950"},           //NUC (Not Using This)
             {"textgenA", "22950"},                 //NUC - GPT2
             {"textgenB", "22950"},                 //NUC
             {"textgenC", "22950"},                 //NUC
             {"keywords", "22950"},                  //NUC
             {"word2vec", "22950"},                 //NUC
             {"doc2vec", "22950"},                  //NUC
        };
    }

    public string GetServerIP(string _ModelName)
    {
        //string ServerIP = "http://studio522.iptime.org:" + Ports[_ModelName] + "/ai?model=" + _ModelName;
        string ServerIP = "http://127.0.0.1:5000/ai?model=" + _ModelName;
        return ServerIP;
    }
}
