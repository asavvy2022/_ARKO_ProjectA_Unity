using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_Keyword_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "keyword";
    public InputField inputField;
    public Text Result;

    private void Start()
    {
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }

    public void OnClick_SendKeywordRequest()
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
            string Keyword = www.downloadHandler.text;
            Keyword = Keyword.Trim();
            Keyword = Keyword.Substring(1, www.downloadHandler.text.Length - 2);
            //var jo = JObject.Parse(www.downloadHandler.text);
            //var jArray = JArray.Parse(jsonString);
            print("Keyword:" + Keyword);

            string Output = "";
            string[] Keywords = Keyword.Split(',');
            for(int i = 0; i < Keywords.Length; i++)
            {
                Keywords[i] = Keywords[i].Trim();
                Keywords[i] = Keywords[i].Substring(1, Keywords[i].Length - 2);
                print(Keywords[i]);
                Output += Keywords[i];
                Output += "\n";
            }

            Result.text = Output;

        }
    }

    string MakeRequest(string _Text)
    {
        string ServerIP = GameObject.FindObjectOfType<NLP_Server_Controller>().GetServerIP(ModelName);
        string Request = "";
        Request += ServerIP;
        Request += "&text=" + _Text;

        return Request;
    }
}
