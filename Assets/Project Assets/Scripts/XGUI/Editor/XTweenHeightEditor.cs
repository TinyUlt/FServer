﻿using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(XTweenHeight))]
public class XTweenHeightEditor : Editor 
{
	/// <summary>
	/// Changes the inspector
	/// </summary>
	public override void OnInspectorGUI ()
	{
		XTweenHeight myTarget = (XTweenHeight)target;
		
		myTarget.from = EditorGUILayout.FloatField("From", myTarget.from);
		myTarget.to = EditorGUILayout.FloatField("To", myTarget.to);
		
		DrawTweener(myTarget);
	}

	/// <summary>
	/// Tweener values that belong in the inspector
	/// </summary>
	public void DrawTweener(XTweenHeight myTarget)
	{
		myTarget.playStyle =(XTweener.Style)EditorGUILayout.EnumPopup("Play Style", myTarget.playStyle);
		myTarget.animationCurve = 			EditorGUILayout.CurveField("Animationcurve", myTarget.animationCurve);
		myTarget.duration = 				EditorGUILayout.FloatField("Duration", myTarget.duration);
		myTarget.startDelay = 				EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
		myTarget.ignoreTimescale = 			EditorGUILayout.Toggle("Ignore Timescale", myTarget.ignoreTimescale);
	}
}