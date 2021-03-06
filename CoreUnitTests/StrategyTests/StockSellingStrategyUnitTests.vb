﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Core
Imports NSubstitute

<TestClass()>
Public Class StockSellingStrategyUnitTests
    <TestMethod()>
    Public Sub StockSellingStrategy_SellingRequiredResources_SellAll()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company(New AssetManager())
        seller.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 1000})

        Dim nTransaction As New Transaction() With {.Owner = seller, .PricePerUnit = 10, .Resource = New CraftResource() With {.Name = "TestResource", .Shares = 1}}
        Dim returnList As New List(Of Transaction)
        returnList.Add(nTransaction)

        mockMarket.SellingOfferings.Returns(returnList)


        Dim myCompany As New Company(New AssetManager())
        myCompany.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 1000})
        myCompany.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 500})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 0

        'test
        Dim testStrategy As New StockSellingBasicStrategy(priceToBuy, sharesToHave, New CraftResource() With {.Name = "TestResource"})
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Sell(100, New CraftResource() With {.Name = "TestResource", .Shares = 500}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockSellingStrategy_SellingRequiredResources_SomeResources()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company(New AssetManager())
        seller.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 1000})


        Dim myCompany As New Company(New AssetManager())
        myCompany.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 1000})
        myCompany.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 400})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 300

        'test
        Dim testStrategy As New StockSellingBasicStrategy(priceToBuy, sharesToHave, New CraftResource() With {.Name = "TestResource"})
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.Received().Sell(100, New CraftResource() With {.Name = "TestResource", .Shares = 100}, myCompany)

    End Sub

    <TestMethod()>
    Public Sub StockSellingStrategy_SellingRequiredResources_SellNone()
        'Setup
        Dim mockMarket As IMarket = Substitute.For(Of IMarket)()

        Dim seller As New Company(New AssetManager())
        seller.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 1000})


        Dim myCompany As New Company(New AssetManager())
        myCompany.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 1000})
        myCompany.AddResource(New CraftResource() With {.Name = "TestResource", .Shares = 100})

        Dim priceToBuy As Integer = 100
        Dim sharesToHave As Integer = 500

        'test
        Dim testStrategy As New StockSellingBasicStrategy(priceToBuy, sharesToHave, New CraftResource() With {.Name = "TestResource"})
        testStrategy.Execute(myCompany, mockMarket)

        'verify
        mockMarket.DidNotReceiveWithAnyArgs.Sell(100, New CraftResource() With {.Name = "TestResource", .Shares = 100}, myCompany)

    End Sub






End Class