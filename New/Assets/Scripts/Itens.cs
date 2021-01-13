using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Itens : ScriptableObject
{
    public string nameItem;
    public Image spriteItem;
    public float value;
    public Player play;
    public List<Itens> Item = new List<Itens>();
    // Start is called before the first frame update
    void Start()
    {

    }
    [System.Serializable]
    public enum SlotType{
            armor,
            helmet,
            espada,
            consumivel

    }
    public SlotType TipoSlot;
    [System.Serializable]
    public enum TypeItem
    {
        equipe,
        potion
    }
    public TypeItem itemtype;
    public void OnDestroy()
    {
        Debug.Log("Ok");
        foreach(Itens  item in Item)
        {
            //Inventory.instance.Remevo(item);
        }
    }
    public void GetAction()
    {
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        switch (itemtype)
        {
            case TypeItem.potion:
                play.AddHP(value);
                break;
            case TypeItem.equipe:
                play.AddAtk(value);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
