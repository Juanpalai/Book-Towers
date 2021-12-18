using UnityEngine;
using UnityEngine.UI;

public class SelectedCardUIC : PlayerCardController
{
	//位置修正
	private float _x = 5.0f;

	private Image _selected_card_ui;

	private bool _is_hide;

	private void Awake()
	{
	}

	// Start is called before the first frame update
	private void Start()
	{
		_selected_card_ui = GameObject.Find("SelectedCardUi").GetComponent<Image>();
		hideUi();

	}

	// Update is called once per frame
	private void Update()
	{
		setSelectedCardUi();
	}

	private void setSelectedCardUi()
	{
		if (isPlayerDealCardNull())
		{
			hideUi();
			return;
		}
		setUiPos();
		setUiSize();
		showUi();
	}

	private void setUiPos()
	{
		float x = _my_player.dealCard.rectTransform.position.x + 5.0f;
		float y = _my_player.dealCard.rectTransform.position.y - 3.0f;
		if (isMousePointOnCard())
		{
			x += _x;
		}
		else
		{
			x += (_x / 2);
		}
		_selected_card_ui.rectTransform.position = new Vector3(x, y, 0);
	}

	private void setUiSize()
	{
		Vector2 size = _my_player.dealCard.rectTransform.sizeDelta;
		_selected_card_ui.rectTransform.sizeDelta = size * 0.75f;
	}

	private void showUi()
	{
		_selected_card_ui.gameObject.SetActive(true);
		_selected_card_ui.transform.SetAsFirstSibling( 
			);
		_is_hide = false;
	}

	private void hideUi()
	{
		_selected_card_ui.gameObject.SetActive(false);
		_is_hide = true;
	}

	public bool isHide()
	{
		if (_is_hide)
		{
			return true;
		}

		return false;
	}
}
