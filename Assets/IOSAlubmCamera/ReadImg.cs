using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class ReadImg : MonoBehaviour {

	public string ImageURL = "";
	//按钮上的文本
	//public Text Btn_ShibieText;
	//显示结果
	//public GameObject ShowResult;
	public Text show;

	public void TestHttpSend()
	{
		//识别文字
		WWWForm form = new WWWForm();
		form.AddField("api_key", "4ve3GloQbtbArqjKacQsIEoqlcVoT2Od");
		form.AddField("api_secret", "Aiwt7vPwoBvyZfcoY3jt6A_LXEGgSRni");
		form.AddField("image_url", ImageURL);
		StartCoroutine(SendPost("https://api-cn.faceplusplus.com/imagepp/v1/recognizetext", form));
	}
	public void TestHttpSend(string path)
	{
		//识别文字
		WWWForm form = new WWWForm();
		form.AddField("api_key", "4ve3GloQbtbArqjKacQsIEoqlcVoT2Od");
		form.AddField("api_secret", "Aiwt7vPwoBvyZfcoY3jt6A_LXEGgSRni");
		//form.AddField("image_url", path);
		form.AddField("image_base64", path);
		StartCoroutine(SendPost("https://api-cn.faceplusplus.com/imagepp/v1/recognizetext", form));
	}

	//提交数据进行识别
	IEnumerator SendPost(string _url, WWWForm _wForm)
	{
		WWW postData = new WWW(_url, _wForm);
		yield return postData;
		if (postData.error != null)
		{
			Debug.Log(postData.error);
			//ShowResult.SetActive(true);
			//Btn_ShibieText.text = "识别";
			//ShowResult.transform.Find("Text").GetComponent<Text>().text = "识别失败！";
			GameObject.Find("DebugText").GetComponent<Text>().text = postData.error;
			//myTimer = 2.0f;
		}
		else
		{
			//Btn_ShibieText.text = "识别";
			Debug.Log(postData.text);
			//GameObject.Find("DebugText").GetComponent<Text>().text = postData.text;
			JsonJieXi(postData.text);
			//show.text = postData.text.ToString ();
		}
	}
	void JsonJieXi(string str)
	{	
		show.text = "";
		JsonData jd = JsonMapper.ToObject(str);
		Debug.Log(jd["result"].Count);
		for (int i = 0; i < jd["result"].Count; i++)
		{
			//for (int j = 0; j < jd["result"]["child-objects"].Count; j++)
			//{
				Debug.Log(jd["result"][i]["type"].ToString());
				Debug.Log(jd["result"][i]["value"].ToString());
			show.text = show.text + jd ["result"] [i] ["value"].ToString ();
			//}
		}
	}
}
