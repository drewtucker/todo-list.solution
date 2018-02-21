using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ToDoListApp.Models;
using ToDoListApp;

namespace ToDoListApp.Tests
{

  [TestClass]
  public class ItemTest : IDisposable
  {
    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      Item newItem1 = new Item("Walk the dog", 1);
      Item newItem2 = new Item("Do some other crap", 1);
      newItem1.Save();
      newItem2.Save();

      //Act
      List<Item> result = Item.GetAll();
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", 1);

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      Item firstItem = new Item("Mow the lawn", 1);
      Item secondItem = new Item("Mow the lawn", 1);

      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", 1);
      testItem.Save();

      //Act
      Item savedItem = Item.GetAll()[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", 1);
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }

    public void Dispose()
    {
      Item.DeleteAll();
      Category.DeleteAll();
    }

    // [TestMethod]
    // public void Edit_UpdatesItemInDatabase_String()
    // {
    //   string firstDescription = "Walk the Dog";
    //   Item testItem = new Item(firstDescription, 1);
    //   testItem.Save();
    //   string secondDescription = "Mow the lawn";
    //
    //   testItem.Edit(secondDescription);
    //
    //   string result = Item.Find(testItem.GetId()).GetDescription();
    //
    //   Assert.AreEqual(secondDescription , result);
    // }

  }
}
