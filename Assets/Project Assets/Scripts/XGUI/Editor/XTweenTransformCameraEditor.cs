using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(XTweenTransformCamera))]
public class XTweenTransformCameraEditor : Editor 
{
	/// <summary>
	/// Changes the inspector
	/// </summary>
	public override void OnInspectorGUI ()
	{
		XTweenTransformCamera myTarget = (XTweenTransformCamera)target;

		//myTarget.from = EditorGUILayout.ObjectField(myTarget.from, typeof(GameObject), allowSceneObjects : true) as GameObject;
		//myTarget.to = EditorGUILayout.ObjectField(myTarget.to, typeof(GameObject), allowSceneObjects : true) as GameObject;

		myTarget.from = EditorGUILayout.ObjectField(myTarget.from, typeof(GameObject), true) as GameObject;
		myTarget.to = EditorGUILayout.ObjectField(myTarget.to, typeof(GameObject), true) as GameObject;

		DrawTweener(myTarget);
	}

	/// <summary>
	/// Tweener values that belong in the inspector
	/// </summary>
	public void DrawTweener(XTweenTransformCamera myTarget)
	{
		myTarget.playStyle =(XTweener.Style)EditorGUILayout.EnumPopup("Play Style", myTarget.playStyle);
		myTarget.animationCurve = 			EditorGUILayout.CurveField("Animationcurve", myTarget.animationCurve);
		myTarget.duration = 				EditorGUILayout.FloatField("Duration", myTarget.duration);
		myTarget.startDelay = 				EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
		myTarget.ignoreTimescale = 			EditorGUILayout.Toggle("Ignore Timescale", myTarget.ignoreTimescale);
	}
}

