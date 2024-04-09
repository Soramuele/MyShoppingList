using UnityEngine;
using TMPro;

public class ShoppingList : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputProducts;
    [SerializeField] private TMP_Text textRecommandation;
    private string[] products;

    [SerializeField] private string[] recipeBook;

    public void AnAbnormalCheckingList()
    {
        string[] check;
        int i;
        products = _inputProducts.text.Split('\n');

        for(i = 0; i < recipeBook.Length; i++)
        {
            check = recipeBook[i].Split(',');

            foreach (var key in products)
                foreach (var prod in check)
                    if (key == prod)
                    {
                        textRecommandation.text = "We recommand you: \n";
                        for (int j = 0; j < check.Length; j++)
                            if (check[j] != key) {
                                Debug.Log(check[j]);
                                textRecommandation.text += check[j] + "\n";
                            }
                    }
        }
    }
}
