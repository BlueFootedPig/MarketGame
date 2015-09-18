Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()>
Public Class StandardSpendingCampaignUnitTests

    <TestMethod()>
    Public Sub StandardSpendingCampaign_BuyPartial()

        Dim testSpendingCampaign As New StandardSpendingCampaign
        Dim mockmarket As IMarket = Substitute.For(Of IMarket)()

        'setup wallet
        Dim wallet As New Dictionary(Of String, Double)
        wallet.Add("TestTag", 100)

        'return listings for sale
        Dim sellingList As New List(Of Transaction)
        Dim itemForSale As New LuxuryResource() With {.Name = "TestStool", .Shares = 100, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 10, .Resource = itemForSale})
        mockmarket.SellingOfferings.Returns(sellingList)


        testSpendingCampaign.RunCamapign(wallet, mockmarket)

        mockmarket.Received().Buy(10, New Resource() With {.Name = "TestStool", .Shares = 10}, Company.VoidCompany)

        Assert.AreEqual(0.0, wallet("TestTag"))

    End Sub

    <TestMethod()>
    Public Sub StandardSpendingCampaign_Buy_NoItemsForSale()

        Dim testSpendingCampaign As New StandardSpendingCampaign
        Dim mockmarket As IMarket = Substitute.For(Of IMarket)()

        'setup wallet
        Dim wallet As New Dictionary(Of String, Double)
        wallet.Add("TestTag", 100)

        'return listings for sale
        Dim sellingList As New List(Of Transaction)
        mockmarket.SellingOfferings.Returns(sellingList)


        testSpendingCampaign.RunCamapign(wallet, mockmarket)

        mockmarket.DidNotReceiveWithAnyArgs().Buy(0, Nothing, Nothing)

        Assert.AreEqual(100.0, wallet("TestTag"))

    End Sub

    <TestMethod()>
    Public Sub StandardSpendingCampaign_BuyPartial_ItemWithPrefferedTag()

        Dim testSpendingCampaign As New StandardSpendingCampaign
        Dim mockmarket As IMarket = Substitute.For(Of IMarket)()

        'setup wallet
        Dim wallet As New Dictionary(Of String, Double)
        wallet.Add("TestTag", 100)

        'return listings for sale
        Dim sellingList As New List(Of Transaction)
        Dim itemForSale As New LuxuryResource() With {.Name = "TestStool", .Shares = 100, .Level = 1}
        itemForSale.AddPrefferedCustomer("TestTag")
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 10, .Resource = itemForSale})

        Dim worseItemForSale As New LuxuryResource() With {.Name = "WrongStoole", .Shares = 100, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 9, .Resource = worseItemForSale})

        mockmarket.SellingOfferings.Returns(sellingList)


        testSpendingCampaign.RunCamapign(wallet, mockmarket)

        mockmarket.Received().Buy(10, New Resource() With {.Name = "TestStool", .Shares = 10}, Company.VoidCompany)

        Assert.AreEqual(0.0, wallet("TestTag"))

    End Sub

    <TestMethod()>
    Public Sub StandardSpendingCampaign_BuyPartial_BetterItemLevel()

        Dim testSpendingCampaign As New StandardSpendingCampaign
        Dim mockmarket As IMarket = Substitute.For(Of IMarket)()

        'setup wallet
        Dim wallet As New Dictionary(Of String, Double)
        wallet.Add("TestTag", 100)

        'return listings for sale
        Dim sellingList As New List(Of Transaction)
        Dim itemForSale As New LuxuryResource() With {.Name = "TestStool", .Shares = 100, .Level = 5}
        itemForSale.AddPrefferedCustomer("TestTag")
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 10, .Resource = itemForSale})

        Dim worseItemForSale As New LuxuryResource() With {.Name = "WrongStoole", .Shares = 100, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 7, .Resource = worseItemForSale})

        mockmarket.SellingOfferings.Returns(sellingList)


        testSpendingCampaign.RunCamapign(wallet, mockmarket)

        mockmarket.Received().Buy(10, New Resource() With {.Name = "TestStool", .Shares = 10}, Company.VoidCompany)

        Assert.AreEqual(0.0, wallet("TestTag"))

    End Sub

    <TestMethod()>
    Public Sub StandardSpendingCampaign_BuyAll_SingleTransaction()

        Dim testSpendingCampaign As New StandardSpendingCampaign
        Dim mockmarket As IMarket = Substitute.For(Of IMarket)()

        'setup wallet
        Dim wallet As New Dictionary(Of String, Double)
        wallet.Add("TestTag", 100000000)

        'return listings for sale
        Dim sellingList As New List(Of Transaction)
        Dim itemForSale As New LuxuryResource() With {.Name = "TestStool", .Shares = 100, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 10, .Resource = itemForSale})
        mockmarket.SellingOfferings.Returns(sellingList)


        testSpendingCampaign.RunCamapign(wallet, mockmarket)

        mockmarket.Received().Buy(10, New Resource() With {.Name = "TestStool", .Shares = 100}, Company.VoidCompany)

    End Sub

    <TestMethod()>
    Public Sub StandardSpendingCampaign_BuyAll_3Transactions()

        Dim testSpendingCampaign As New StandardSpendingCampaign
        Dim mockmarket As IMarket = Substitute.For(Of IMarket)()

        'setup wallet
        Dim wallet As New Dictionary(Of String, Double)
        wallet.Add("TestTag", 10000000)

        'return listings for sale
        Dim sellingList As New List(Of Transaction)
        Dim itemForSale As New LuxuryResource() With {.Name = "TestStool", .Shares = 100, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 10, .Resource = itemForSale})

        Dim itemForSale2 As New LuxuryResource() With {.Name = "TestStool", .Shares = 200, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 15, .Resource = itemForSale2})
        mockmarket.SellingOfferings.Returns(sellingList)

        Dim itemForSale3 As New LuxuryResource() With {.Name = "TestStool", .Shares = 500, .Level = 1}
        sellingList.Add(New Transaction() With {.Owner = Company.VoidCompany, .PricePerUnit = 20, .Resource = itemForSale3})
        mockmarket.SellingOfferings.Returns(sellingList)

        testSpendingCampaign.RunCamapign(wallet, mockmarket)

        mockmarket.Received().Buy(10, New Resource() With {.Name = "TestStool", .Shares = 100}, Company.VoidCompany)
        mockmarket.Received().Buy(15, New Resource() With {.Name = "TestStool", .Shares = 200}, Company.VoidCompany)
        mockmarket.Received().Buy(20, New Resource() With {.Name = "TestStool", .Shares = 500}, Company.VoidCompany)



    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub StandardSpendingCampaign_NoWallet()
        Dim testSpendingCampaign As New StandardSpendingCampaign()
        testSpendingCampaign.RunCamapign(Nothing, Substitute.For(Of IMarket))
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub StandardSpendingCampaign_NoMarket()
        Dim testSpendingCampaign As New StandardSpendingCampaign()
        testSpendingCampaign.RunCamapign(New Dictionary(Of String, Double), Nothing)
    End Sub


End Class