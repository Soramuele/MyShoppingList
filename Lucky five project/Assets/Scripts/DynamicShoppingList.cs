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

    // Variable to check if product exist in list
    private bool doesExist = false;

    // Add new product in shopping list
    public void AddProduct()
    {
        // Check if product already in shopping list
        for (count = 0; count < transform.childCount - 1; count++)
            if (transform.GetChild(count).name.ToUpper() == input.text.ToUpper())
            {
                transform.GetChild(count).TryGetComponent<ScrollViewItem>(out ScrollViewItem data);

                // Increase product total price
                productsData.totalPrice[count] += productsData.prices[count];

                // Set new product data
                data.ChangeAmount(productsData.totalPrice[count], ++productsData.amount[count]);
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
                // Instantiate product in shopping list at last
                GameObject newProduct = Instantiate(yourProductPrefab, scrollViewContent);
                newProduct.transform.SetAsFirstSibling();

                // Set product data
                if (newProduct.TryGetComponent<ScrollViewItem>(out ScrollViewItem data))
                {
                    data.ChangeData(changeSprite, productsData.names[count], productsData.prices[count], ++productsData.amount[count]);
                    newProduct.name = productsData.names[count];
                }
                return;
            }
            count++;
        }

        // Instantiate product that doesn't exist in list
        if (!doesExist)
        {
            // Instantiate product in shopping list at last
            GameObject newProduct = Instantiate(yourProductPrefab, scrollViewContent);
            newProduct.transform.SetAsFirstSibling();

            // Set product data
            if (newProduct.TryGetComponent<ScrollViewItem>(out ScrollViewItem data))
            {
                data.ChangeData(null, input.text, 0.00f, 1);
                newProduct.name = input.text;
            }
        }
    }
}
