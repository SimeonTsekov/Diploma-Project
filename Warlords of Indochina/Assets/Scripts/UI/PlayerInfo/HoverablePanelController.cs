using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.PlayerInfo
{
	public class HoverablePanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public GameObject panel;
		private void Awake()
		{
			panel.gameObject.SetActive(false);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			panel.gameObject.SetActive(true);
		}
		
		public void OnPointerExit(PointerEventData eventData)
		{
			panel.gameObject.SetActive(false);
		}
	}
}