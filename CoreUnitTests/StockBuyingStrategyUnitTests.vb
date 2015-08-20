Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()>
Public Class StockBuyingStrategyUnitTests

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_NoResourcesYet_NotEnoughOnMarket()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company()
        seller.AddResource(New Resource() With {.Name = "TestResource", .Shares = 1000})

        Dim nTransaction As New Transaction() With {.Owner = seller, .PricePerUnit = 10, .Resource = New Resource() With {.Name = "TestResource", .Shares = 1}}
        Dim returnList As New List(Of Transaction)
        returnList.Add(nTransaction)

        mockMarket.SellingOfferings.Returns(returnList)


        Dim myCompany As New Company()
        myCompany.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 500

        'test
        Dim testStrategy As New StockBuyBackStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Buy(10, New Resource() With {.Name = "TestResource", .Shares = 1}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_NoResourcesYet_EnoughFromSingleSeller()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company()
        seller.AddResource(New Resource() With {.Name = "TestResource", .Shares = 1000})

        Dim nTransaction As New Transaction() With {.Owner = seller, .PricePerUnit = 10, .Resource = New Resource() With {.Name = "TestResource", .Shares = 1000}}
        Dim returnList As New List(Of Transaction)
        returnList.Add(nTransaction)

        mockMarket.SellingOfferings.Returns(returnList)


        Dim myCompany As New Company()
        myCompany.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 100

        'test
        Dim testStrategy As New StockBuyBackStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Buy(10, New Resource() With {.Name = "TestResource", .Shares = 100}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_NoResourcesYet_EnoughFromMultipleSeller()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company()
        seller.AddResource(New Resource() With {.Name = "TestResource", .Shares = 1000})

        Dim returnList As New List(Of Transaction)
        Dim nTransaction As New Transaction() With {.Owner = seller, .PricePerUnit = 10, .Resource = New Resource() With {.Name = "TestResource", .Shares = 20}}
        returnList.Add(nTransaction)

        nTransaction = New Transaction() With {.Owner = seller, .PricePerUnit = 20, .Resource = New Resource() With {.Name = "TestResource", .Shares = 30}}
        returnList.Add(nTransaction)

        nTransaction = New Transaction() With {.Owner = seller, .PricePerUnit = 50, .Resource = New Resource() With {.Name = "TestResource", .Shares = 100}}
        returnList.Add(nTransaction)

        mockMarket.SellingOfferings.Returns(returnList)


        Dim myCompany As New Company()
        myCompany.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 100

        'test
        Dim testStrategy As New StockBuyBackStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Buy(10, New Resource() With {.Name = "TestResource", .Shares = 20}, myCompany)
        mockMarket.Received().Buy(20, New Resource() With {.Name = "TestResource", .Shares = 30}, myCompany)
        mockMarket.Received().Buy(50, New Resource() With {.Name = "TestResource", .Shares = 50}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_NoResourcesYet_TooExpensive()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company()
        seller.AddResource(New Resource() With {.Name = "TestResource", .Shares = 1000})

        Dim returnList As New List(Of Transaction)
        Dim nTransaction As New Transaction() With {.Owner = seller, .PricePerUnit = 1000, .Resource = New Resource() With {.Name = "TestResource", .Shares = 20}}
        returnList.Add(nTransaction)


        mockMarket.SellingOfferings.Returns(returnList)


        Dim myCompany As New Company()
        myCompany.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000})

        Dim priceToBuy As Integer = 200
        Dim sharesToHave As Integer = 100

        'test
        Dim testStrategy As New StockBuyBackStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.DidNotReceiveWithAnyArgs().Buy(100, Nothing, Nothing)


    End Sub

End Class