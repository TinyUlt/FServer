using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Colors text with a top-bottom gradient
/// NOTE: TEXT ONLY!
/// </summary>
#if UNITY_5_0 || UNITY_5_1
public class XGradient : BaseVertexEffect {
	[SerializeField]
	private Color32 topColor = Color.white;
	[SerializeField]
	private Color32 bottomColor = Color.black;
	
	/// <summary>
	/// Colors the vertices of a ui element. Code snippet by Hampst.
	/// </summary>
	public override void ModifyVertices(List<UIVertex> vertexList) {
		if (this.GetComponent<XTweenColor>() != null){
			topColor = this.GetComponent<XTweenColor>().value;
		}

		if (this.GetComponent<XTweenAlpha>() != null){
			Color tempColor = topColor;
			tempColor.a = this.GetComponent<XTweenAlpha>().value;
			topColor = tempColor;
			tempColor = bottomColor;
			tempColor.a = this.GetComponent<XTweenAlpha>().value;
			bottomColor = tempColor;
		}
		
		if (!IsActive()) {
			return;
		}
		
		int count = vertexList.Count;
		float bottomY = vertexList[0].position.y;
		float topY = vertexList[0].position.y;
		
		for (int i = 1; i < count; i++) {
			float y = vertexList[i].position.y;
			if (y > topY) {
				topY = y;
			} else if (y < bottomY) {
				bottomY = y;
			}
		}
		
		float uiElementHeight = topY - bottomY;
		
		for (int i = 0; i < count; i++) {
			UIVertex uiVertex = vertexList[i];
			uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY) / uiElementHeight);
			vertexList[i] = uiVertex;
		}
	}
	
}
#else 
#if UNITY_5_2_1
public class XGradient : BaseMeshEffect {
	[SerializeField] private Color32 topColor = Color.white;
	[SerializeField] private Color32 bottomColor = Color.black;
	
	/// <summary>
	/// Colors the vertices of a ui element. Code snippet by Hampst.
	/// </summary>
	public override void ModifyMesh(Mesh aMesh) {
		if (this.GetComponent<XTweenColor>() != null){
			topColor = this.GetComponent<XTweenColor>().value;
		}
		
		if (this.GetComponent<XTweenAlpha>() != null){
			Color tempColor = topColor;
			tempColor.a = this.GetComponent<XTweenAlpha>().value;
			topColor = tempColor;
			tempColor = bottomColor;
			tempColor.a = this.GetComponent<XTweenAlpha>().value;
			bottomColor = tempColor;
		}
		
		if (!IsActive()){
			return;
		}
		
		Vector3[] vertexList = aMesh.vertices;
		int count = vertexList.Length;
		float bottomY = vertexList[0].y;
		float topY = vertexList[0].y;
		
		for (int i = 1; i < count; i++) {
			float y = vertexList[i].y;
			if (y > topY) {
				topY = y;
			}else if (y < bottomY) {
				bottomY = y;
			}
		}
		
		float uiElementHeight = topY - bottomY;
		Color[] colors = aMesh.colors;
		for (int i = 0; i < count; i++) {
			colors[i] = Color32.Lerp(bottomColor, topColor, (vertexList[i].y - bottomY) / uiElementHeight);
		}
		aMesh.colors = colors;
	}
}
#else
public class XGradient : BaseMeshEffect {
	[SerializeField] private Color32 topColor = Color.white;
	[SerializeField] private Color32 bottomColor = Color.black;

	public override void ModifyMesh(VertexHelper aHelper){

		if (this.GetComponent<XTweenColor>() != null){
			topColor = this.GetComponent<XTweenColor>().value;
		}
		
		if (this.GetComponent<XTweenAlpha>() != null){
			Color tempColor = topColor;
			tempColor.a = this.GetComponent<XTweenAlpha>().value;
			topColor = tempColor;
			tempColor = bottomColor;
			tempColor.a = this.GetComponent<XTweenAlpha>().value;
			bottomColor = tempColor;
		}
		
		if (!IsActive()){
			return;
		}

		List<UIVertex> vertexList = new List<UIVertex>();
		aHelper.GetUIVertexStream(vertexList);
		int count = vertexList.Count;
		float bottomY = vertexList[0].position.y;
		float topY = vertexList[0].position.y;
		
		for (int i = 1; i < count; i++) {
			float y = vertexList[i].position.y;
			if (y > topY) {
				topY = y;
			}else if (y < bottomY) {
				bottomY = y;
			}
		}
		
		float uiElementHeight = topY - bottomY;
		for (int i = 0; i < count; i++) {
			UIVertex uiVertex = vertexList[i];
			uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY) / uiElementHeight);
			vertexList[i] = uiVertex;
		}

		aHelper.Clear();
		aHelper.AddUIVertexTriangleStream(vertexList);
	}
}
#endif
#endif
