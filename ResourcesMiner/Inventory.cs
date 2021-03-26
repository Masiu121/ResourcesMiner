namespace ResourcesMiner
{
    public class Inventory
    {
        private int _invLength = 20;
        private OreItem[] _inventory;

        public Inventory()
        {
            _inventory = new OreItem[20];
            SetOres();
        }
        
        public void Add(int type)
        {
            _inventory[type].Count++;
        }

        private void SetOres()
        {
            _inventory[5] = new OreItem();
            _inventory[6] = _inventory[5];
            _inventory[7] = new OreItem();
            _inventory[8] = new OreItem();
            _inventory[9] = new OreItem();
            _inventory[10] = _inventory[9];
            _inventory[11] = new OreItem();
            _inventory[12] = new OreItem();
        }
    }
    
    public class OreItem
    {
        public int Count;
    }
}