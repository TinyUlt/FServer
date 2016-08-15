using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class mDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public PlayerControl playerControl;

    private Vector3 m_StartPos;

    void Start()
    {
        m_StartPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        var offset = transform.position - m_StartPos;

        transform.position = m_StartPos + Vector3.ClampMagnitude(offset, 100 * transform.lossyScale.y);

        offset = transform.position - m_StartPos;

        offset /= transform.lossyScale.y;

        playerControl.inputMovementH = offset.x / 100;

        playerControl.inputMovementV = offset.y / 100;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.red;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;

        transform.localScale = new Vector3(1f, 1f, 1f);

        transform.position = m_StartPos;

        playerControl.inputMovementH = 0;

        playerControl.inputMovementV = 0;
    }
}
