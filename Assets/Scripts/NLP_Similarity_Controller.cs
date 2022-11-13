using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_Similarity_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "similarity";
    public InputField inputField_Text1, inputField_Text2;

    private void Start()
    {
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }

    public void OnClick_SendSimilarityRequest()
    {
        StartCoroutine(ISendRequest());
    }

    IEnumerator ISendRequest()
    {
        string Text1 = inputField_Text1.text;
        string Text2 = inputField_Text2.text;
        print("Context=" + Text1);
        print("Question=" + Text2);

        string Request = MakeRequest(Text1, Text2);
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

            float Similarity = float.Parse(www.downloadHandler.text);
            print(Similarity);
        }
    }

    string MakeRequest(string _Text1, string _Text2)
    {
        string ServerIP = GameObject.FindObjectOfType<NLP_Server_Controller>().GetServerIP(ModelName);
        string Request = "";
        Request += ServerIP;
        Request += "&text1=" + _Text1;
        Request += "&text2=" + _Text2;

        return Request;
    }
}
