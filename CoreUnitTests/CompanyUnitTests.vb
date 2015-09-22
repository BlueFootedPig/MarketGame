Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()> Public Class CompanyUnitTests

    <TestMethod()>
    Public Sub Company_PerformSingleStrategy()

        'setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()
        Dim mockStrategy As IStrategy = Substitute.For(Of IStrategy)()

        Dim testCompany As New Company(New AssetManager())
        testCompany.gamingStrategy.Add(mockStrategy)
        'test
        testCompany.PerformAction(mockMarket)

        'verify
        mockStrategy.Received().Execute(testCompany, mockMarket)

    End Sub

    <TestMethod>
    Public Sub Company_Produce()
        'Setup
        Dim testCompany As New Company(New AssetManager())
        Dim mockProduction As IResourceProduction = Substitute.For(Of IResourceProduction)()


        testCompany.ProducedResource = mockProduction

        'Test
        testCompany.Produce()

        'Verify
        mockProduction.ReceivedWithAnyArgs().Produce(Nothing)


    End Sub

 
    <TestMethod()>
    Public Sub Company_AddResource()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 1337}

        'test
        testCompany.AddResource(newResourceToAdd)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As CraftResource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(1337, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Company_AddResource_NullResource()
        'setup
        Dim testCompany As New Company(New AssetManager())

        'test
        testCompany.AddResource(Nothing)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Company_AddResource_NoShares()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 0}

        'test
        testCompany.AddResource(newResourceToAdd)

    End Sub

    <TestMethod()>
    Public Sub Company_RemoveResource()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 20}
        Dim resourceToRemove As New CraftResource() With {.Name = "TestResource", .Shares = 5}
        testCompany.AddResource(newResourceToAdd)
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As CraftResource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Company_RemoveResource_NoResource()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim resourceToRemove As New CraftResource() With {.Name = "TestResource", .Shares = 5}
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As CraftResource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Company_RemoveResource_NotEnough()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New CraftResource() With {.Name = "TestResource", .Shares = 5}
        Dim resourceToRemove As New CraftResource() With {.Name = "TestResource", .Shares = 20}
        testCompany.AddResource(newResourceToAdd)
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As CraftResource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

End Class