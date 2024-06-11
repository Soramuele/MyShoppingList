using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowNextItem : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Shoppping list")]
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private ProductsScriptableObject productsData;
    
    [Header("Show product")]
    [SerializeField] private TMP_Text prodName;
    [SerializeField] private TMP_Text prodAmount;

    [Header("Suggestions")]
    [SerializeField] private TMP_Text suggestName;
    [SerializeField] private Image suggestSprite;
    [SerializeField] private Sprite applepieMixSprite;

    public string[] nextProduct;
    public int[] nextAmount;
    private int totalProduct = 0;
    private bool shownext = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowProductOnScreen());
    }

    // Update is called once every frame
    void Update()
    {
        if (!shownext)
            StartCoroutine(ShowProductOnScreen());

        
    }

    private IEnumerator ShowProductOnScreen()
    {
        shownext = true;
        for (int i = 0; i < 6; i++)
        {
        prodName.text = nextProduct[i];
        prodAmount.text = nextAmount[i].ToString();

        yield return new WaitForSeconds(5);
        }
        shownext = false;
    }

    public void ShowSuggestion()
    {
        /*
        int random = UnityEngine.Random.Range(0, 45);
        
        suggestName.text = productsData.names[random];
        suggestSprite.sprite = productsData.sprites[random];
        */
        suggestName.text = "ApplePie Mix";
        suggestSprite.sprite = applepieMixSprite;
    }
}
