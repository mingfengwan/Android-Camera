using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	private WebCamTexture webcamTexture;
	private Color[] data;
	private RawImage image;
	private string deviceName;
	private Text rgb;

	// Use this for initialization
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		deviceName = devices[1].name;
		webcamTexture = new WebCamTexture(deviceName, Screen.width, Screen.height);
		image = GameObject.Find ("RawImage").GetComponent<RawImage> ();
		rgb = GameObject.Find ("Pixels").GetComponent<Text> ();
		image.rectTransform.sizeDelta = new Vector2 (Screen.height, Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
		image.material.mainTexture = webcamTexture;
		if (webcamTexture.isPlaying) {
			data = webcamTexture.GetPixels ();
			double average_r = 0f;
			double average_g = 0f;
			double average_b = 0f;
			foreach (Color color in data) {
				average_r += color.r;
				average_g += color.g;
				average_b += color.b;
			}
			average_r = average_r / data.Length;
			average_g = average_g / data.Length;
			average_b = average_b / data.Length;
			rgb.text = average_r.ToString ("N1") + " " + average_g.ToString ("N1") + " " + average_b.ToString ("N1");
		}
	}

	public void Play(){
		webcamTexture.Play();
	}

	public void Pause(){
		webcamTexture.Stop ();
	}
}
