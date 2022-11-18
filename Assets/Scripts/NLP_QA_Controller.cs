using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_QA_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "qa";
    public InputField inputField_Context, inputField_Question;
    public Text Result;

    private void Start()
    {
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }

    public void OnClick_SendQARequest()
    {
        StartCoroutine(ISendRequest());
    }

    IEnumerator ISendRequest()
    {
        string Context = inputField_Context.text;
        string Question = inputField_Question.text;
        print("Context=" + Context);
        print("Question=" + Question);

        string Request = MakeRequest(Context, Question);
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

            string Answer = www.downloadHandler.text;
            print(Answer);
            Result.text = Answer;
        }
    }

    string MakeRequest(string _Context, string _Question)
    {
        string ServerIP = GameObject.FindObjectOfType<NLP_Server_Controller>().GetServerIP(ModelName);
        string Request = "";
        Request += ServerIP;
        Request += "&context=" + _Context;
        Request += "&question=" + _Question;

        return Request;
    }
}
