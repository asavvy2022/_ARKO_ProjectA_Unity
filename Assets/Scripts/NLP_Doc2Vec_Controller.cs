using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_Doc2Vec_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "doc2vec";
    public InputField inputField;
    public Text Result;
    private void Start()
    {
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }

    public void OnClick_SendDoc2VecRequest()
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

            string result = www.downloadHandler.text;
            //var jo = JObject.Parse(www.downloadHandler.text);
            //print(jo);
            //var ja = JArray.Parse(result);
            //print(ja);
            result = result.Substring(1, result.Length - 2);
            print(result);    
            Result.text = result;
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
