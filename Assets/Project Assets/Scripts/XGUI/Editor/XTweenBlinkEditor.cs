using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(XTweenBlink))]
public class XTweenBlinkEditor : Editor 
{
	/// <summary>
	/// Changes the inspector
	/// </summary>
	public override void OnInspectorGUI ()
	{
		XTweenBlink myTarget = (XTweenBlink)target;
		
		myTarget.from = EditorGUILayout.FloatField("On Time", myTarget.from);
		myTarget.to = EditorGUILayout.FloatField("Off Time", myTarget.to);
		myTarget.count = EditorGUILayout.IntField("Count", myTarget.count);
		myTarget.deactivateWhenDone = EditorGUILayout.Toggle("Deactivate On Done", myTarget.deactivateWhenDone);
		myTarget.includeChildren = EditorGUILayout.Toggle("Include Children", myTarget.includeChildren);
		
		DrawTweener(myTarget);
	}

	/// <summary>
	/// Tweener values that belong in the inspector
	/// </summary>
	public void DrawTweener(XTweenBlink myTarget)
	{
		myTarget.startDelay = 				EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
		myTarget.ignoreTimescale = 			EditorGUILayout.Toggle("Ignore Timescale", myTarget.ignoreTimescale);
	}
}
