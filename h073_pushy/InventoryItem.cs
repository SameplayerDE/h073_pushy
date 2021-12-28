namespace h073_pushy
{

    public struct ItemAmountChangeResult
    {
        public int Amount;
        public bool Success;
        public bool NegativeError;
        public bool MaxError;
    }
    
    public abstract class InventoryItem
    {
        protected string _displayname;
        protected int _amount;
        protected readonly int _maxAmount = 1;

        public InventoryItem(string displayname, int amount = 1, int maxAmount = 1)
        {
            _displayname = displayname;
            _amount = amount;
            _maxAmount = maxAmount;
        }

        public ItemAmountChangeResult ChangeAmountBy(int amount = 1)
        {
            var result = new ItemAmountChangeResult();
            
            if (_amount + amount > _maxAmount)
            {
                result.MaxError = true;
                result.Success = false;
                result.NegativeError = false;
                result.Amount = _amount;
                return result;
            }

            if (_amount + amount < 0)
            {
                result.MaxError = false;
                result.Success = false;
                result.NegativeError = true;
                result.Amount = _amount;
                return result;
            }

            _amount += amount;
            result.MaxError = false;
            result.Success = true;
            result.NegativeError = false;
            result.Amount = _amount;
            return result;
        }
        
    }
}