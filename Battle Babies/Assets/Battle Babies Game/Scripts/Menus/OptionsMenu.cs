using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public AudioMixer mixer;
	public Dropdown resDropdown;
	Resolution[] resolutions;


	private void Start() {
		resolutions = Screen.resolutions;
		resDropdown.ClearOptions();
		List<string> listResolutions = new List<string>();

		int currentResIndex = 0;
		for (int i  = 0; i < resolutions.Length; i++) {

			string res = resolutions[i].width + " x " + resolutions[i].height;
			listResolutions.Add(res);

			if (resolutions[i].width == Screen.currentResolution.width && 
				resolutions[i].height == Screen.currentResolution.height) {
				currentResIndex = i;
			}
		}

		resDropdown.AddOptions(listResolutions);
		resDropdown.value = currentResIndex;
		resDropdown.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex) {
		Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
	}

	public void AdjustVolume(float Volume) {
		mixer.SetFloat("mixerVolume", Volume);
	}

}
