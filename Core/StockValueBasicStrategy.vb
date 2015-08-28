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

        Dim currentResource As Resource = theCompany.GetAsset(ResourceToBuyName)
        Dim numberOfShares As Integer = 0
        If currentResource IsNot Nothing Then
            numberOfShares = currentResource.Shares
        End If

        If numberOfShares < idealShares Then
            theMarket.Buy(baselinePrice, New Resource() With {.Name = ResourceToBuyName, .Shares = idealShares - numberOfShares}, theCompany)
        End If

    End Sub
End Class

Public Class StockSellingBasicStrategy
    Implements IStrategy
    Dim baselinePrice As Integer
    Dim idealShares As Integer
    Dim ResourceToBuyName As String

    Public Sub New(baseline As Integer, shares As Integer, resourceToBuy As String)
        ResourceToBuyName = resourceToBuy
        baselinePrice = baseline
        idealShares = shares
    End Sub

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements IStrategy.Execute
        Dim currentResource As Resource = theCompany.GetAsset(ResourceToBuyName)
        Dim numberOfShares As Integer = 0
        If currentResource IsNot Nothing Then
            numberOfShares = currentResource.Shares
        End If

        If numberOfShares > idealShares Then
            theMarket.Sell(baselinePrice, New Resource() With {.Name = ResourceToBuyName, .Shares = numberOfShares - idealShares}, theCompany)
        End If

    End Sub
End Class
