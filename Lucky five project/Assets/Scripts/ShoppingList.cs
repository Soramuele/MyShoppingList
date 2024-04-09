using UnityEngine;
using TMPro;

public class ShoppingList : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputProducts;
    [SerializeField] private TMP_Text _recommendations;
    private string[] products;
    private string[] newItems;

    [SerializeField] private string[] recipeBook;

    void Awake()
    {
        recipeBook = new string[4] {
                                        "eggs,butter,muffin mix" ,
                                        "pasta,tomato sauce,bacon,spices",
                                        "flour,yeast",
                                        "onions,carrots,ground meat,tomato sauce"
                                    };
    }

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
                        for (int j = 0; j < check.Length; j++) Debug.Log(check[j]);
                            /*if (check[j] != key)
                                newItems[j] = check[j];
            
            if (newItems[0] != null || newItems[1] != null)
                GiveRecommendations();
                */
        }
    }

    private void GiveRecommendations()
    {
        _recommendations.text = "Based on your shopping list we recommand you: \n";

        foreach (var x in newItems)
            _recommendations.text += x.ToString();
    }
}
