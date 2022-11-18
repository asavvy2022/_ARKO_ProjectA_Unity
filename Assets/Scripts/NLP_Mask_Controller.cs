using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP_Mask_Controller : MonoBehaviour
{
    NLP_Server_Controller ServerController;

    /*** mask, nli, qa, sentiment, similarity, summarizationA, summarizationB, textgenB, textgenC ***/
    string ModelName = "mask";
    public InputField inputField;
    public string MaskedWord = "";
    List<string> ReturnedWords;
    public Text Result;

    private void Start()
    {
        ServerController = GameObject.FindObjectOfType<NLP_Server_Controller>();
    }
    public void OnClick_SendRequest()
    {
        StartCoroutine(ISendRequest());
    }

    IEnumerator ISendRequest()
    {
        string Input = inputField.text;
        print("¿ø¹®=" + Input);        
        string MaskedText = MakeMaskedText(inputField);
        print("Masked Text=" + MaskedText);
        print("Masked Word=" + MaskedWord);
        string Request = MakeMaskRequest(MaskedText);
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
            ReturnedWords = new List<string>();
            ReturnedWords.Add(MaskedWord);
            print("masked word:" + MaskedWord);
            print("masked text:" + MaskedText);

            string result = "input text:" + Input + "\n\n";
            result += "masked word:" + MaskedWord + ", masked text:" + MaskedText + "\n\n";
            foreach (JObject jo in jArray)
            {
                var returnedWord = jo["token_str"];
                var returnedSequence = jo["sequence"];
                print("returned word:" + jo["token_str"] + ", sequence:" + returnedSequence);
                ReturnedWords.Add(returnedWord.ToString());
                result += "token:" + returnedWord + ", returned sequence:" + returnedSequence + "\n";
            }
            print(ReturnedWords);
            Result.text = result;
        }
    }


    string MakeMaskedText(string _Text)
    {
        string Text = "";
        if(_Text.Length <= 0)
        {
            Text = "hello hi there!";
        }
        else
        {
            Text = _Text;
        }
        string[] SplitedText = Text.Split(' ');
        int strLength = SplitedText.Length;
        int randWordIndex = Random.Range(0, strLength);
        SplitedText[randWordIndex] = "[MASK]";
        string MaskedText = string.Join(" ", SplitedText);
        return "\'"+MaskedText+ "\'";
    }

    string MakeMaskedText(InputField inputField)
    {
        string Text = "";
        if (inputField.text.Length <= 0)
        {
            Text = "hello hi there!";
        }
        else
        {
            Text = inputField.text;   
        }
        string[] SplitedText = Text.Split(' ');
        int strLength = SplitedText.Length;
        int randWordIndex = Random.Range(0, strLength);
        MaskedWord = SplitedText[randWordIndex];
        SplitedText[randWordIndex] = "[MASK]";
        string MaskedText = string.Join(" ", SplitedText);
        return "\'" + MaskedText + "\'";
    }

    string MakeMaskRequest(string _MaskedText)
    {
        string ServerIP = GameObject.FindObjectOfType<NLP_Server_Controller>().GetServerIP(ModelName);
        string Request = "";
        Request += ServerIP;
        Request += "&text=" + _MaskedText;

        //inputField.text = "";
        //inputField.ActivateInputField();
        //inputField.Select();
        return Request;
    }
}
