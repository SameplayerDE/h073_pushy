using System;
using System.Collections;
using System.Collections.Generic;

namespace h073_pushy
{

    public class Inventory
    {
        private InventoryItem[] _content;
        private IInventoryHolder _inventoryHolder;
        private readonly int _maxSize;

        public int MaxSize => _maxSize;
        public IInventoryHolder InventoryHolder => _inventoryHolder;
        public InventoryItem[] Content => _content;

        public Inventory(IInventoryHolder inventoryHolder = null, int maxSize = 3)
        {
            _inventoryHolder = inventoryHolder;
            _maxSize = maxSize;
            _content = new InventoryItem[_maxSize];
        }

        public void Set(int slot, InventoryItem item) //adds at slot
        {
            if (slot > _maxSize || slot < 0)
            {
                throw new IndexOutOfRangeException();
            }

            _content[slot] = item ?? throw new NullReferenceException();

        }
        
        public bool Add(InventoryItem item) // Adds random
        {
            var index = _maxSize;

            for (var i = _maxSize - 1; i >= 0; i--)
            {
                //TODO empty item check and type check
                if (_content[i] == null || _content[i] == item)
                {
                    index = i;
                }
            }

            if (index == _maxSize)
            {
                return false;
            }

            _content[index] = item;
            return true;
        }

        public bool Contains()
        {
            return false;
        }
        
        public void Clear()
        {
            Array.Clear(_content, 0, _maxSize);
        }
        
    }
}