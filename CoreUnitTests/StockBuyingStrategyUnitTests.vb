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
        Assert.AreEqual(2, myCompany.GetAllAssets.Count)
        Dim resourceBought As Resource = myCompany.GetAllAssets.First(Function(n) n.Name = "TestResource")
        Assert.AreEqual(1, resourceBought.Shares)
        Dim credits As Resource = myCompany.GetAllAssets.First(Function(n) n.Name = Resource.CREDIT)
        Assert.AreEqual(990, credits.Shares)

    End Sub

End Class