using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamemanager : MonoBehaviour {
    // Prefabs
    public GameObject itemPrefab;
    public GameObject buttonPrefab;
    public GameObject craftButtonPrefab;

    // Holds the inventory
    public GameObject inventory;
    public List<Sprite> itemSprites;

    // Visual for if cursor has item
    GameObject cursorFollower;

    // Items vars
    public GameObject[] items;
    public GameObject selectedItem;
    int numItemsCrafted = 0; // After INV_SIZE items, an error occurs

    // Craft menu boxes
    public GameObject craftItems;
    public GameObject PunnentSquare;
    public GameObject craftedItems;

    // Crafting menu items
    public GameObject craftButton; // Button that crafts
    [SerializeField] GameObject craftItem1;
    [SerializeField] GameObject craftItem2;
    GameObject craftItem3; // The created item box

    // Punnent Square buttons
    public GameObject p0;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    // Var to set # of buttons
    const uint INV_SIZE = 42;

    // Start is called before the first frame update
    void Start()
    {
        // Set up the state dependent variables
        items = new GameObject[INV_SIZE];
        selectedItem = null;
        cursorFollower = null;

        // Set up canvas
        populateInventory(INV_SIZE);
        populateCrafting();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void populateInventory(uint num) {
        for (int i = 0; i < num; i++) {
            GameObject button = Instantiate(buttonPrefab, buttonPrefab.transform.position, buttonPrefab.transform.rotation);
            button.transform.SetParent(inventory.transform);
            button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            button.name = "button"+i;
            button.GetComponent<buttonClick>().gameManager = gameObject;

            // Set item to allow doing things
            if (i < 9) {
                GameObject newItem = Instantiate(itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation);
                newItem.GetComponent<item>().sprite = itemSprites[i % 3];
                newItem.GetComponent<item>().position = i;
                newItem.GetComponent<item>().quantity = Mathf.FloorToInt(Random.Range(1, 10));
                newItem.GetComponent<item>().quantity2 = Mathf.FloorToInt(Random.Range(1, 10));
                button.GetComponent<buttonClick>().setItem(newItem);
                items[i] = newItem;
                numItemsCrafted++;
            }
        }
    }

    void populateCrafting() {
        // Sets up the two crafting inputs
        craftItem1 = Instantiate(buttonPrefab, buttonPrefab.transform.position, buttonPrefab.transform.rotation);
        craftItem2 = Instantiate(buttonPrefab, buttonPrefab.transform.position, buttonPrefab.transform.rotation);
        craftItem1.transform.SetParent(craftItems.transform);
        craftItem2.transform.SetParent(craftItems.transform);
        craftItem1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        craftItem2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        craftItem1.GetComponent<buttonClick>().gameManager = gameObject;
        craftItem2.GetComponent<buttonClick>().gameManager = gameObject;

        // Sets up the crafting results boxes
        craftButton = Instantiate(craftButtonPrefab, craftButtonPrefab.transform.position, craftButtonPrefab.transform.rotation);
        craftItem3 = Instantiate(buttonPrefab, buttonPrefab.transform.position, buttonPrefab.transform.rotation);
        craftButton.transform.SetParent(craftedItems.transform);
        craftItem3.transform.SetParent(craftedItems.transform);
        craftButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        craftItem3.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        craftButton.GetComponent<craftScript>().gameManager = gameObject;
        craftItem3.GetComponent<buttonClick>().gameManager = gameObject;
    }

    // Sets the selected item upon first press. On second press swaps positions
    public void buttonTrigger(GameObject obj, GameObject cursorFollow) {
        if(selectedItem == null) {
            selectedItem = obj;
            selectedItem.GetComponent<buttonClick>().dim();
            cursorFollower = cursorFollow;
        } else {
            // Swap the object locations
            GameObject temp = selectedItem.GetComponent<buttonClick>().heldItem;
//            int tempPos = obj.GetComponent<buttonClick>().heldItem.GetComponent<item>().position;
//            items[temp.GetComponent<item>().position] = obj.GetComponent<buttonClick>().heldItem;
//            items[tempPos] = temp;

            // Swap object visual locations
            selectedItem.GetComponent<buttonClick>().setItem(obj.GetComponent<buttonClick>().heldItem);
            obj.GetComponent<buttonClick>().setItem(temp);

            if (cursorFollower != null) {
                Destroy(cursorFollower);
            }

            // Unselect the orig obj
            selectedItem = null;
            
            checkPopulatePunnentSquare();
        }
    }

    public void checkPopulatePunnentSquare() {
        
        if (craftItem1.GetComponent<buttonClick>().heldItem != null && craftItem2.GetComponent<buttonClick>().heldItem != null) {
            GameObject i1 = craftItem1.GetComponent<buttonClick>().heldItem;
            GameObject i2 = craftItem2.GetComponent<buttonClick>().heldItem;
//            p0.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>(). = "hello";
            p0.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "" + (i1.GetComponent<item>().quantity + i2.GetComponent<item>().quantity) / 2;
            p1.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "" + (i1.GetComponent<item>().quantity + i2.GetComponent<item>().quantity2) / 2;
            p2.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "" + (i1.GetComponent<item>().quantity2 + i2.GetComponent<item>().quantity) / 2;
            p3.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "" + (i1.GetComponent<item>().quantity2 + i2.GetComponent<item>().quantity2) / 2;
        } else {
            GameObject i1 = craftItem1.GetComponent<buttonClick>().heldItem;
            GameObject i2 = craftItem2.GetComponent<buttonClick>().heldItem;
            //            p0.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>(). = "hello";
            p0.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            p1.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            p2.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            p3.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void craft() {
        if(craftItem1.GetComponent<buttonClick>().heldItem == null || craftItem2.GetComponent<buttonClick>().heldItem == null) {
            Debug.Log("Need 2 items to craft.");
            return;
        }
        if(craftItem3.GetComponent<buttonClick>().heldItem != null) {
            Debug.Log("Need crafting space to craft.");
            return;
        }

        int itemType = Mathf.FloorToInt(Random.Range(0, 3));
        if(itemType == 3) {
            itemType = 2;
        }
        
        GameObject newItem = Instantiate(itemPrefab, itemPrefab.transform.position, itemPrefab.transform.rotation);
        newItem.GetComponent<item>().sprite = itemSprites[itemType];
        newItem.GetComponent<item>().position = numItemsCrafted;
        craftItem3.GetComponent<buttonClick>().setItem(newItem);
        items[numItemsCrafted] = newItem;
        numItemsCrafted++;

        // Need to replace with some algorithm that determines stats.
        // This will be based on the heuristic using both plants stats and dominant vs recessive traits.
        // As well as chances to mutate (prolly have it be a high chance so game doesn't stagnate)
        int value = Mathf.FloorToInt(Random.Range(0, 4));

        switch (value) {
            case 0:
                newItem.GetComponent<item>().quantity = craftItem1.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity;
                newItem.GetComponent<item>().quantity2 = craftItem2.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity;
                break;
            case 1:
                newItem.GetComponent<item>().quantity = craftItem1.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity;
                newItem.GetComponent<item>().quantity2 = craftItem2.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity2;
                break;
            case 2:
                newItem.GetComponent<item>().quantity = craftItem1.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity2;
                newItem.GetComponent<item>().quantity2 = craftItem2.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity;
                break;
            default:
                newItem.GetComponent<item>().quantity = craftItem1.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity2;
                newItem.GetComponent<item>().quantity2 = craftItem2.GetComponent<buttonClick>().heldItem.GetComponent<item>().quantity2;
                break;
        }
        Debug.Log($"C1: {newItem.GetComponent<item>().quantity}, C2: {newItem.GetComponent<item>().quantity2}");
    }
}
