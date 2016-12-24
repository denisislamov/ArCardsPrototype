﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using AUP;

public class CustomCameraDemo : MonoBehaviour {
	
	private SharePlugin sharePlugin;
	
	public Text statusText;
	public RawImage rawImage;
	
	private CustomCameraPlugin customCameraPlugin;
	private string folderName="AUP_CCP";
	private string imageFileName="AUP";
	private string imagePath="";
	
	public Button shareButton;
	
	// Use this for initialization
	void Start (){
		sharePlugin = SharePlugin.GetInstance();
		sharePlugin.SetDebug(0);
		
		customCameraPlugin = CustomCameraPlugin.GetInstance();
		customCameraPlugin.SetDebug(0);
		customCameraPlugin.Init(folderName,imageFileName,true);
		
		customCameraPlugin.SetCameraCallbackListener(onCaptureImageComplete,onCaptureImageCancel,onCaptureImageFail);
		
		EnableDisableShareButton(false);
	}
	
	public void OpenCamera(){
		customCameraPlugin.OpenCamera();
		EnableDisableShareButton(false);
		UpdateStatus("Opening Camera");
	}
	
	public void SharePicture(){
		if(!imagePath.Equals("",StringComparison.Ordinal)){
			sharePlugin.ShareImage("MyPictureSubject","MyPictureSubjectContent",imagePath);
			UpdateStatus("Sharing Picture");
		}else{
			Debug.Log("[CustomCameraDemo] imagepath is empty");
			UpdateStatus("can't image path is empty");
		}
	}
	
	private void UpdateStatus(string status){
		if(statusText!=null){
			statusText.text = String.Format("Status: {0}",status);
		}
	}
	
	private void DelayLoadImage(){
		//loads texture
		rawImage.texture = AUP.Utils.LoadTexture(imagePath);
		
		UpdateStatus("load image complete");
		EnableDisableShareButton(true);
	}
	
	private void LoadImageMessage(){
		UpdateStatus("Loading Image...");
	}
	
	private void EnableDisableShareButton(bool val){
		shareButton.interactable = val;
	}
	
	public void onCaptureImageComplete(string imagePaths){
		Dispatcher.GetInstance().InvokeAction(
			()=>{
				Debug.Log("[CustomCameraDemo] onCaptureImageComplete imagePaths " + imagePaths);

				string[] imagePathCollection = imagePaths.Split(',');

				foreach( string path in imagePathCollection){
					Debug.Log("[CustomCameraDemo] onCaptureImageComplete path " + path);
				}

				if(imagePathCollection.Length > 0){
					//get the top most image path
					this.imagePath = imagePathCollection.GetValue(0).ToString();
				}

				UpdateStatus("CaptureImageComplete");
				Invoke("LoadImageMessage",0.3f);
				Invoke("DelayLoadImage",0.5f);
			}
		);
	}
	
	public void onCaptureImageCancel(){
		Dispatcher.GetInstance().InvokeAction(
			()=>{
				UpdateStatus("CaptureImageCancel");
				Debug.Log("[CustomCameraDemo] onCaptureImageCancel");
			}
		);
	}
	
	public void onCaptureImageFail(){
		Dispatcher.GetInstance().InvokeAction(
			()=>{
				UpdateStatus("CaptureImageFail");
				Debug.Log("[CustomCameraDemo] onCaptureImageFail");
			}
		);
	}
	
}
