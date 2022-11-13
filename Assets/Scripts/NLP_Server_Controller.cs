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
        //Models = new Dictionary<string, string>()
        //{
        //     {"mask", "mask"},
        //     {"nli", "nli"},
        //     {"qa", "qa"},
        //     {"sentiment", "sentiment"},
        //     {"similarity", "similarity"},
        //     {"summarizationA", "summarizationA"},
        //     {"summarizationB", "summarizationB"},
        //     {"textgenA", "textgenA"},
        //     {"textgenB", "textgenB"},
        //     {"textgenC", "textgenC"},
        //     {"keyword", "keyword"},
        //};       
        
        //string Mask = "mask";
        //string NLI = "nli";
        //string QA = "qa";
        //string Sentiment = "sentiment";
        //string Similiarity = "similarity";
        //string SummarizationA = "summarizationA";
        //string SummarizationB = "summarizationB";
        //string TextgenA = "textgenA";
        //string TextgenB = "textgenB";
        //string TextgenC = "textgenC";
        //string Keyword = "keyword";

        Ports = new Dictionary<string, string>()
        {
             {"mask", "23050"},                     //NUC
             {"nli", "23050"},                      //NUC
             {"qa", "23050"},                       //NUC
             {"sentiment", "23050"},                //NUC
             {"similarity", "22950"},               //NUC
             {"summarizationA", "23050"},           //NUC
             {"summarizationB", "23050"},           //NUC (Not Using This)
             {"textgenA", "23050"},                 //NUC - GPT2
             {"textgenB", "23050"},                 //NUC
             {"textgenC", "23050"},                 //NUC
             {"keyword", "23050"},                  //NUC
             {"word2vec", "23050"},                 //NUC
             {"doc2vec", "23050"},                  //NUC
        };

        

        //ServerAddr = new Dictionary<string, string>()
        //{
        //    {Mask, string.Format("{0}:{1}={2}", Server, Ports[Mask])},
        //    {NLI, string.Format("{0}:{1}={2}", Server, Ports[NLI])},
        //    {QA, string.Format("{0}={1}", Server, Ports[QA])},
        //    {Sentiment, string.Format("{0}={1}", Server, Ports[Sentiment])},
        //    {Similiarity, string.Format("{0}={1}", Server, Ports[Similiarity])},
        //    {SummarizationA, string.Format("{0}={1}", Server, Ports[SummarizationA])},
        //    {SummarizationB, string.Format("{0}={1}", Server, Ports[SummarizationB])},
        //    {TextgenA, string.Format("{0}={1}", Server, Ports[TextgenA])},
        //    {TextgenB, string.Format("{0}={1}", Server, Ports[TextgenB])},
        //    {TextgenC, string.Format("{0}={1}", Server, Ports[TextgenC])},
        //    {Keyword, string.Format("{0}={1}", Server, Ports[Keyword])},
        //};

        //print(GetServerIP("nli"));
    }

    public string GetServerIP(string _ModelName)
    {
        string ServerIP = "http://studio522.iptime.org:" + Ports[_ModelName] + "/ai?model=" + _ModelName;
        //string ServerIP = ServerAddr[_ModelName];
        return ServerIP;
    }
}
