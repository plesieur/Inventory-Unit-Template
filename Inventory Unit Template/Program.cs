﻿namespace Inventory_Unit_Template
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;

    // Item Module
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string GetItemDetails()
        {
            return $"ID: {ItemId}, Name: {Name}, Quantity: {Quantity}, Price: {Price:C}";
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity >= 0)
            {
                Quantity = newQuantity;
            }
            else
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }
        }
    }

    // Inventory Module
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        private string mainFilePath;
        private string tempFilePath;

        public Inventory(string mainFilePath, string tempFilePath)
        {
            this.mainFilePath = mainFilePath;
            this.tempFilePath = tempFilePath;
            LoadTempInventory(); // Load from temp on start.
        }

        private void SaveTempInventory()
        {
            string jsonString = JsonSerializer.Serialize(items);
            File.WriteAllText(tempFilePath, jsonString);
        }

        private void LoadTempInventory()
        {
            if (File.Exists(tempFilePath))
            {
                string jsonString = File.ReadAllText(tempFilePath);
                items = JsonSerializer.Deserialize<List<Item>>(jsonString) ?? new List<Item>();
            }
            else
            {
                items = new List<Item>();
            }
        }
    }

    // UI Module (Command-Line)
    public class UI
    {
        private Inventory inventory;

        public UI(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("1. Test Case");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TestCase();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }


        private void TestCase()
        {
            Console.WriteLine("Test Failed");
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            string mainFilePath = "../../../inventory.json";
            string tempFilePath = "../../../temp_inventory.json";
            Inventory inventory = new Inventory(mainFilePath, tempFilePath);
            UI ui = new UI(inventory);
            ui.Run();
        }
    }
}
