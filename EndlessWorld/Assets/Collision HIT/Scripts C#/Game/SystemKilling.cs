using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;
using System.Collections.Generic;

public class SystemKilling : MonoBehaviour {

	[HideInInspector]
	public float JustHP;

	private GameObject LineOfHP;
	private int BadlyNaw;
	private GameObject SecLine_zero;

	public AudioClip SoundOfCollision;

	public GameObject GameOverPanel;

	[HideInInspector]
	public bool NoDeleteHP = false;

	private GameObject PointSec_0;
	private GameObject PointSec_1;
	private GameObject PointSec_2;
	private GameObject PointSec_3;

	private GameObject PWC;
	private GameObject FB;
	private GameObject NT;
	private GameObject Mus;
	private GameObject B1;
	private GameObject B2;

    public string clickid;
    private StarkAdManager starkAdManager;

    void Start(){
		PointSec_0 = GameObject.Find("Security 0");
		PointSec_1 = GameObject.Find("Security 1");
		PointSec_2 = GameObject.Find("Security 2");
		PointSec_3 = GameObject.Find("Security 3");
		LineOfHP = GameObject.Find("Sec Line");
		SecLine_zero = GameObject.Find("Sec Line 0");
		PWC = GameObject.Find("prop with Camera");
		FB = GameObject.Find("Fon Badly");
		NT = GameObject.Find("NoTap");
		Mus = GameObject.Find("Music");
		B1 = GameObject.Find("Black Fon 1");
		B2 = GameObject.Find("Black Fon 2");
		JustHP = 1;
		StartCoroutine("RenderUp");
	}

	void OnTriggerEnter(Collider other){ 
		if(gameObject.GetComponent<SystemKilling>().enabled == true){
		if(other.gameObject.tag=="CollisionWith"){
			if(BadlyNaw==0){ BadlyNaw=1; 
				StartCoroutine("CollisionWith");
			}
		}
		}
	}

	IEnumerator CollisionWith(){
		if(JustHP>=1){
			JustHP -= 0.99f; StartCoroutine("CollisionGood");
		}
		else{JustHP = 0; StartCoroutine("CollisionBad");
		}
		yield return new WaitForSeconds(2);
		BadlyNaw=0;
	}

	public void AddHP(){
		SecLine_zero.GetComponent<Image> ().color = new Color (SecLine_zero.GetComponent<Image> ().color.r,SecLine_zero.GetComponent<Image> ().color.g,SecLine_zero.GetComponent<Image> ().color.b,1);
		NoDeleteHP=true;
	}

	public IEnumerator AddHPEnd(){
		int Point =0;
		while(Point == 0){
			if(SecLine_zero.GetComponent<Image>().color.a > 0.3f){
				SecLine_zero.GetComponent<Image> ().color = new Color (SecLine_zero.GetComponent<Image> ().color.r,SecLine_zero.GetComponent<Image> ().color.g,SecLine_zero.GetComponent<Image> ().color.b,SecLine_zero.GetComponent<Image>().color.a - 0.05f);
			}else{Point = 1; NoDeleteHP=false;}
			yield return new WaitForSeconds(0.05f);
		}
	}



	IEnumerator CollisionGood(){
		if(SoundOfCollision){
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundOfCollision);
		}
		PWC.GetComponent<Animation>().Play();
		int Point = 0;
		FB.GetComponent<Image>().enabled=true;
		FB.GetComponent<Image> ().color = new Color (FB.GetComponent<Image>().color.r,FB.GetComponent<Image>().color.g,FB.GetComponent<Image>().color.b,1);
		while(Point == 0){
			if(FB.GetComponent<Image>().color.a>0){
				FB.GetComponent<Image> ().color = new Color (FB.GetComponent<Image>().color.r,FB.GetComponent<Image>().color.g,FB.GetComponent<Image>().color.b,FB.GetComponent<Image>().color.a - 0.04f);
			}else{
				Point = 1;
				FB.GetComponent<Image>().enabled=false;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator CollisionBad(){
        //gameObject.GetComponent<CameraMove>().SaveDistance();
        //if (SoundOfCollision) {
        //	gameObject.GetComponent<AudioSource> ().PlayOneShot (SoundOfCollision);
        //}
        //NT.GetComponent<BoxCollider>().enabled=true;
        //PWC.GetComponent<Animation>().Play();
        //FB.GetComponent<Image>().enabled=true;
        //FB.GetComponent<Image> ().color = new Color (FB.GetComponent<Image>().color.r,FB.GetComponent<Image>().color.g,FB.GetComponent<Image>().color.b,1);

        //gameObject.GetComponent<CameraMove>().StartCoroutine("SpeedOfCameraDown");
        //yield return new WaitForSeconds(2);
        //B1.GetComponent<Image>().enabled=true;
        //B2.GetComponent<Image>().enabled=true;
        //int Point = 0;
        //while(Point == 0){
        //	if (B2.GetComponent<Image>().color.a<1){
        //		B1.GetComponent<Image>().color = new Color(0,0,0,B1.GetComponent<Image>().color.a + 0.05f);
        //		B2.GetComponent<Image>().color = new Color(0,0,0,B2.GetComponent<Image>().color.a + 0.025f);
        //		Mus.GetComponent<AudioSource>().pitch -= 0.025f;
        //	}else{
        //		Mus.GetComponent<AudioSource>().pitch=0;
        //		Point = 1;
        //	}
        //	yield return new WaitForSeconds(0.05f);
        //}
        //yield return new WaitForSeconds(1);
        //Application.LoadLevel(GameObject.Find("Canvas Inteface").GetComponent<PauseGame>().NameOfMenu);
        if (SoundOfCollision)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(SoundOfCollision);
        }
        PWC.GetComponent<Animation>().Play();
        int Point = 0;
        FB.GetComponent<Image>().enabled = true;
        FB.GetComponent<Image>().color = new Color(FB.GetComponent<Image>().color.r, FB.GetComponent<Image>().color.g, FB.GetComponent<Image>().color.b, 1);
        while (Point == 0)
        {
            if (FB.GetComponent<Image>().color.a > 0)
            {
                FB.GetComponent<Image>().color = new Color(FB.GetComponent<Image>().color.r, FB.GetComponent<Image>().color.g, FB.GetComponent<Image>().color.b, FB.GetComponent<Image>().color.a - 0.04f);
            }
            else
            {
                Point = 1;
                FB.GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
        ShowInterstitialAd("81me0b9l59bc36a996",
            () => {

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
	public void ReloadGame()
	{
		Time.timeScale = 1;
        Application.LoadLevel(GameObject.Find("Canvas Inteface").GetComponent<PauseGame>().NameOfMenu);
    }
	public void ContinueGame()
	{
        ShowVideoAd("3a4gg4kf453ehb1a4n",
            (bol) => {
                if (bol)
                {
                    JustHP -= 0.99f;
                    GameOverPanel.SetActive(false);
                    Time.timeScale = 1;

                    clickid = "";
                    getClickid();
                    apiSend("game_addiction", clickid);
                    apiSend("lt_roi", clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
        

    }
	void FixedUpdate(){
		if(NoDeleteHP==false){
			if(JustHP>1.05f && JustHP<2){
				JustHP -= 0.0005f;
			}
			if(JustHP>=2){
				JustHP -= 0.001f;
			}
		}
	}

	IEnumerator RenderUp(){
		int Point  = 0;
		while(Point == 0){
			LineOfHP.GetComponent<Image>().fillAmount = JustHP/3;
			
			if(JustHP<=0){
				PointSec_0.GetComponent<Image>().color = new Color(PointSec_0.GetComponent<Image>().color.r,PointSec_0.GetComponent<Image>().color.g,PointSec_0.GetComponent<Image>().color.b,0.3f);
				PointSec_1.GetComponent<Image>().color = new Color(PointSec_1.GetComponent<Image>().color.r,PointSec_1.GetComponent<Image>().color.g,PointSec_1.GetComponent<Image>().color.b,0.3f);
				PointSec_2.GetComponent<Image>().color = new Color(PointSec_2.GetComponent<Image>().color.r,PointSec_2.GetComponent<Image>().color.g,PointSec_2.GetComponent<Image>().color.b,0.3f);
				PointSec_3.GetComponent<Image>().color = new Color(PointSec_3.GetComponent<Image>().color.r,PointSec_3.GetComponent<Image>().color.g,PointSec_3.GetComponent<Image>().color.b,0.3f);
			}
			if(JustHP>0&&JustHP<1){
				PointSec_0.GetComponent<Image>().color = new Color(PointSec_0.GetComponent<Image>().color.r,PointSec_0.GetComponent<Image>().color.g,PointSec_0.GetComponent<Image>().color.b,1);
				PointSec_1.GetComponent<Image>().color = new Color(PointSec_1.GetComponent<Image>().color.r,PointSec_1.GetComponent<Image>().color.g,PointSec_1.GetComponent<Image>().color.b,0.3f);
				PointSec_2.GetComponent<Image>().color = new Color(PointSec_2.GetComponent<Image>().color.r,PointSec_2.GetComponent<Image>().color.g,PointSec_2.GetComponent<Image>().color.b,0.3f);
				PointSec_3.GetComponent<Image>().color = new Color(PointSec_3.GetComponent<Image>().color.r,PointSec_3.GetComponent<Image>().color.g,PointSec_3.GetComponent<Image>().color.b,0.3f);
			}
			if(JustHP>=1&&JustHP<2){
				PointSec_0.GetComponent<Image>().color = new Color(PointSec_0.GetComponent<Image>().color.r,PointSec_0.GetComponent<Image>().color.g,PointSec_0.GetComponent<Image>().color.b,1);
				PointSec_1.GetComponent<Image>().color = new Color(PointSec_1.GetComponent<Image>().color.r,PointSec_1.GetComponent<Image>().color.g,PointSec_1.GetComponent<Image>().color.b,1);
				PointSec_2.GetComponent<Image>().color = new Color(PointSec_2.GetComponent<Image>().color.r,PointSec_2.GetComponent<Image>().color.g,PointSec_2.GetComponent<Image>().color.b,0.3f);
				PointSec_3.GetComponent<Image>().color = new Color(PointSec_3.GetComponent<Image>().color.r,PointSec_3.GetComponent<Image>().color.g,PointSec_3.GetComponent<Image>().color.b,0.3f);
			}
			if(JustHP>=2&&JustHP<3){
				PointSec_0.GetComponent<Image>().color = new Color(PointSec_0.GetComponent<Image>().color.r,PointSec_0.GetComponent<Image>().color.g,PointSec_0.GetComponent<Image>().color.b,1);
				PointSec_1.GetComponent<Image>().color = new Color(PointSec_1.GetComponent<Image>().color.r,PointSec_1.GetComponent<Image>().color.g,PointSec_1.GetComponent<Image>().color.b,1);
				PointSec_2.GetComponent<Image>().color = new Color(PointSec_2.GetComponent<Image>().color.r,PointSec_2.GetComponent<Image>().color.g,PointSec_2.GetComponent<Image>().color.b,1);
				PointSec_3.GetComponent<Image>().color = new Color(PointSec_3.GetComponent<Image>().color.r,PointSec_3.GetComponent<Image>().color.g,PointSec_3.GetComponent<Image>().color.b,0.3f);
			}
			if(JustHP>=3){
				PointSec_0.GetComponent<Image>().color = new Color(PointSec_0.GetComponent<Image>().color.r,PointSec_0.GetComponent<Image>().color.g,PointSec_0.GetComponent<Image>().color.b,1);
				PointSec_1.GetComponent<Image>().color = new Color(PointSec_1.GetComponent<Image>().color.r,PointSec_1.GetComponent<Image>().color.g,PointSec_1.GetComponent<Image>().color.b,1);
				PointSec_2.GetComponent<Image>().color = new Color(PointSec_2.GetComponent<Image>().color.r,PointSec_2.GetComponent<Image>().color.g,PointSec_2.GetComponent<Image>().color.b,1);
				PointSec_3.GetComponent<Image>().color = new Color(PointSec_3.GetComponent<Image>().color.r,PointSec_3.GetComponent<Image>().color.g,PointSec_3.GetComponent<Image>().color.b,1);
			}
			yield return new  WaitForSeconds(0.1f);
		}
	}




    public void getClickid()
    {
        var launchOpt = StarkSDK.API.GetLaunchOptionsSync();
        if (launchOpt.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOpt.Query)
                if (kv.Value != null)
                {
                    Debug.Log(kv.Key + "<-参数-> " + kv.Value);
                    if (kv.Key.ToString() == "clickid")
                    {
                        clickid = kv.Value.ToString();
                    }
                }
                else
                {
                    Debug.Log(kv.Key + "<-参数-> " + "null ");
                }
        }
    }

    public void apiSend(string eventname, string clickid)
    {
        TTRequest.InnerOptions options = new TTRequest.InnerOptions();
        options.Header["content-type"] = "application/json";
        options.Method = "POST";

        JsonData data1 = new JsonData();

        data1["event_type"] = eventname;
        data1["context"] = new JsonData();
        data1["context"]["ad"] = new JsonData();
        data1["context"]["ad"]["callback"] = clickid;

        Debug.Log("<-data1-> " + data1.ToJson());

        options.Data = data1.ToJson();

        TT.Request("https://analytics.oceanengine.com/api/v2/conversion", options,
           response => { Debug.Log(response); },
           response => { Debug.Log(response); });
    }


    /// <summary>
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="closeCallBack"></param>
    /// <param name="errorCallBack"></param>
    public void ShowVideoAd(string adId, System.Action<bool> closeCallBack, System.Action<int, string> errorCallBack)
    {
        starkAdManager = StarkSDK.API.GetStarkAdManager();
        if (starkAdManager != null)
        {
            starkAdManager.ShowVideoAdWithId(adId, closeCallBack, errorCallBack);
        }
    }

    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="errorCallBack"></param>
    /// <param name="closeCallBack"></param>
    public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
    {
        starkAdManager = StarkSDK.API.GetStarkAdManager();
        if (starkAdManager != null)
        {
            var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
            mInterstitialAd.Load();
            mInterstitialAd.Show();
        }
    }
}
