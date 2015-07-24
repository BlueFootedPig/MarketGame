Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core

<TestClass()>
Public Class MarketUnitTests

    <TestMethod()>
    Public Sub Market_AddSellOrder()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New Resource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company() With {.Name = "TheCompany"}

        'test
        testMarket.Sell(140, resourceToSell, companySelling)

        'validation
        Assert.AreEqual(1, testMarket.SellingOfferings.Count)
        Assert.AreEqual(companySelling.Name, testMarket.SellingOfferings.First().Owner.Name)
        Assert.AreEqual(140.0, testMarket.SellingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourceToSell.Name, testMarket.SellingOfferings.First().Resource.Name)

    End Sub


    <TestMethod()>
    Public Sub Market_BuyOrder()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New Resource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company() With {.Name = "TheCompany"}

        'test
        testMarket.Buy(140, resourceToSell, companySelling)


        'validation
        Assert.AreEqual(1, testMarket.BuyingOfferings.Count)
        Assert.AreEqual(companySelling.Name, testMarket.BuyingOfferings.First().Owner.Name)
        Assert.AreEqual(140.0, testMarket.BuyingOfferings.First().PricePerUnit)
        Assert.AreEqual(resourceToSell.Name, testMarket.BuyingOfferings.First().Resource.Name)

    End Sub

    <TestMethod()>
    Public Sub Market_BuyOrder_SellOrderAlreadyExists()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New Resource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company() With {.Name = "TheCompany"}
        companySelling.Assests.AddAsset(New Resource() With {.Name = "TestResource", .Shares = 1000})

        Dim resourcetobuy As New Resource() With {.Name = "TestResource", .Shares = 1}
        Dim companyBuying As New Company() With {.Name = "Buyer"}


        companyBuying.Assests.AddAsset(New Resource() With {.Name = Resource.CREDIT, .Shares = 150})
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
    Public Sub Market_SellOrder_BuyOrderAlreadyExists()

        'setup
        Dim testMarket As New Market
        Dim resourceToSell As New Resource() With {.Name = "TestResource", .Shares = 1337}
        Dim companySelling As New Company() With {.Name = "TheCompany"}
        companySelling.Assests.AddAsset(New Resource() With {.Name = "TestResource", .Shares = 1000})

        Dim resourcetobuy As New Resource() With {.Name = "TestResource", .Shares = 1}
        Dim companyBuying As New Company() With {.Name = "Buyer"}


        companyBuying.Assests.AddAsset(New Resource() With {.Name = Resource.CREDIT, .Shares = 150})
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




End Class