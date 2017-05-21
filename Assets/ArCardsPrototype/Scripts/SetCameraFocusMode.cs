using UnityEngine;
using System.Collections;
using Vuforia;

// TODO - test
public class SetCameraFocusMode : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.5f);

		FocusModeSet();
	}

	private void OnPaused(bool paused)
	{
		if (paused)
		{
			return;
		}

		FocusModeSet();
	}

	private static void FocusModeSet()
	{
		var focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		if (!focusModeSet)
		{
			Debug.Log("Failed to set focus mode (unsupported mode).");
		}
	}
}
