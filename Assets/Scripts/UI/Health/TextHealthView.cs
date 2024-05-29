using TMPro;

public class TextHealthView : HealthView
{
    private TMP_Text _text;

    private void Awake()
    {
        if (TryGetComponent(out TMP_Text text))
            _text = text;
    }

    protected override void Change()
    {
        if (_text != null)
            _text.text = $"{Health.Current} / {Health.MaxHealthPont}";
    }
}
