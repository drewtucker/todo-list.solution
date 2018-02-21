using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp;

namespace ToDoListApp.Models
{
  public class Item
  {
    private string _name;
    private int _id;
    private int _categoryId;

    public Item (string name, int categoryId = 0, int Id = 0)
    {
      _name = name;
      _categoryId = categoryId;
      _id = Id;
    }


    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCategoryId()
    {
      return _categoryId;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool nameEquality = (this.GetName() == newItem.GetName());
        bool categoryIdEquality = this.GetCategoryId() == newItem.GetCategoryId();
        return (idEquality && nameEquality && categoryIdEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description, category_id) VALUES (@name, @category_id);";

      MySqlParameter name = new MySqlParameter("@name", _name);
      // name.ParameterName = "@name";
      // name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter catId = new MySqlParameter("@category_id", _categoryId);
      // catId.ParameterName = "@category_id";
      // catId.Value = this._categoryId;
      cmd.Parameters.Add(catId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Item> GetAll()
    {
      List<Item> allCategories = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemName = rdr.GetString(1);
        int itemCategoryId = rdr.GetInt32(3);
        Item newItem = new Item(itemName, itemCategoryId, itemId);
        allCategories.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
    }

    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `items` WHERE id = (@searchId);";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@searchId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemName = "";
      int itemCategoryId = 0;

      while(rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemName = rdr.GetString(1);
        itemCategoryId = rdr.GetInt32(3);
      }
      Item newItem = new Item(itemName, itemCategoryId, itemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return newItem;
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public static void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
