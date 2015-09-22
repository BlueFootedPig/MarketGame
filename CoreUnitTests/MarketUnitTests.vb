Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

<TestClass()>
Public Class MarketUnitTests

    <TestMethod()>
    Public Sub Market_AddSellOrder()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}
        companySelling.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 2000})
        'test
        testMarket.Sell(140, resourceToSell, companySelling)

        'validation
        Assert.AreEqual(1, testMarket.SellingOfferings.Count)
        Assert.AreEqual(companySelling.Name, testMarket.SellingOfferings.First().Owner.Name)
        Assert.AreEqual(140.0, testMarket.SellingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourceToSell.Name, testMarket.SellingOfferings.First().Resource.Name)
        Assert.AreEqual(663, companySelling.GetAsset("TestResource").Shares)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Market_AddSellOrder_NoShares()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 0}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Sell(140, resourceToSell, companySelling)


    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Market_AddSellOrder_NullCompany()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 1337}

        'test
        testMarket.Sell(140, resourceToSell, Nothing)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Market_AddSellOrder_NullResource()

        'setup
        Dim testMarket As New Market
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Sell(140, Nothing, companySelling)


    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Market_AddSellOrder_NoSharesSpecified()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource"}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Sell(140, resourceToSell, companySelling)


    End Sub


    <TestMethod()>
    Public Sub Market_BuyOrder()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Buy(140, resourceToSell, companySelling)


        'validation
        Assert.AreEqual(1, testMarket.BuyingOfferings.Count)
        Assert.AreEqual(companySelling.Name, testMarket.BuyingOfferings.First().Owner.Name)
        Assert.AreEqual(140.0, testMarket.BuyingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourceToSell.Name, testMarket.BuyingOfferings.First().Resource.Name)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Market_AddBuyOrder_NoShares()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 0}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Buy(140, resourceToSell, companySelling)


    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Market_AddBuyOrder_NullCompany()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 1337}

        'test
        testMarket.Buy(140, resourceToSell, Nothing)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub Market_AddBuyOrder_NullResource()

        'setup
        Dim testMarket As New Market
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Buy(140, Nothing, companySelling)


    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Market_AddBuyOrder_NoSharesSpecified()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource"}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}

        'test
        testMarket.Buy(140, resourceToSell, companySelling)


    End Sub

    <TestMethod()>
    Public Sub Market_SellOrderAlreadyExists_Fulfilled()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}
        companySelling.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 10000})

        Dim resourcetobuy As New CraftResource() With {.Name = "TestResource", .Shares = 1}
        Dim companyBuying As New Company(New AssetManager()) With {.Name = "Buyer"}


        companyBuying.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 150})
        'test

        testMarket.Sell(100, resourceToSell, companySelling)
        testMarket.Buy(140, resourcetobuy, companyBuying)


        'validation
        Assert.AreEqual(0, testMarket.BuyingOfferings.Count)
        Assert.AreEqual(1, testMarket.SellingOfferings.Count)
        Assert.AreEqual(companySelling.Name, testMarket.SellingOfferings.First().Owner.Name)
        Assert.AreEqual(100.0, testMarket.SellingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourceToSell.Name, testMarket.SellingOfferings.First().Resource.Name)
        Assert.AreEqual(1336, testMarket.SellingOfferings(0).Resource.Shares)

    End Sub

    <TestMethod()>
    Public Sub Market_BuyOrderAlreadyExists_NotEnoughBuys()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}
        companySelling.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 10000})

        Dim resourcetobuy As New CraftResource() With {.Name = "TestResource", .Shares = 1}
        Dim companyBuying As New Company(New AssetManager()) With {.Name = "Buyer"}


        companyBuying.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 150})
        'test

        testMarket.Buy(140, resourcetobuy, companyBuying)
        testMarket.Sell(100, resourceToSell, companySelling)



        'validation
        Assert.AreEqual(0, testMarket.BuyingOfferings.Count)
        Assert.AreEqual(1, testMarket.SellingOfferings.Count)
        Assert.AreEqual(companySelling.Name, testMarket.SellingOfferings.First().Owner.Name)
        Assert.AreEqual(100.0, testMarket.SellingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourceToSell.Name, testMarket.SellingOfferings.First().Resource.Name)
        Assert.AreEqual(1336, testMarket.SellingOfferings(0).Resource.Shares)

    End Sub

    <TestMethod()>
    Public Sub Market_BuyOrderAlreadyExists_Fullfilled()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New CraftResource() With {.Name = "TestResource", .Shares = 5}
        Dim companySelling As New Company(New AssetManager()) With {.Name = "TheCompany"}
        companySelling.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 1000})

        Dim resourcetobuy As New CraftResource() With {.Name = "TestResource", .Shares = 15}
        Dim companyBuying As New Company(New AssetManager()) With {.Name = "Buyer"}


        companyBuying.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 1500})
        'test

        testMarket.Buy(140, resourcetobuy, companyBuying)
        testMarket.Sell(100, resourceToSell, companySelling)



        'validation
        Assert.AreEqual(1, testMarket.BuyingOfferings.Count)
        Assert.AreEqual(0, testMarket.SellingOfferings.Count)
        Assert.AreEqual(companyBuying.Name, testMarket.BuyingOfferings.First().Owner.Name)
        Assert.AreEqual(140.0, testMarket.BuyingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourcetobuy.Name, testMarket.BuyingOfferings.First().Resource.Name)
        Assert.AreEqual(10, testMarket.BuyingOfferings(0).Resource.Shares)

    End Sub




End Class