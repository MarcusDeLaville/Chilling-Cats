using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdsUpgrade : StoreItem
{
    [Header("Parameters")]
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _requiredTapsCount;
    [SerializeField] private int _advertiseLevel;
    [SerializeField] private List<GameObject> _rewardCustomers;

    [Header("Required Components")]
    [SerializeField] private Advertise _advertise;
    [SerializeField] private Coins _coins;
    [SerializeField] private AdvertiseIcon _advertiseIcon;

    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _buyingObject;
    [SerializeField] private Text _buyStatus;

    private void Start()
    {
        if (Price > 0)
        {
            _priceText.text = Price.ToString();
        }
        else
        {
            _priceText.text = "БЕСПЛАТНО";
        }

        if (PlayerPrefs.GetInt(Name) == 1)
        {
            IsBought = true;
        }

        UpgradeAds();
        SetCheakmark();
    }

    public override void OnTap()
    {
        if (_coins.GetHearts() >= Price)
        {
            PlayerPrefs.SetInt(Name, 1);
            _coins.DepriveHearts(Price);
            IsBought = true;

            UpgradeAds();
            SetCheakmark();
        }
    }

    private void UpgradeAds()
    {
        if (IsBought)
        {
            _advertise.AddCustomers(_rewardCustomers);
            _advertise.SetTapsCount(_requiredTapsCount);
            _advertise.SetSpawnDelay(_spawnDelay);

            _buyingObject.SetActive(false);

            SaveLevel();
            _advertiseIcon.SetIcon();
        }
    }

    private void SetCheakmark()
    {
        if (PlayerPrefs.GetInt(Name) == 1)
        {
            _buyStatus.text = "<color=#228B22>✔</color>";
        }
        else
        {
            _buyStatus.text = "<color=#FF6347>X</color>";
        }
    }

    private void SaveLevel()
    {
        if (PlayerPrefs.HasKey("Ad"))
        {
            if (PlayerPrefs.GetInt("Ad") < _advertiseLevel)
            {
                PlayerPrefs.SetInt("Ad", _advertiseLevel);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Ad", _advertiseLevel);
        }
    }

}
