﻿using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSAlbumCamera : MonoBehaviour
{
	[DllImport ("__Internal")]
	private static extern void _iosOpenPhotoLibrary();
	[DllImport ("__Internal")]
	private static extern void _iosOpenPhotoAlbums();
	[DllImport ("__Internal")]
	private static extern void _iosOpenCamera();
	[DllImport ("__Internal")]
	private static extern void _iosOpenPhotoLibrary_allowsEditing();	
	[DllImport ("__Internal")]
	private static extern void _iosOpenPhotoAlbums_allowsEditing();	
	[DllImport ("__Internal")]
	private static extern void _iosOpenCamera_allowsEditing();
	[DllImport ("__Internal")]
	private static extern void _iosSaveImageToPhotosAlbum(string readAddr);

    /// <summary>
    /// 打开照片
    /// </summary>
    /// <param name="allowsEditing"></param>
	public static void iosOpenPhotoLibrary(bool allowsEditing=false)
	{
		if (allowsEditing)
			_iosOpenPhotoLibrary_allowsEditing ();
		else
			_iosOpenPhotoLibrary ();
	}

    /// <summary>
    /// 打开相册
    /// </summary>
    /// <param name="allowsEditing"></param>
	public static void iosOpenPhotoAlbums(bool allowsEditing=false)
	{
		if (allowsEditing)
			_iosOpenPhotoAlbums_allowsEditing ();
		else
			_iosOpenPhotoAlbums ();
	}

    /// <summary>
    /// 打开相机
    /// </summary>
    /// <param name="allowsEditing"></param>
	public static void iosOpenCamera(bool allowsEditing=false)
	{
		if (allowsEditing)
			_iosOpenCamera_allowsEditing ();
		else
			_iosOpenCamera ();
	}

    /// <summary>
    /// 保存图片到相册
    /// </summary>
    /// <param name="readAddr"></param>
	public static void iosSaveImageToPhotosAlbum(string readAddr)
	{
		_iosSaveImageToPhotosAlbum (readAddr);
	}

    /// <summary>
    /// 将ios传过的string转成u3d中的texture
    /// </summary>
    /// <param name="base64"></param>
    /// <returns></returns>
	public static Texture2D Base64StringToTexture2D(string base64)
	{
		Texture2D tex = new Texture2D (4, 4, TextureFormat.ARGB32, false);
		try
		{
			byte[] bytes = System.Convert.FromBase64String(base64);
			tex.LoadImage(bytes);
		}
		catch(System.Exception ex)
		{
            Debug.LogError(ex.Message);
		}
		return tex;
	}    

	private static IOSAlbumCamera _instance;
	public static IOSAlbumCamera Instance{get{ return _instance; }}

	public System.Action<string> CallBack_PickImage_With_Base64;
	public System.Action<string> CallBack_ImageSavedToAlbum;

	void Awake()
	{
		if (_instance != null)
		{
			DestroyImmediate(this);
			return;
		}
		_instance = this;
//		GameObject go = new GameObject ("IOSAlbumCamera");
//		_instance = go.AddComponent<IOSAlbumCamera> ();
	}

	/// <summary>
	/// 打开相册相机后的从ios回调到unity的方法
	/// </summary>
	/// <param name="base64">Base64.</param>
	void PickImageCallBack_Base64(string base64)
	{
		if(CallBack_PickImage_With_Base64!=null)
		{
			CallBack_PickImage_With_Base64(base64);
		}
	}

    /// <summary>
    /// 保存图片到相册后，从ios回调到unity的方法
    /// </summary>
    /// <param name="msg">Message.</param>
    void SaveImageToPhotosAlbumCallBack(string msg)
	{
		Debug.Log (" -- msg: " + msg);
		if(CallBack_ImageSavedToAlbum!=null)
		{
			CallBack_ImageSavedToAlbum(msg);
		}
	}
}
