using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(LimitVisibleCharacters))]
public class Typewriter : MonoBehaviour
{
	public float delayBetweenSymbols = 0.1f;
	public AudioClip[] typeSoundEffects;
	public AudioSource audioSourceForTypeEffect;

    public bool ReWrite = false;
    public bool Perehod = false;
    public UnityEvent NextSlide;

    public float DuplicateTime = 10f;


    private float _timer = 0;
	private LimitVisibleCharacters _limitVisibleCharactersComponent = null;
	private Text _textComponent = null;

	void Start ()
	{

	}

	void Update ()
	{

	}

	private void OnEnable ()
	{
		if (_limitVisibleCharactersComponent == null)
		{
			_limitVisibleCharactersComponent = GetComponent<LimitVisibleCharacters>();
		}
		if (_textComponent == null)
		{
			_textComponent = GetComponent<Text>();
		}

		StopCoroutine("PlayTypewriter");
		StartCoroutine("PlayTypewriter");
	}

	private void OnDisable ()
	{
		StopCoroutine("PlayTypewriter");
	}

	private IEnumerator PlayTypewriter()
	{
		_timer = 0;
		_limitVisibleCharactersComponent.visibleCharacterCount = 0;
		yield return null;
		while (_limitVisibleCharactersComponent.visibleCharacterCount <= _textComponent.text.Length)
		{
			_timer += Time.deltaTime;
			if ((typeSoundEffects != null) && (typeSoundEffects.Length > 0) && (audioSourceForTypeEffect != null) && (_limitVisibleCharactersComponent.visibleCharacterCount != (int)(_timer / delayBetweenSymbols)))
			{
				audioSourceForTypeEffect.PlayOneShot(typeSoundEffects[UnityEngine.Random.Range(0, typeSoundEffects.Length)]);
			}
			_limitVisibleCharactersComponent.visibleCharacterCount = (int)(_timer / delayBetweenSymbols);
			yield return null;
		}
		_limitVisibleCharactersComponent.visibleCharacterCount = _textComponent.text.Length;

        if (_limitVisibleCharactersComponent.visibleCharacterCount >= _textComponent.text.Length)
        {
            // Debug.Log("Exit"); Когда текст закончится выполняем действие и возвращаемся.
            if (Perehod == true)
            {
                StartCoroutine("Perehod_Next_Slide");
            }

            if (ReWrite == true)
            {
                StartCoroutine("PlayTypeWretirTwo");
                yield return null;
            }
        }
    }

    private IEnumerator PlayTypeWretirTwo()
    {
        yield return new WaitForSeconds(DuplicateTime);
        _timer = 0;
        _limitVisibleCharactersComponent.visibleCharacterCount = 0;
        StartCoroutine("PlayTypewriter");
        yield return null;
    }

    private IEnumerator Perehod_Next_Slide()
    {
        yield return new WaitForSeconds(DuplicateTime);
        NextSlide.Invoke();
        yield return null;
    }
}
