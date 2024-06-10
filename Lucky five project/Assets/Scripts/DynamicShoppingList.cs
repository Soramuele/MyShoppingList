using UnityEngine;
using TMPro;

public class DynamicShoppingList : MonoBehaviour
{
    [Header("Change prefab")]
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private GameObject yourProductPrefab;

    [Header("Your product")]
    [SerializeField] private TMP_InputField input;

    [Header("Products data")]
    [SerializeField] private ProductsScriptableObject productsData;

    // Variable responsable for counting
    private int count = 0;

    // Add new product in shopping list
    public void AddProduct()
    {
        // Check if product already in shopping list
        for (count = 1; count < transform.childCount; count++)
            if (transform.GetChild(count).name.ToUpper() == input.text.ToUpper())
            {
                transform.GetChild(count).TryGetComponent<ScrollViewItem>(out ScrollViewItem item);

                // Increase product total price
                productsData.totalPrice[count] += productsData.prices[count];

                // Set new product data
                item.ChangeAmount(productsData.totalPrice[count], ++productsData.amount[count]);
                return;
            }

        // Reset counting variable
        count = 0;

        // Add new product to shopping list
        foreach (Sprite changeSprite in productsData.sprites)
        {
            // Check if product exist in shop
            if (productsData.names[count].ToUpper() == input.text.ToUpper())
            {
                // Instantiate product in shopping list
                GameObject product = Instantiate(yourProductPrefab, scrollViewContent);

                // Set product data
                if (product.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
                {
                    item.ChangeData(changeSprite, productsData.names[count], productsData.prices[count], ++productsData.amount[count]);
                    product.name = productsData.names[count];
                    return;
                }
            }
            count++;
        }

        // Instantiate product that doesn't exist in list
        GameObject newProduct = Instantiate(yourProductPrefab, scrollViewContent);

        // Set product data
        if (newProduct.TryGetComponent<ScrollViewItem>(out ScrollViewItem data))
        {
            data.ChangeData(null, input.text, 0.00f, 1);
            newProduct.name = input.text;
        }
    }
}
