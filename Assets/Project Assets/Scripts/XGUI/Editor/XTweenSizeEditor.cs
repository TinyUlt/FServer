using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(XTweenSize))]
public class XTweenSizeEditor : Editor 
{
	/// <summary>
	/// Changes the inspector
	/// </summary>
	public override void OnInspectorGUI ()
	{
		XTweenSize myTarget = (XTweenSize)target;
		
		myTarget.from = EditorGUILayout.Vector2Field("From", myTarget.from);
		myTarget.to = EditorGUILayout.Vector2Field("To", myTarget.to);
		
		DrawTweener(myTarget);
	}

	/// <summary>
	/// Tweener values that belong in the inspector
	/// </summary>
	public void DrawTweener(XTweenSize myTarget)
	{
		myTarget.playStyle =(XTweener.Style)EditorGUILayout.EnumPopup("Play Style", myTarget.playStyle);
		myTarget.animationCurve = 			EditorGUILayout.CurveField("Animationcurve", myTarget.animationCurve);
		myTarget.duration = 				EditorGUILayout.FloatField("Duration", myTarget.duration);
		myTarget.startDelay = 				EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
		myTarget.ignoreTimescale = 			EditorGUILayout.Toggle("Ignore Timescale", myTarget.ignoreTimescale);
	}
}
