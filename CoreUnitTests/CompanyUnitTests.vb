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
    Public Sub Company_Produce_SingleRequiredResource()
        'Setup
        Dim testCompany As New Company(New AssetManager())
        Dim requiredResource As New Resource() With {.Name = "TestResource", .Shares = 2}
        testCompany.RequiredResources.Add(requiredResource)
        testCompany.ProducedResource = New Resource() With {.Name = "ProducedResource", .Shares = 1}

        testCompany.AddResource(New Resource() With {.Name = "TestResource", .Shares = 100})

        'Test
        testCompany.Produce()

        'Verify
        Dim produced As Resource = testCompany.GetAsset("ProducedResource")
        Assert.AreEqual(1, produced.Shares)

        Dim required As Resource = testCompany.GetAsset("TestResource")
        Assert.AreEqual(98, required.Shares)


    End Sub

    <TestMethod>
    Public Sub Company_Produce_ManyRequiredResource()

        'Setup
        Dim testCompany As New Company(New AssetManager())
        Dim requiredResource As New Resource() With {.Name = "TestResource", .Shares = 2}
        testCompany.RequiredResources.Add(requiredResource)

        requiredResource = New Resource() With {.Name = "TestResource2", .Shares = 3}
        testCompany.RequiredResources.Add(requiredResource)

        requiredResource = New Resource() With {.Name = "TestResource3", .Shares = 5}
        testCompany.RequiredResources.Add(requiredResource)

        testCompany.ProducedResource = New Resource() With {.Name = "ProducedResource", .Shares = 1}

        testCompany.AddResource(New Resource() With {.Name = "TestResource3", .Shares = 100})
        testCompany.AddResource(New Resource() With {.Name = "TestResource2", .Shares = 100})
        testCompany.AddResource(New Resource() With {.Name = "TestResource", .Shares = 100})

        'Test
        testCompany.Produce()

        'Verify
        Dim produced As Resource = testCompany.GetAsset("ProducedResource")
        Assert.AreEqual(1, produced.Shares)

        Dim required As Resource = testCompany.GetAsset("TestResource")
        Assert.AreEqual(98, required.Shares)

        required = testCompany.GetAsset("TestResource2")
        Assert.AreEqual(97, required.Shares)

        required = testCompany.GetAsset("TestResource3")
        Assert.AreEqual(95, required.Shares)

    End Sub

    <TestMethod>
    Public Sub Company_Produce_NotEnoughResources()
        'Setup
        Dim testCompany As New Company(New AssetManager())
        Dim requiredResource As New Resource() With {.Name = "TestResource", .Shares = 10}
        testCompany.RequiredResources.Add(requiredResource)
        testCompany.ProducedResource = New Resource() With {.Name = "ProducedResource", .Shares = 1}

        testCompany.AddResource(New Resource() With {.Name = "TestResource", .Shares = 5})

        'Test
        testCompany.Produce()

        'Verify
        Dim produced As Resource = testCompany.GetAsset("ProducedResource")
        Assert.IsNull(produced)

        Dim required As Resource = testCompany.GetAsset("TestResource")
        Assert.AreEqual(5, required.Shares)
    End Sub

    <TestMethod>
    Public Sub Company_Produce_ManyRequiredResource_MissingOne()
        'Setup
        Dim testCompany As New Company(New AssetManager())
        Dim requiredResource As New Resource() With {.Name = "TestResource", .Shares = 2}
        testCompany.RequiredResources.Add(requiredResource)

        requiredResource = New Resource() With {.Name = "TestResource2", .Shares = 3}
        testCompany.RequiredResources.Add(requiredResource)

        requiredResource = New Resource() With {.Name = "TestResource3", .Shares = 500}
        testCompany.RequiredResources.Add(requiredResource)

        testCompany.ProducedResource = New Resource() With {.Name = "ProducedResource", .Shares = 1}

        testCompany.AddResource(New Resource() With {.Name = "TestResource", .Shares = 100})
        testCompany.AddResource(New Resource() With {.Name = "TestResource2", .Shares = 50})
        testCompany.AddResource(New Resource() With {.Name = "TestResource3", .Shares = 10})


        'Test
        testCompany.Produce()

        'Verify
        Dim produced As Resource = testCompany.GetAsset("ProducedResource")
        Assert.IsNull(produced)

        Dim required As Resource = testCompany.GetAsset("TestResource")
        Assert.AreEqual(100, required.Shares)

        required = testCompany.GetAsset("TestResource2")
        Assert.AreEqual(50, required.Shares)

        required = testCompany.GetAsset("TestResource3")
        Assert.AreEqual(10, required.Shares)
    End Sub

    <TestMethod()>
    Public Sub Company_AddResource()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 1337}

        'test
        testCompany.AddResource(newResourceToAdd)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
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
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 0}

        'test
        testCompany.AddResource(newResourceToAdd)

    End Sub

    <TestMethod()>
    Public Sub Company_RemoveResource()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 20}
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 5}
        testCompany.AddResource(newResourceToAdd)
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Company_RemoveResource_NoResource()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 5}
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Company_RemoveResource_NotEnough()
        'setup
        Dim testCompany As New Company(New AssetManager())
        Dim newResourceToAdd As New Resource() With {.Name = "TestResource", .Shares = 5}
        Dim resourceToRemove As New Resource() With {.Name = "TestResource", .Shares = 20}
        testCompany.AddResource(newResourceToAdd)
        'test
        testCompany.RemoveResource(resourceToRemove)

        'verify
        Assert.AreEqual(1, testCompany.GetAllAssets().Count)
        Dim resourceToCheck As Resource = testCompany.GetAllAssets().First()
        Assert.AreEqual("TestResource", resourceToCheck.Name)
        Assert.AreEqual(15, resourceToCheck.Shares)

    End Sub

End Class