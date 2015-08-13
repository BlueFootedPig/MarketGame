Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

<TestClass()> Public Class AssetManagerUnitTests

    <TestMethod()>
    Public Sub AssetManager_AddResource_NoneAlreadyExist()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 10}

        'test
        testManager.AddAsset(resourceToAdd)

        'verify
        Assert.AreEqual(1, testManager.GetAllAssets.Count())
        Dim firstResource As Resource = testManager.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(10, firstResource.Shares)

    End Sub

    <TestMethod()>
    Public Sub AssetManager_AddResource_AlreadyExist()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 10}

        'test
        testManager.AddAsset(resourceToAdd)
        testManager.AddAsset(resourceToAdd)

        'verify
        Assert.AreEqual(1, testManager.GetAllAssets.Count())
        Dim firstResource As Resource = testManager.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(20, firstResource.Shares)

    End Sub

    <TestMethod()>
    Public Sub AssetManager_RemoveResource()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 10}
        testManager.AddAsset(resourceToAdd)

        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 5}
        'test

        testManager.RemoveAsset(resourceToRemove)

        'verify
        Assert.AreEqual(1, testManager.GetAllAssets.Count())
        Dim firstResource As Resource = testManager.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(5, firstResource.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub AssetManager_RemoveResource_NotEnoughResources()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 5}
        testManager.AddAsset(resourceToAdd)

        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 15}
        'test

        testManager.RemoveAsset(resourceToRemove)



    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub AssetManager_RemoveResource_AssetDoesntExist()
        'setup
        Dim testManager As New AssetManager
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 5}

        'test
        testManager.RemoveAsset(resourceToRemove)

    End Sub

End Class