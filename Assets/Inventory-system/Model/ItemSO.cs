using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/NewItemSO")]
    public abstract class ItemSO : ScriptableObject
    {
        [Header("Card Type")]
        [Space()]
        public bool Damage;
        public bool Defence;
        public bool Curse;
        public bool Medicated;
        [Header("Card Details")]
        [Space()]
        public string Card_Name;
        public string Card_Type;
        public string disription;
        public string Rarity;

        [Header("Card Powers")]
        [Space()]
        public int MagicPowerRequired;
        public int CurseEffect;
        public float EnemyDamage;
        public float BlockedDamage;


        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }

    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}

