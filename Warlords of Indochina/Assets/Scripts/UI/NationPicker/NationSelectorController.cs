using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.NationPicker
{
	public class NationSelectorController : MonoBehaviour
	{
		private Text _txt;

		private void Start()
		{
			_txt = GetComponent<Text>();
			_txt.text = "";
		}

		private void Update()
		{
			try
			{
				_txt.text = "Nation: " + NatioIdParser.ParseId(PlayerController.Instance.NationId);
			}
			catch (Exception e)
			{
				Debug.Log(e);
			}
		}
	}
}
