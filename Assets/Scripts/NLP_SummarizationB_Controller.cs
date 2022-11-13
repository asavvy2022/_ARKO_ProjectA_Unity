using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_SummarizationB_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "summarizationB";
    public InputField inputField;

    public int paddingMaxLength = 200, noRepeatGramSize = 5, numBeams = 10;

    private void Start()
    {
        paddingMaxLength = 200;
        noRepeatGramSize = 5;
        numBeams = 10;
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }
    public void OnClick_SendSummarizationBRequest()
    {
        StartCoroutine(ISendRequest());
    }

    IEnumerator ISendRequest()
    {
        string Text = inputField.text;
        print("Text=" + Text);

        string Request = MakeRequest(Text);
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

            string Summary = www.downloadHandler.text;
            //var jo = JObject.Parse(www.downloadHandler.text);
            //var jArray = JArray.Parse(jsonString);
            print("Summary:" + Summary);
        }
    }

    string MakeRequest(string _Text)
    {
        string ServerIP = GameObject.FindObjectOfType<NLP_Server_Controller>().GetServerIP(ModelName);
        string Request = "";
        Request += ServerIP;
        Request += "&text=" + _Text;
        Request += "&paddingMaxLength=" + paddingMaxLength;
        Request += "&noRepeatGramSize=" + noRepeatGramSize;
        Request += "&numBeams=" + numBeams;

        return Request;
    }
}
