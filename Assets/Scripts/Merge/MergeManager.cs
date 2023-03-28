using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Merge
{
    public class MergeManager : MonoBehaviour
    {
        [SerializeField] private TileData _grid;
        public TileData Cells { get => _grid; }

        [SerializeField] private Item _weponPrefab;
        [SerializeField] private Item _ammoPrefab;
        [SerializeField] private Item _explosinablePrefab;

        [SerializeField, Range(0, 100)] private int _ammoRandomPercantage;

        [SerializeField] private BuyButton _weaponBuy;
        [SerializeField] private BuyButton _ammoBuy;


        private bool _isCanBuyWeapon = true;
        private bool _isCanBuyAmmo = true;



        private void OnEnable()
        {
            _weaponBuy.Button.onClick.AddListener(TryBuyWeapon);
            _ammoBuy.Button.onClick.AddListener(TryBuyAmmunation);
            EditorApplication.quitting += Quit;

        }

        private void OnDisable()
        {
            _weaponBuy.Button.onClick.RemoveListener(TryBuyWeapon);
            _ammoBuy.Button.onClick.RemoveListener(TryBuyAmmunation);
            EditorApplication.quitting -= Quit;
        }

        private void Start()
        {
            LoadWeapons();
            UpdateButtonStates();
        }

        public void TryBuyWeapon()
        {
            UpdateButtonStates();
            if (_isCanBuyWeapon == true)
            {
                TutorSystem.GetInstance().InvokeTutor(TutorActivness.WeaponBought);
                IntantiateItem(_weponPrefab);
                MoneyManager.Instance.TakeMoney(_weaponBuy.Price);
                UpdateButtonStates();
            }
        }

        public void TryBuyAmmunation()
        {
            UpdateButtonStates();
            if (_isCanBuyAmmo == true)
            {
                TutorSystem.GetInstance().InvokeTutor(TutorActivness.AmmoBought);

                if (ChooseRandomlyWeaponAmmo() == true)
                {
                    IntantiateItem(_ammoPrefab);
                }
                else
                {
                    IntantiateItem(_explosinablePrefab);
                }
                MoneyManager.Instance.TakeMoney(_ammoBuy.Price);
                UpdateButtonStates();
            }
        }

        private bool ChooseRandomlyWeaponAmmo()
        {
            bool isAmmo = new System.Random().Next(0, 100) < _ammoRandomPercantage;
            if (PlayerPrefs.GetInt(Prefs.TutorPassed, 0) != 1)
            {
                isAmmo = true;
            }
            return isAmmo;
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



        private Item _justInstantiatedItem;
        private void IntantiateItem(Item itemPrefab)
        {
            SoundManager.Instance.ItemBought();
            Item item = Instantiate(itemPrefab);
            _justInstantiatedItem = item;
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

        private void LoadWeapons()
        {
            int index = PlayerPrefs.GetInt(Prefs.CountOfItems);
            print("Index is " + index);

            for (int i = 0; i < index; i++)
            {
                LoadItem(i);
            }
        }
        private void LoadItem(int index)
        {
            int type = PlayerPrefs.GetInt(Prefs.Items + index.ToString() + " type");
            int improveIndex = PlayerPrefs.GetInt(Prefs.Items + index.ToString() + " index");
            print(type);
            switch ((ItemType)type)
            {
                case ItemType.Firearms:
                    IntantiateItem(_weponPrefab);
                    break;
                case ItemType.Explosives:
                    IntantiateItem(_explosinablePrefab);
                    break;
                case ItemType.Ammo:
                    IntantiateItem(_ammoPrefab);
                    break;
                default:
                    break;
            }
            _justInstantiatedItem.ItemMerge.SetLevel(improveIndex);
        }



        private void OnApplicationQuit()
        {
            SaveWeapons();
        }
        private void SaveWeapons()
        {
            int index = 0;

            DeleteSaves();
            for (int i = 0; i < _grid.rows.Length; i++)
            {
                for (int j = 0; j < _grid.rows[i].row.Length; j++)
                {
                    Item item = _grid.rows[i].row[j].CurrentItem;

                    if (item == null)
                    {
                        continue;
                    }

                    PlayerPrefs.SetInt(Prefs.Items + index.ToString() + " type", (int)item.ItemType);
                    PlayerPrefs.SetInt(Prefs.Items + index.ToString() + " index", item.ItemMerge.Index);
                    PlayerPrefs.SetString(Prefs.Items + index.ToString() + " cellName", item.CurrentCell.name);
                    index++;
                }
            }

            PlayerPrefs.SetInt(Prefs.CountOfItems, index);
        }

        private void DeleteSaves()
        {
           
            int index = PlayerPrefs.GetInt(Prefs.CountOfItems);

            for (int i = 0; i < index; i++)
            {
                PlayerPrefs.DeleteKey(Prefs.Items + i.ToString() + " type");
                PlayerPrefs.DeleteKey(Prefs.Items + i.ToString() + " index");
                PlayerPrefs.DeleteKey(Prefs.Items + i.ToString() + " cellName");
            }
        }
        
            public void Quit()
            {
            #if UNITY_STANDALONE
                    Application.Quit();
            #endif
            #if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            }

    }
}

