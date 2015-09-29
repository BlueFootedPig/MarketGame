Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()> Public Class ResourceProductionUnitTests



    <TestMethod>
    Public Sub Company_Produce_SingleRequiredItem()
        'Setup

        Dim settings As New EngineSettings
        settings.minWage = 0
        Dim testProduction As New ResourceProduction(settings)

        Dim requiredResource As New CraftResource() With {.Name = "TestResource", .Shares = 2}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource2", .Shares = 3}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource3", .Shares = 5}
        testProduction.RequiredResources.Add(requiredResource)

        testProduction.ProducedResource = New CraftResource() With {.Name = "ProducedResource", .Shares = 1}

        Dim assets As New AssetManager

        assets.AddAsset(New CraftResource() With {.Name = "TestResource3", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource2", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource", .Shares = 100})
        'Dim credit As IResource = IResource.CREDIT.CopyResource()
        'credit.Shares = 2
        'assets.AddAsset(credit)
        'Test
        testProduction.Produce(assets)

        'Verify
        Dim produced As CraftResource = assets.GetResource("ProducedResource")
        Assert.AreEqual(1, produced.Shares)

        Dim required As CraftResource = assets.GetResource("TestResource")
        Assert.AreEqual(98, required.Shares)

        required = assets.GetResource("TestResource2")
        Assert.AreEqual(97, required.Shares)

        required = assets.GetResource("TestResource3")
        Assert.AreEqual(95, required.Shares)

    End Sub

    <TestMethod>
    Public Sub Company_Produce_NoMoney()
        'Setup

        Dim settings As New EngineSettings
        settings.minWage = 1
        Dim testProduction As New ResourceProduction(settings)

        Dim requiredResource As New CraftResource() With {.Name = "TestResource", .Shares = 2}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource2", .Shares = 3}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource3", .Shares = 5}
        testProduction.RequiredResources.Add(requiredResource)

        testProduction.ProducedResource = New CraftResource() With {.Name = "ProducedResource", .Shares = 1}

        Dim assets As New AssetManager

        assets.AddAsset(New CraftResource() With {.Name = "TestResource3", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource2", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource", .Shares = 100})

        'Test
        testProduction.Produce(assets)

        'Verify
        Dim produced As CraftResource = assets.GetResource("ProducedResource")
        Assert.IsNull(produced)

        Dim required As CraftResource = assets.GetResource("TestResource")
        Assert.AreEqual(100, required.Shares)

    End Sub


    <TestMethod>
    Public Sub Company_Produce_ManyRequiredResource()

        'Setup
        Dim settings As New EngineSettings
        settings.minWage = 0
        Dim testProduction As New ResourceProduction(settings)
        Dim requiredResource As New CraftResource() With {.Name = "TestResource", .Shares = 2}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource2", .Shares = 3}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource3", .Shares = 5}
        testProduction.RequiredResources.Add(requiredResource)

        testProduction.ProducedResource = New CraftResource() With {.Name = "ProducedResource", .Shares = 1}


        Dim assets As New AssetManager
        assets.AddAsset(New CraftResource() With {.Name = "TestResource3", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource2", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource", .Shares = 100})

        'Test
        testProduction.Produce(assets)

        'Verify
        Dim produced As CraftResource = assets.GetResource("ProducedResource")
        Assert.AreEqual(1, produced.Shares)

        Dim required As CraftResource = assets.GetResource("TestResource")
        Assert.AreEqual(98, required.Shares)

        required = assets.GetResource("TestResource2")
        Assert.AreEqual(97, required.Shares)

        required = assets.GetResource("TestResource3")
        Assert.AreEqual(95, required.Shares)

    End Sub

    <TestMethod>
    Public Sub Company_Produce_NotEnoughResources()
        'Setup
        Dim settings As New EngineSettings
        Dim testProduction As New ResourceProduction(settings)
        Dim requiredResource As New CraftResource() With {.Name = "TestResource", .Shares = 10}
        testProduction.RequiredResources.Add(requiredResource)
        testProduction.ProducedResource = New CraftResource() With {.Name = "ProducedResource", .Shares = 1}

        Dim assets As New AssetManager
        assets.AddAsset(New CraftResource() With {.Name = "TestResource", .Shares = 5})

        'Test
        testProduction.Produce(assets)

        'Verify
        Dim produced As CraftResource = assets.GetResource("ProducedResource")
        Assert.IsNull(produced)

        Dim required As CraftResource = assets.GetResource("TestResource")
        Assert.AreEqual(5, required.Shares)
    End Sub

    <TestMethod>
    Public Sub Company_Produce_ManyRequiredResource_MissingOne()
        'Setup
        Dim settings As New EngineSettings
        Dim testProduction As New ResourceProduction(settings)
        Dim requiredResource As New CraftResource() With {.Name = "TestResource", .Shares = 2}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource2", .Shares = 3}
        testProduction.RequiredResources.Add(requiredResource)

        requiredResource = New CraftResource() With {.Name = "TestResource3", .Shares = 500}
        testProduction.RequiredResources.Add(requiredResource)

        testProduction.ProducedResource = New CraftResource() With {.Name = "ProducedResource", .Shares = 1}

        Dim assets As New AssetManager
        assets.AddAsset(New CraftResource() With {.Name = "TestResource", .Shares = 100})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource2", .Shares = 50})
        assets.AddAsset(New CraftResource() With {.Name = "TestResource3", .Shares = 10})


        'Test
        testProduction.Produce(assets)

        'Verify
        Dim produced As CraftResource = assets.GetResource("ProducedResource")
        Assert.IsNull(produced)

        Dim required As CraftResource = assets.GetResource("TestResource")
        Assert.AreEqual(100, required.Shares)

        required = assets.GetResource("TestResource2")
        Assert.AreEqual(50, required.Shares)

        required = assets.GetResource("TestResource3")
        Assert.AreEqual(10, required.Shares)
    End Sub

End Class