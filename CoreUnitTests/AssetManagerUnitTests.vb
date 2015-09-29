Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

<TestClass()> Public Class AssetManagerUnitTests

    <TestMethod()>
    Public Sub AssetManager_HasEnough_HasEnough()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 10}
        testManager.AddAsset(resourceToAdd)

        'test
        Dim result As Boolean = testManager.HasEnough(New CraftResource() With {.Name = "TestResource", .Shares = 5})

        'verify
        Assert.IsTrue(result)

    End Sub

    <TestMethod()>
    Public Sub AssetManager_HasEnough_NotEnough()
        'setup
        Dim testManager As New AssetManager
        
        'test
        Dim result As Boolean = testManager.HasEnough(New CraftResource() With {.Name = "TestResource", .Shares = 5})

        'verify
        Assert.IsFalse(result)
    End Sub

    <TestMethod()>
    Public Sub AssetManager_HasEnough_TestItemHasNoShares()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 10}
        testManager.AddAsset(resourceToAdd)

        'test
        Dim result As Boolean = testManager.HasEnough(New CraftResource() With {.Name = "Different", .Shares = 0})

        'verify
        Assert.IsTrue(result)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub AssetManager_HasEnough_NullObjectPassed()
        'setup
        Dim testManager As New AssetManager

        'test
        testManager.HasEnough(Nothing)


    End Sub

    <TestMethod()>
    Public Sub AssetManager_AddResource_NoneAlreadyExist()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 10}

        'test
        testManager.AddAsset(resourceToAdd)

        'verify
        Assert.AreEqual(1, testManager.GetAllAssets.Count())
        Dim firstResource As CraftResource = testManager.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(10, firstResource.Shares)

    End Sub

    <TestMethod()>
    Public Sub AssetManager_AddResource_AlreadyExist()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 10}

        'test
        testManager.AddAsset(resourceToAdd)
        testManager.AddAsset(resourceToAdd)

        'verify
        Assert.AreEqual(1, testManager.GetAllAssets.Count())
        Dim firstResource As CraftResource = testManager.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(20, firstResource.Shares)

    End Sub

    <TestMethod()>
    Public Sub AssetManager_RemoveResource()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 10}
        testManager.AddAsset(resourceToAdd)

        Dim resourceToRemove As New CraftResource() With {.Name = "TestResource", .Shares = 5}
        'test

        testManager.RemoveAsset(resourceToRemove)

        'verify
        Assert.AreEqual(1, testManager.GetAllAssets.Count())
        Dim firstResource As CraftResource = testManager.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(5, firstResource.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub AssetManager_RemoveResource_NullResourcePassedIn()
        'setup
        Dim testManager As New AssetManager

        testManager.RemoveAsset(Nothing)


    End Sub

    <TestMethod()>
    Public Sub AssetManager_RemoveResource_RemoveNoShares()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 10}
        testManager.AddAsset(resourceToAdd)

        Dim resourceToRemove As New CraftResource() With {.Name = "Test", .Shares = 0}

        'test
        testManager.RemoveAsset(resourceToRemove)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub AssetManager_RemoveResource_NotEnoughResources()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 5}
        testManager.AddAsset(resourceToAdd)

        Dim resourceToRemove As New CraftResource() With {.Name = "TestResource", .Shares = 15}
        'test

        testManager.RemoveAsset(resourceToRemove)



    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub AssetManager_RemoveResource_AssetDoesntExist()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToRemove As New CraftResource() With {.Name = "TestResource", .Shares = 5}

        'test
        testManager.RemoveAsset(resourceToRemove)

    End Sub

End Class