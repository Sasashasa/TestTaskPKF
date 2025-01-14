using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSingleUI : MonoBehaviour
{
	[SerializeField] private Image _effectImage;
	[SerializeField] private TextMeshProUGUI _durationText;

	public void SetImage(Effect effect)
	{
		switch (effect)
		{
			case BarrierEffect:
				_effectImage.color = Color.cyan;
				break;
			case BurnEffect:
				_effectImage.color = Color.red;
				break;
			case RegenEffect:
				_effectImage.color = Color.green;
				break;
		}
	}
	
	public void SetDurationText(int duration)
	{
		_durationText.text = duration.ToString();
	}
}