using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FS_Hack
{
    public class Menu : MonoBehaviour
    {
        public static bool menuIsOpen = false;
        public static bool dwellerMenuIsOpen = false;
        public static bool inventoryMenuIsOpen = false;

        public static float windowWidth = 200;
        public static float windowHeight = 340;

        private float PowerValue = 50f;
        private float FoodValue = 50f;
        private float WaterValue = 50f;
        private float NukaValue = 100f;
        private float NukaQValue = 20f;
        private float StimpackValue = 50f;
        private float RadAwayValue = 50f;

        private bool showWeapons = false;
        private bool showPets = false;
        private bool showOutfits = false;
        private bool showJunk = false;
        private Vector2 invScroll;

        public void Start()
        {
        }

        public void OnGUI()
        {
            if (MonoSingleton<Vault>.Instance == null)
                return;
            if (GUI.Button(new Rect(Screen.width - 110, 100, 100, 40), "Main Menu"))
                OpenMenu(1);
            if (GUI.Button(new Rect(Screen.width - 110, 145, 100, 40), "Dweller Menu"))
                OpenMenu(2);
            if (GUI.Button(new Rect(Screen.width - 110, 190, 100, 40), "Inventory Menu"))
                OpenMenu(3);

            if (menuIsOpen)
                DrawMainWindow();
            if (dwellerMenuIsOpen)
                DrawDwellerWindow();
            if (inventoryMenuIsOpen)
                DrawInvWindow();
        }

        private void OpenMenu(int id)
        {
            switch (id)
            {
                case 1:
                    {
                        menuIsOpen = !menuIsOpen;
                        dwellerMenuIsOpen = false;
                        inventoryMenuIsOpen = false;
                        break;
                    }
                case 2:
                    {
                        menuIsOpen = false;
                        dwellerMenuIsOpen = !dwellerMenuIsOpen;
                        inventoryMenuIsOpen = false;
                        break;
                    }
                case 3:
                    {
                        menuIsOpen = false;
                        dwellerMenuIsOpen = false;
                        inventoryMenuIsOpen = !inventoryMenuIsOpen;
                        break;
                    }
            }
        }

        private void DrawMainWindow()
        {
            GUILayout.BeginArea(new Rect(Screen.width - (windowWidth + 10), Screen.height / 2 - (windowHeight / 2) + 85, windowWidth, windowHeight), "Menu", GUI.skin.GetStyle("window"));
            GUILayout.BeginVertical();

            if (GUILayout.Button("+ " + PowerValue + " Energy"))
                MonoSingleton<Vault>.Instance.Storage.Resources.Power += PowerValue;
            PowerValue = GUILayout.HorizontalSlider(Mathf.Round(PowerValue), 10, 200);
            GUILayout.Space(4f);
            if (GUILayout.Button("+ " + FoodValue + " Food"))
                MonoSingleton<Vault>.Instance.Storage.Resources.Food += FoodValue;
            FoodValue = GUILayout.HorizontalSlider(Mathf.Round(FoodValue), 10, 200);
            GUILayout.Space(4f);
            if (GUILayout.Button("+ " + WaterValue + " Water"))
                MonoSingleton<Vault>.Instance.Storage.Resources.Water += WaterValue;
            WaterValue = GUILayout.HorizontalSlider(Mathf.Round(WaterValue), 10, 200);
            GUILayout.Space(4f);
            if (GUILayout.Button("+ " + NukaValue + " Nuka Cola"))
                MonoSingleton<Vault>.Instance.Storage.Resources.Nuka += NukaValue;
            NukaValue = GUILayout.HorizontalSlider(Mathf.Round(NukaValue), 50, 1000);
            GUILayout.Space(4f);
            if (GUILayout.Button("+ " + NukaQValue + " Nuke Quantum"))
                MonoSingleton<Vault>.Instance.Storage.Resources.NukeColaQuantum += NukaQValue;
            NukaQValue = GUILayout.HorizontalSlider(Mathf.Round(NukaQValue), 10, 200);
            GUILayout.Space(4f);
            if (GUILayout.Button("+ " + StimpackValue + " Stimpacks"))
                MonoSingleton<Vault>.Instance.Storage.Resources.StimPack += StimpackValue;
            StimpackValue = GUILayout.HorizontalSlider(Mathf.Round(StimpackValue), 50, 500);
            GUILayout.Space(4f);
            if (GUILayout.Button("+ " + RadAwayValue + " RadAways"))
                MonoSingleton<Vault>.Instance.Storage.Resources.RadAway += RadAwayValue;
            RadAwayValue = GUILayout.HorizontalSlider(Mathf.Round(RadAwayValue), 50, 500);

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void DrawDwellerWindow()
        {
            GUILayout.BeginArea(new Rect(Screen.width - (windowWidth + 10), Screen.height / 2 - (windowHeight / 2) + 85, windowWidth, windowHeight), "Dweller Menu", GUI.skin.GetStyle("window"));
            GUILayout.BeginVertical();

            if (GUILayout.Button("Make Dwellers Happy"))
            {
                List<Dweller> dwellers = MonoSingleton<Vault>.Instance.Dwellers;
                for (int i = 0; i < dwellers.Count; i++)
                    dwellers[i].Happiness.AddHappiness(100);
            }
            if (GUILayout.Button("Spawn Rare Dweller"))
                MonoSingleton<DwellerSpawner>.Instance.CreateWaitingDweller(EGender.Any, false, 0, EDwellerRarity.Rare, false);
            if (GUILayout.Button("Spawn Legendary Dweller"))
                MonoSingleton<DwellerSpawner>.Instance.CreateWaitingDweller(EGender.Any, false, 0, EDwellerRarity.Legendary, false);

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void DrawInvWindow()
        {
            GUILayout.BeginArea(new Rect(Screen.width - (windowHeight + 10), Screen.height / 2 - (windowHeight / 2) + 85, windowWidth + 130, windowHeight), "Inventory Menu", GUI.skin.GetStyle("window"));
            GUILayout.BeginVertical();

            invScroll = GUILayout.BeginScrollView(invScroll);

            if (GUILayout.Button("Increase Inventory Space"))
                MonoSingleton<Vault>.Instance.Inventory.SetMaxItems(MonoSingleton<Vault>.Instance.Inventory.ItemCountMax + 25);
            GUILayout.Space(15f);
            if (GUILayout.Button("Show Weapons"))
                showWeapons = !showWeapons;
            if (showWeapons)
            {
                DwellerWeaponItem[] weapons = MonoSingleton<GameParameters>.Instance.Items.WeaponsList;
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (GUILayout.Button(string.Format("{0} {1}-{2} {3}", weapons[i].GetName(), weapons[i].DamageMin, weapons[i].DamageMax, weapons[i].ItemRarity)))
                        MonoSingleton<Vault>.Instance.Inventory.AddItem(weapons[i].GetAsDwellerItem(false), false);
                }
            }
            GUILayout.Space(12f);
            if (GUILayout.Button("Show Pets"))
                showPets = !showPets;
            if (showPets)
            {
                List<DwellerPetItem> pets = Catalog.Instance.m_petsCustomizationData.PetItems;
                for (int i = 0; i < pets.Count; i++)
                {
                    if (GUILayout.Button(string.Format("{0} {1}", pets[i].GetName(), pets[i].ItemRarity)))
                    {
                        DwellerItem pet = pets[i].GetAsDwellerItem();
                        pet.ExtraData = pets[i].GenerateRandomData();
                        MonoSingleton<Vault>.Instance.Inventory.AddItem(pet, false);
                    }
                }
            }
            GUILayout.Space(12f);
            if (GUILayout.Button("Show Outfits"))
                showOutfits = !showOutfits;
            if (showOutfits)
            {
                DwellerOutfitItem[] outfits = MonoSingleton<GameParameters>.Instance.Items.OutfitList;
                for (int i = 0; i < outfits.Length; i++)
                {
                    if (GUILayout.Button(string.Format("{0} {1}", outfits[i].GetName(), outfits[i].m_category)))
                        MonoSingleton<Vault>.Instance.Inventory.AddItem(outfits[i].GetAsDwellerItem(), false);
                }
            }
            GUILayout.Space(12f);
            if (GUILayout.Button("Show Junk"))
                showJunk = !showJunk;
            if (showJunk)
            {
                DwellerJunkItem[] junk = MonoSingleton<GameParameters>.Instance.Items.JunksList;
                for (int i = 0; i < junk.Length; i++)
                {
                    if (GUILayout.Button(junk[i].GetName()))
                        MonoSingleton<Vault>.Instance.Inventory.AddItem(junk[i].GetAsDwellerItem(), false);
                }
            }

            GUILayout.EndScrollView();

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
