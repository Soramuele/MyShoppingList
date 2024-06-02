using UnityEngine;
using TMPro;

public class DynamicScrollView : MonoBehaviour
{
    [Header("Change prefab")]
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private GameObject productPrefab;

    [Header("Products data")]
    [SerializeField] private ProductsScriptableObject productsData;

    [Header("Research")]
    [SerializeField] private TMP_InputField searchResult;
    [SerializeField] private GameObject noProductPrefab;

    // Variable responsable for counting
    private int count = 0;
    // Variable to check if products are already showing
    private bool showing = true;

    void Start()
    {
        // Show all products when starting the app
        ShowAll();
        // Reset counter
        count = 0;
    }

    void Update()
    {
        // Check if user isn't searching and products arent showing
        if (!showing && string.IsNullOrWhiteSpace(searchResult.text))
        {
            // Show all products
            ShowAll();

            // Reset variables
            showing = true;
            count = 0;
        }

        // If there are no product in list, instantiate error message
        if (transform.childCount == 0)
            Instantiate(noProductPrefab, scrollViewContent);
    }

    // Show all product
    private void ShowAll()
    {
        // Destroy every object in list
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
        
        // Instantiate every product
        foreach (Sprite changeSprite in productsData.sprites)
        {
            GameObject product = Instantiate(productPrefab, scrollViewContent);

            // Set product data
            if (product.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
            {
                item.ChangeData(changeSprite, productsData.names[count], productsData.prices[count], productsData.amount[count]);
                item.name = productsData.names[count];
            }
            count++;
        }
    }

    // Show researched product
    public void ShowResult()
    {
        // Check if user is actually searching for a product's name
        if (!string.IsNullOrWhiteSpace(searchResult.text))
        {
            // Destroy every object in list
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
            
            // Check for product
            foreach (Sprite changeSprite in productsData.sprites)
            {
                // Check if product exist
                if (productsData.names[count].ToUpper().Contains(searchResult.text.ToUpper()))
                {
                    // Instantiate product
                    GameObject product = Instantiate(productPrefab, scrollViewContent);

                    // Set product data
                    if (product.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
                    {
                        item.ChangeData(productsData.sprites[count], productsData.names[count], productsData.prices[count], productsData.amount[count]);
                        product.name = productsData.names[count];
                    }
                }
                count++;
            }

            // Reset counter and set showing to false
            count = 0;
            showing = false;
        }
    }
}
