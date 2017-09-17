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
		//Opens the front camera in the phone, or back camera in the laptop
		deviceName = devices[1].name;
		webcamTexture = new WebCamTexture(deviceName, Screen.width, Screen.height);

		//Get the two game objects
		image = GameObject.Find ("RawImage").GetComponent<RawImage> ();
		rgb = GameObject.Find ("Pixels").GetComponent<Text> ();

		//Resize the image
		image.rectTransform.sizeDelta = new Vector2 (Screen.height, Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
		//set the image into displaying the captured video
		image.material.mainTexture = webcamTexture;

		if (webcamTexture.isPlaying) {
			//Get the video raw data
			data = webcamTexture.GetPixels ();

			double average_r = 0f;
			double average_g = 0f;
			double average_b = 0f;

			//Add the rgb value in each pixel
			foreach (Color color in data) {
				average_r += color.r;
				average_g += color.g;
				average_b += color.b;
			}

			//Caculate the average
			average_r = average_r / data.Length;
			average_g = average_g / data.Length;
			average_b = average_b / data.Length;

			rgb.text = average_r.ToString ("N1") + " " + average_g.ToString ("N1") + " " + average_b.ToString ("N1");
		}
	}

	//Opens the camera and plays the video captured
	public void Play(){
		webcamTexture.Play();
	}

	//Pause the video
	public void Pause(){
		webcamTexture.Stop ();
		rgb.text = "RGB";
	}
}
