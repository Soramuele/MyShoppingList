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

    private bool showing = true;

    void Start()
    {
        // Show all products when starting the app
        ShowAll();
    }

    void Update()
    {
        // Check if user isn't searching and products arent showing
        if (string.IsNullOrWhiteSpace(searchResult.text) && !showing)
        {
            // Show all products
            ShowAll();
            showing = true;
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

        // Instantiate every product in data list

        int i = 0;
        foreach (Sprite sprite in productsData.sprites)
        {
            // Instantiate product
            GameObject product = Instantiate(productPrefab, scrollViewContent);

            // Set product data
            if (product.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
            {
                item.ChangeData(sprite, productsData.names[i], productsData.prices[i], productsData.amount[i]);
                product.name = productsData.names[i];
            }
            i++;
        }
    }

    // Show researched product
    public void ShowResult()
    {
        // Check if user is actually searching for a product's name
        if (!string.IsNullOrWhiteSpace(searchResult.text))
        {
            showing = false;

            // Destroy every object in list
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
            
            // Check for product
            for (int i = 0; i < productsData.sprites.Length; i++)
            {
                // Check if product exist
                if (productsData.names[i].ToUpper().Contains(searchResult.text.ToUpper()))
                {
                    // Instantiate product
                    GameObject product = Instantiate(productPrefab, scrollViewContent);

                    // Set product data
                    if (product.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
                    {
                        item.ChangeData(productsData.sprites[i], productsData.names[i], productsData.prices[i], productsData.amount[i]);
                        product.name = productsData.names[i];
                    }
                }
            }
        }
    }
}
