using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class MainMenu : MonoBehaviour {

    public GameObject OwnedPrimaryWeaponText;
    public GameObject PurchasePrimaryWeaponButton;
    public Text PurchasePrimaryWeaponButtonText;

    public GameObject FriendPanel;

    public GameObject OwnedSecondaryWeaponText;
    public GameObject PurchaseSecondaryWeaponButton;
    public Text PurchaseSecondaryWeaponText;

    public Text friendrequestText;

    public Text UsernameText;
    public Text GoldAmtText;
    public int curGold;

    public string lastlogin = "Offline";

    List<FriendInfo> _friends = null;

    public GameObject Friend;
    public GameObject[] Friends;

    public bool PlayerOwnsAKM;
    public bool PlayerOwnsM4;
    public bool PlayerOwnsL96;
    public bool PlayerOwnsGlock;
    public bool PlayerOwnsShotgun;

    public GameObject[] PrimaryWeaponImages;
    public Text PrimaryWeaponText;

    //For friendslist
    public Text FriendUser;
    public Text FriendStatus;

    public GameObject[] SecondaryWeaponImages;
    public Text SecondaryWeaponText;

    public void PurchaseWeaponPrimary()
    {
        PurchaseItemRequest request = new PurchaseItemRequest();
        request.CatalogVersion = "Weapons";
        request.VirtualCurrency = "GO";

        if (PrimaryWeaponImages[0].activeSelf)
        {
            request.ItemId = "AKM";
            request.Price = 2000;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert("You have purchased the AKM!"));
                UpdateCurrentGold();
                PlayerOwnsAKM = true;
            }, error =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
        else if (PrimaryWeaponImages[1].activeSelf)
        {
            request.ItemId = "M4";
            request.Price = 2000;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert("You have purchased the M4!"));
                UpdateCurrentGold();
                PlayerOwnsM4 = true;
            }, error =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
        else if (PrimaryWeaponImages[2].activeSelf)
        {
            request.ItemId = "L96";
            request.Price = 4500;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert("You have purchased the L96 Sniper!"));
                UpdateCurrentGold();
                PlayerOwnsL96 = true;
            }, error =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
    }

    public void PurchaseWeaponSecondary()
    {
        PurchaseItemRequest request = new PurchaseItemRequest();
        request.CatalogVersion = "Weapons";
        request.VirtualCurrency = "GO";

        if (SecondaryWeaponImages[0].activeSelf)
        {
            request.ItemId = "Glock";
            request.Price = 500;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert("You have purchased the Glock!"));
                UpdateCurrentGold();
                PlayerOwnsGlock = true;
            }, error =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
        if (SecondaryWeaponImages[1].activeSelf)
        {
            request.ItemId = "Shotgun";
            request.Price = 3000;

            PlayFabClientAPI.PurchaseItem(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert("You have purchased the Shotgun!"));
                UpdateCurrentGold();
                PlayerOwnsShotgun = true;
            }, error =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
    }

    public void NextPrimaryWeapon()
    {
        CheckUserInventory();

        if (PrimaryWeaponImages[0].activeSelf)
        {
            PurchasePrimaryWeaponButtonText.text = "Purchase (2000)";
            PrimaryWeaponText.text = "M4";
            PrimaryWeaponImages[0].SetActive(false);
            PrimaryWeaponImages[1].SetActive(true);
        }
        else if (PrimaryWeaponImages[1].activeSelf)
        {
            PurchasePrimaryWeaponButtonText.text = "Purchase (4500)";
            PrimaryWeaponText.text = "L96 Sniper";
            PrimaryWeaponImages[1].SetActive(false);
            PrimaryWeaponImages[2].SetActive(true);
        }
        else if (PrimaryWeaponImages[2].activeSelf)
        {
            PurchasePrimaryWeaponButtonText.text = "Purchase (2000)";
            PrimaryWeaponText.text = "AKM";
            PrimaryWeaponImages[2].SetActive(false);
            PrimaryWeaponImages[0].SetActive(true);
        }
    }

    public void LastPrimaryWeapon()
    {
        CheckUserInventory();

        if (PrimaryWeaponImages[2].activeSelf)
        {
            PurchasePrimaryWeaponButtonText.text = "Purchase (2000)";
            PrimaryWeaponText.text = "M4";
            PrimaryWeaponImages[2].SetActive(false);
            PrimaryWeaponImages[1].SetActive(true);
        }
        else if (PrimaryWeaponImages[1].activeSelf)
        {
            PurchasePrimaryWeaponButtonText.text = "Purchase (2000)";
            PrimaryWeaponText.text = "AKM";
            PrimaryWeaponImages[1].SetActive(false);
            PrimaryWeaponImages[0].SetActive(true);
        }
        else if (PrimaryWeaponImages[0].activeSelf)
        {
            PurchasePrimaryWeaponButtonText.text = "Purchase (4500)";
            PrimaryWeaponText.text = "L96 Sniper";
            PrimaryWeaponImages[0].SetActive(false);
            PrimaryWeaponImages[2].SetActive(true);
        }
    }

    public void NextSecondaryWeapon()
    {
        CheckUserInventory();

        if (SecondaryWeaponImages[0].activeSelf)
        {
            PurchaseSecondaryWeaponText.text = "Purchase (3000)";
            SecondaryWeaponText.text = "Shotgun";
            SecondaryWeaponImages[0].SetActive(false);
            SecondaryWeaponImages[1].SetActive(true);
        }
        else if (SecondaryWeaponImages[1].activeSelf)
        {
            PurchaseSecondaryWeaponText.text = "Purchase (500)";
            SecondaryWeaponText.text = "Glock";
            SecondaryWeaponImages[1].SetActive(false);
            SecondaryWeaponImages[0].SetActive(true);
        }
    }

    public void LastSecondaryWeapon()
    {
        CheckUserInventory();

        if (SecondaryWeaponImages[1].activeSelf)
        {
            PurchaseSecondaryWeaponText.text = "Purchase (500)";
            SecondaryWeaponText.text = "Glock";
            SecondaryWeaponImages[1].SetActive(false);
            SecondaryWeaponImages[0].SetActive(true);
        }
        else if (SecondaryWeaponImages[0].activeSelf)
        {
            PurchaseSecondaryWeaponText.text = "Purchase (3000)";
            SecondaryWeaponText.text = "Shotgun";
            SecondaryWeaponImages[0].SetActive(false);
            SecondaryWeaponImages[1].SetActive(true);
        }
    }

    public void Start()
    {

        //Gets the player name
        GetPlayerProfileRequest requestProfile = new GetPlayerProfileRequest();

        PlayFabClientAPI.GetPlayerProfile(requestProfile, result =>
        {
            UsernameText.text = result.PlayerProfile.DisplayName;
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });

        UpdateCurrentGold();
        CheckUserInventory();
        GetFriends();
    }


    public void DisplayFriends(List<FriendInfo> friendsCache)
    {
        friendsCache.ForEach(f =>
        {
            GameObject Friend1 = Instantiate(Friend, Vector3.zero, Quaternion.identity);
            Friend1.transform.SetParent(FriendPanel.transform);
            Friend1.transform.localPosition = Vector3.zero;
            Friend1.GetComponentInChildren<Friend>().UsernameText.text = f.Username;
            Friend1.transform.localScale = new Vector3(1, 1, 1);

            Friend1.GetComponentInChildren<Friend>().StatusText.text = lastlogin;
            Debug.Log(f.Username);
        });
    }

    public void GetFriends()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeSteamFriends = false,
            IncludeFacebookFriends = false
        }, result =>
        {
            _friends = result.Friends;
            DisplayFriends(_friends);
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });
    }

    public void AddFriend()
    {
        AddFriendRequest request = new AddFriendRequest();
        request.FriendUsername = friendrequestText.text;

        PlayFabClientAPI.AddFriend(request, result =>
        {
            Alerts a = new Alerts();
            StartCoroutine(a.CreateNewAlert("Added friend: " + friendrequestText.text));

            GameObject[] curFriends = GameObject.FindGameObjectsWithTag("Friend");

            foreach(GameObject friend in curFriends)
            {
                GameObject.Destroy(friend);
            }

            GetFriends();
        }, error =>
        {
            Alerts a = new Alerts();
            StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            Debug.Log(error.ErrorMessage);
        });
    }

    void UpdateCurrentGold()
    {
        GetUserInventoryRequest requestInventory = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(requestInventory, result =>
        {
            result.VirtualCurrency.TryGetValue("GO", out curGold);
            GoldAmtText.text = curGold.ToString();
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });
    }

    void CheckUserInventory()
    {
        GetUserInventoryRequest requestInventory = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(requestInventory, result =>
        {
            foreach (ItemInstance item in result.Inventory)
            {
                if (item.ItemId == "AKM")
                    PlayerOwnsAKM = true;

                if (item.ItemId == "M4")
                    PlayerOwnsM4 = true;

                if (item.ItemId == "L96")
                    PlayerOwnsL96 = true;

                if (item.ItemId == "Glock")
                    PlayerOwnsGlock = true;

                if (item.ItemId == "Shotgun")
                    PlayerOwnsShotgun = true;
            }
        }, error =>
        {
            Alerts a = new Alerts();
            StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
        });
    }

    public void Update()
    {
        if (PlayerOwnsAKM && PrimaryWeaponImages[0].activeSelf)
        {
            OwnedPrimaryWeaponText.SetActive(true);
            PurchasePrimaryWeaponButton.SetActive(false);
        }
        else if (PlayerOwnsM4 && PrimaryWeaponImages[1].activeSelf)
        {
            OwnedPrimaryWeaponText.SetActive(true);
            PurchasePrimaryWeaponButton.SetActive(false);
        }
        else if (PlayerOwnsL96 && PrimaryWeaponImages[2].activeSelf)
        {
            OwnedPrimaryWeaponText.SetActive(true);
            PurchasePrimaryWeaponButton.SetActive(false);
        }
        else
        {
            OwnedPrimaryWeaponText.SetActive(false);
            PurchasePrimaryWeaponButton.SetActive(true);
        }

        //Secondary
        if (PlayerOwnsGlock && SecondaryWeaponImages[0].activeSelf)
        {
            OwnedSecondaryWeaponText.SetActive(true);
            PurchaseSecondaryWeaponButton.SetActive(false);
        }
        else if (PlayerOwnsShotgun && SecondaryWeaponImages[1].activeSelf)
        {
            OwnedSecondaryWeaponText.SetActive(true);
            PurchaseSecondaryWeaponButton.SetActive(false);
        }
        else
        {
            OwnedSecondaryWeaponText.SetActive(false);
            PurchaseSecondaryWeaponButton.SetActive(true);
        }
    }
}
