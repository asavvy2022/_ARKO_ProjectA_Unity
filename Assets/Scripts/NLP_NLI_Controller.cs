using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_NLI_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "nli"; 
    public InputField inputField_Premise, inputField_Hypothesis;

    private void Start()
    {
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }

    public void OnClick_SendNLIRequest()
    {
        StartCoroutine(ISendRequest());
    }

    IEnumerator ISendRequest()
    {
        string Premise = inputField_Premise.text;
        string Hypothesis = inputField_Hypothesis.text;
        print("Premise=" + Premise);
        print("Hypothesis=" + Hypothesis);

        string Request = MakeNLIRequest(Premise, Hypothesis);
        print("Request=" + Request);
        UnityWebRequest www = UnityWebRequest.Get(Request);

        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            print("Error:" + www.error);
        }
        else
        {
            print("Success:" + www.downloadHandler.text);

            string jsonString = www.downloadHandler.text;
            //var jo = JObject.Parse(www.downloadHandler.text);
            var jArray = JArray.Parse(jsonString);
            var jo = jArray[0];
            print("label:" + jo["label"].ToString());
            print("score:" + jo["score"].ToString());
        }
    }

    string MakeNLIRequest(string _Premise, string _Hypothesis)
    {
        string ServerIP = GameObject.FindObjectOfType<NLP_Server_Controller>().GetServerIP(ModelName);
        string Request = "";
        Request += ServerIP;
        Request += "&premise=" + _Premise;
        Request += "&hypothesis=" + _Hypothesis;

        return Request;
    }
}
