using UnityEngine;
using UnityEngine.UI;

public class XTweenPosition : XTweener 
{
	public Vector3 from = new Vector3(0, 0, 0);
	public Vector3 startPosition = new Vector3(0, 0, 0);
	public Vector3 to = new Vector3(0, 0, 0);
	public Vector3 endPosition = new Vector3(0, 0, 0);
	public bool relativeToObject = false;
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public Vector3 value;

	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>
	
	public override void SetValue()
	{
		value = this.GetComponent<RectTransform>().position;

//		Vector3 parentPosition = this.GetComponent<RectTransform>().position - this.GetComponent<RectTransform>().localPosition;
		startPosition = from; // + parentPosition; // (DG) removed this for now since it works better without in anchored cases.
		endPosition = to; // + parentPosition; // (DG) removed this for now.
		if (relativeToObject)
		{
			startPosition = from + value;
			endPosition = to + value;
		}
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	
	public override void ChangeValue(float factor)
	{
		value = Vector3.Lerp(startPosition, endPosition, factor);
		ObjectType();
	}

	/// <summary>
	/// Changes the real value
	/// </summary>
	
	public override void ObjectType()
	{
		Vector3 tempMovement = transform.position; // (DG) why set the variable to this?
		tempMovement = value;
//		transform.Position = tempMovement;
		this.GetComponent<RectTransform>().anchoredPosition = tempMovement;  // (DG) trying anchoredposition of the rect to see if it works better in common cases.
	}

	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	
	public override void SetStartValue()
	{
		startPosition = value;
		endPosition = to;
	}

	/// <summary>
	/// Sets end value to current value
	/// </summary>
	
	public override void SetEndValue()
	{
		startPosition = from;
		endPosition = value;
	}
}
