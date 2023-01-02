using UnityEngine;


namespace Merge
{
    public class MergeManager : MonoBehaviour
    {
        [SerializeField] private TileData _grid;
        public TileData Cells { get => _grid; }

        [SerializeField] private Item _weponPrefab;
        [SerializeField] private Item _ammoPrefab;
        [SerializeField] private Item _explosinablePrefab;

        [SerializeField] private BuyButton _weaponBuy;
        [SerializeField] private BuyButton _ammoBuy;

        private bool _isCanBuyWeapon = true;
        private bool _isCanBuyAmmo = true;



        private void OnEnable()
        {
            _weaponBuy.Button.onClick.AddListener(TryBuyWeapon);
            _ammoBuy.Button.onClick.AddListener(TryBuyAmmunation);
        }
        private void OnDisable()
        {
            _weaponBuy.Button.onClick.RemoveListener(TryBuyWeapon);
            _ammoBuy.Button.onClick.RemoveListener(TryBuyAmmunation);
        }




        private void Start()
        {
            UpdateButtonStates();
        }
        public void TryBuyWeapon()
        {
            if (_isCanBuyWeapon == true)
            {
                IntantiateItem(_weponPrefab);
                MoneyManager.Instance.TakeMoney(_weaponBuy.Price);
                UpdateButtonStates();
            }
        }
        public void TryBuyAmmunation()
        {
            if (_isCanBuyWeapon == true)
            {
                IntantiateItem(_ammoPrefab);
                MoneyManager.Instance.TakeMoney(_ammoBuy.Price);
                UpdateButtonStates();
            }
        }

        private void UpdateButtonStates()
        {
            CheckIsCanBuyWeapon();
            CheckIsCanBuyAmmo();
        }
        private void CheckIsCanBuyWeapon()
        {
            if (CheckIsGridFull() == true)
            {
                _isCanBuyWeapon = false;
                return;
            }
            if (CheckIsMoneyEnought(_weaponBuy) == false)
            {
                _isCanBuyWeapon = false;
                return;
            }
            _isCanBuyWeapon = true;
        }
        private void CheckIsCanBuyAmmo()
        {
            if (CheckIsGridFull() == true)
            {
                _isCanBuyAmmo = false;
                return;
            }
            if (CheckIsMoneyEnought(_ammoBuy) == false)
            {
                _isCanBuyAmmo = false;
                return;
            }
            _isCanBuyAmmo = true;
        }

        private bool CheckIsGridFull()
        {
            for (int i = 0; i < _grid.rows.Length; i++)
            {
                for (int j = 0; j < _grid.rows[i].row.Length; j++)
                {
                    if (_grid.rows[i].row[j].isEmpty() == true)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool CheckIsMoneyEnought(BuyButton buyButton)
        {
            if (buyButton.Price > MoneyManager.Instance.MoneyCount)
            {
                buyButton.SetAsInactive();
                return false;
            }
            buyButton.SetAsActive();
            return true;
        }

        private void IntantiateItem(Item itemPrefab)
        {
            Item item = Instantiate(itemPrefab);

            for (int i = 0; i < _grid.rows.Length; i++)
            {
                for (int j = 0; j < _grid.rows[i].row.Length; j++)
                {
                    if (_grid.rows[i].row[j].isEmpty() == true)
                    {
                        Cell cell = _grid.rows[i].row[j];

                        item.transform.parent = cell.transform.parent.parent;
                        item.transform.localPosition = cell.transform.localPosition;

                        item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y, -2.21f);
                        item.transform.localScale = Vector3.one;
                        item.transform.localEulerAngles = Vector3.zero;

                        item.CurrentCell = cell;
                        cell.SetItem(item);

                        return;
                    }
                }
            }
        }
    }
}

