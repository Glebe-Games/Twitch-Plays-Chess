using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	public AudioMixer audioMixer;
	public UnityEngine.UI.Dropdown resDropDown;

	Resolution[] Res;

	// Use this for initialization
	void Start ()
	{
		Res = Screen.resolutions;

		resDropDown.ClearOptions();

		List<string> options = new List<string>();


		 int currentScreenRes = 0;

		for (int x = 0; x < Res.Length; x++)
		{
			string option = Res[x].width + "+" + Res[x].height;
			options.Add(option);

			if (Res[x].width == Screen.currentResolution.width && Res[x].height == Screen.currentResolution.height)
			{
				currentScreenRes = x;
			}
		}
		resDropDown.AddOptions(options);
		resDropDown.value = currentScreenRes;
		resDropDown.RefreshShownValue();
		}

	public void volumeMixer(float volume)
	{
		Debug.Log(volume);
		audioMixer.SetFloat("volume", volume);
	}
	public void FullScreenTogle(bool fullScreenOn)
	{
		Screen.fullScreen = fullScreenOn;
	}
	public void SetRes(int resIndex)
	{
		Resolution res = Res[resIndex];
		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
	}
}
