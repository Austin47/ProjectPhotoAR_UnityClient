﻿using UnityEngine;
using System.Collections.Generic;


namespace Prime31 {
		
public class TouchKitLite : MonoBehaviour
{
	public bool shouldProcessTouches = true;
	const int kTotalTouchesToProcess = 2;


	public List<TKLTouch> liveTouches = new List<TKLTouch>( 2 );
	TKLTouch[] _touchCache;
	const float inchesToCentimeters = 2.54f;

	public float screenPixelsPerCm
	{
		get
		{
			var fallbackDpi = 72f;

			#if UNITY_ANDROID
			// Android MDPI setting fallback
			// http://developer.android.com/guide/practices/screens_support.html
			fallbackDpi = 160f;
			#elif UNITY_WP8 || UNITY_WP8_1 || UNITY_WSA || UNITY_WSA_8_0
			// Windows phone is harder to track down
			// http://www.windowscentral.com/higher-resolution-support-windows-phone-7-dpi-262
			fallbackDpi = 92f;
			#elif UNITY_IOS
			// iPhone 4-6 range
			fallbackDpi = 326f;
			#endif

			return Screen.dpi == 0f ? fallbackDpi / inchesToCentimeters : Screen.dpi / inchesToCentimeters;
		}
	}


	private static TouchKitLite _instance = null;
	public static TouchKitLite instance
	{
		get
		{
			if( System.Object.Equals( _instance, null ) )
				_instance = FindObjectOfType( typeof( TouchKitLite ) ) as TouchKitLite;

			return _instance;
		}
	}


	#region MonoBehaviour

	void Awake()
	{
		// prep our TKLTouch cache so we avoid excessive allocations
		_touchCache = new TKLTouch[kTotalTouchesToProcess];
		for( int i = 0; i < kTotalTouchesToProcess; i++ )
			_touchCache[i] = new TKLTouch( i );
	}


	void Update()
	{
		liveTouches.Clear();

		if( !shouldProcessTouches )
			return;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER || UNITY_WEBGL

		// we only need to process if we have some interesting input this frame
		if( Input.GetMouseButtonUp( 0 ) || Input.GetMouseButton( 0 ) )
		{
#if UNITY_EDITOR
			// in the editor if the remote is not connected we use the mouse to simulate touch
			if( !UnityEditor.EditorApplication.isRemoteConnected )
#endif
				liveTouches.Add( _touchCache[0].populateFromMouse() );
		}

#endif


		if( Input.touchCount > 0 )
		{
			var maxTouchIndexToExamine = Mathf.Min( Input.touches.Length, kTotalTouchesToProcess );
			for( var i = 0; i < maxTouchIndexToExamine; i++ )
			{
				var touch = Input.touches[i];
				if( touch.fingerId < kTotalTouchesToProcess )
					liveTouches.Add( _touchCache[touch.fingerId].populateWithTouch( touch ) );
			}
		}
	}


	void OnApplicationQuit()
	{
		_instance = null;
	}

	#endregion


	public bool hasTouchBeganInRect( Rect rect )
	{
		for( var i = 0; i < liveTouches.Count; i++ )
		{
			var touch = liveTouches[i];
			if( touch.phase == TouchPhase.Began && rect.Contains( touch.position ) )
				return true;
		}

		return false;
	}
}}
