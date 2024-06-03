using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Products Data", fileName = "Products Data")]
public class ProductsScriptableObject : ScriptableObject
{
    // Product data
    public Sprite[] sprites;
    public string[] names;
    public float[] prices;
    public float[] totalPrice;
    public int[] amount;

    //Shopping list
    public Transform scrollViewItem;

    // Check if in shopping list
    public bool IsInShoppingList(string productName)
    {
        int i;
        for (i = 0; i < scrollViewItem.childCount; i++)
        {
            if (scrollViewItem.GetChild(i).name.ToUpper() == productName.ToUpper())
            {
                return true;
            }
        }
        return false;
    }
}
