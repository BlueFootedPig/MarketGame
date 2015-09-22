﻿Public Interface IStrategy
    Sub Execute(company As Company, theMarket As IMarket)
End Interface

Public Class StockBuyStrategy
    Implements IStrategy
    Dim baselinePrice As Integer
    Dim idealShares As Integer
    Dim ResourceToBuyName As String

    ''' <summary>
    ''' Sets up a strategy of buying resources.
    ''' </summary>
    ''' <param name="baseline">Price to buy at.</param>
    ''' <param name="shares">Ideal amount to hold onto.</param>
    ''' <param name="resourceToBuy">Name of Resource to buy.</param>
    ''' <remarks></remarks>
    Public Sub New(baseline As Integer, shares As Integer, resourceToBuy As String)
        baselinePrice = baseline
        idealShares = shares
        ResourceToBuyName = resourceToBuy
    End Sub

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements IStrategy.Execute

        Dim currentResource As CraftResource = theCompany.GetAsset(ResourceToBuyName)
        Dim numberOfShares As Integer = 0
        If currentResource IsNot Nothing Then
            numberOfShares = currentResource.Shares
        End If

        Dim listOfMyOfferings As IEnumerable(Of Transaction) = theMarket.BuyingOfferings.Where(Function(n) n.Owner.Name = theCompany.Name AndAlso n.Resource.Name = ResourceToBuyName)
        If listOfMyOfferings.Count > 0 Then numberOfShares += listOfMyOfferings.Sum(Function(n) n.Resource.Shares)


        If numberOfShares < idealShares Then
            theMarket.Buy(baselinePrice, New CraftResource() With {.Name = ResourceToBuyName, .Shares = idealShares - numberOfShares}, theCompany)
        End If

    End Sub
End Class

Public Class StockSellingBasicStrategy
    Implements IStrategy
    Dim baselinePrice As Integer
    Dim idealShares As Integer
    Dim ResourceToSellName As String

    Public Sub New(baseline As Integer, shares As Integer, resourceToSell As String)
        ResourceToSellName = resourceToSell
        baselinePrice = baseline
        idealShares = shares
    End Sub

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements IStrategy.Execute
        Dim currentResource As CraftResource = theCompany.GetAsset(ResourceToSellName)
        Dim numberOfShares As Integer = 0
        If currentResource IsNot Nothing Then
            numberOfShares = currentResource.Shares
        End If

        If numberOfShares > idealShares Then
            theMarket.Sell(baselinePrice, New CraftResource() With {.Name = ResourceToSellName, .Shares = numberOfShares - idealShares}, theCompany)
        End If

    End Sub
End Class
