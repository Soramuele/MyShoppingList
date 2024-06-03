using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollViewItem : MonoBehaviour
{
    [Header("Product data")]
    [SerializeField] private ProductsScriptableObject productsData;

    [Header("Set product data")]
    [SerializeField] private Image childImage;
    [SerializeField] private TMP_Text childName;
    [SerializeField] private TMP_Text childPrice;

    [Header("Set product amount")]
    [SerializeField] private TMP_Text childAmount;

    // Set product data
    public void ChangeData(Sprite image, string name, float price, int amount)
    {
        childImage.sprite = image;
        childName.text = name;
        childPrice.text = "€ " + price.ToString();
        childAmount.text = amount.ToString();
    }

    // Change product amount
    public void ChangeAmount(float price, int amount)
    {
        childPrice.text = "€ " + price.ToString();
        childAmount.text = amount.ToString();
    }

    // Increase amount of product from button
    public void IncreaseAmount()
    {
        // Find product position in names array
        for (int count = 0; count < productsData.amount.Length; count++)
        {
            if (productsData.names[count] == this.name)
            {
                // Increase total price
                productsData.totalPrice[count] += productsData.prices[count];

                // Set new data
                this.ChangeAmount(productsData.totalPrice[count], ++productsData.amount[count]);

                return;
            }
        }

        // If product doesn't exist in list
        int.TryParse(childAmount.text, out int amount);
        this.ChangeAmount(float.NaN , ++amount);
    }

    // Decrease amount of product from button
    public void DecreaseAmount()
    {
        // Find product position in names array
        for (int count = 0; count < productsData.amount.Length; count++)
        {
            if (productsData.names[count] == this.name)
            {
                // Check if total price doesn't go to zero
                if (productsData.totalPrice[count] - productsData.prices[count] != 0)
                {
                    // Decrease total price
                    productsData.totalPrice[count] -= productsData.prices[count];

                    // Set new data
                    this.ChangeAmount(productsData.totalPrice[count], --productsData.amount[count]);

                    return;
                } else // Already only one item left, so destroy
                {
                    DestroyImmediate(this.gameObject);

                    return;
                }
            }
        }

        // If product doesn't exist in list
        int.TryParse(childAmount.text, out int amount);
        if (amount - 1 != 0)
            this.ChangeAmount(float.NaN, --amount);
        else
            DestroyImmediate(this.gameObject);
    }
}
