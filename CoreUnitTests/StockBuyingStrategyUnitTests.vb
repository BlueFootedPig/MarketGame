Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()>
Public Class StockBuyingStrategyUnitTests

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_NoResourcesYet()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()
        mockMarket.BuyingOfferings.Returns(New List(Of Transaction))

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
        Dim testStrategy As New StockBuyStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Buy(100, New Resource() With {.Name = "TestResource", .Shares = 500}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_SomeResources()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()
        mockMarket.BuyingOfferings.Returns(New List(Of Transaction))

        Dim seller As New Company()
        seller.AddResource(New Resource() With {.Name = "TestResource", .Shares = 1000})


        Dim myCompany As New Company()
        myCompany.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000})
        myCompany.AddResource(New Resource() With {.Name = "TestResource", .Shares = 400})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 500

        'test
        Dim testStrategy As New StockBuyStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Buy(100, New Resource() With {.Name = "TestResource", .Shares = 100}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockBuyingStrategy_BuyingRequiredResources_EnoughResources()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()
        mockMarket.BuyingOfferings.Returns(New List(Of Transaction))

        Dim seller As New Company()
        seller.AddResource(New Resource() With {.Name = "TestResource", .Shares = 1000})


        Dim myCompany As New Company()
        myCompany.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000})
        myCompany.AddResource(New Resource() With {.Name = "TestResource", .Shares = 600})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 500

        'test
        Dim testStrategy As New StockBuyStrategy(priceToBuy, sharesToHave, "TestResource")
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.DidNotReceiveWithAnyArgs.Buy(100, New Resource() With {.Name = "TestResource", .Shares = 100}, myCompany)

    End Sub
   


End Class