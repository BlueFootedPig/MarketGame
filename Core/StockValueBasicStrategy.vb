﻿Public Interface Strategy
    Sub Execute(company As Company, theMarket As IMarket)
End Interface

Public Class StockBuyBackStrategy
    Implements Strategy
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

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements Strategy.Execute
        Dim listOfWantedResources As IEnumerable(Of Transaction) = theMarket.SellingOfferings.Where(Function(n) n.Resource.Name = ResourceToBuyName AndAlso n.PricePerUnit < baselinePrice)

        Dim sortedTransactions As IEnumerable(Of Transaction) = listOfWantedResources.OrderBy(Function(n) n.PricePerUnit)


        Dim currentResourceAmount As Resource = theCompany.GetAllAssets().FirstOrDefault(Function(n) n.Name = ResourceToBuyName)
        If currentResourceAmount Is Nothing Then currentResourceAmount = New Resource() With {.Name = ResourceToBuyName, .Shares = 0}
        Dim neededAmount As Integer = idealShares - currentResourceAmount.Shares


        For Each currentTransaction As Transaction In sortedTransactions
            If neededAmount <= 0 Then Exit For
            Dim amount As Integer = neededAmount
            If currentTransaction.Resource.Shares < amount Then amount = currentTransaction.Resource.Shares

            theMarket.Buy(currentTransaction.PricePerUnit, New Resource() With {.Name = ResourceToBuyName, .Shares = amount}, theCompany)

            neededAmount -= amount
        Next


    End Sub
End Class

Public Class StockSellingBasicStrategy
    Implements Strategy
    Dim baselinePrice As Integer
    Dim idealShares As Integer
    Dim ResourceToBuyName As String

    Public Sub New(baseline As Integer, shares As Integer, resourceToBuy As String)
        ResourceToBuyName = resourceToBuy
        baselinePrice = baseline
        idealShares = shares
    End Sub

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements Strategy.Execute
        'Dim coeffecient As Double = theCompany.Assests.First(Function(n) n.Name = ResourceToBuyName).Shares / idealShares
        'Dim currentValue As Double = coeffecient * baselinePrice

        ''Selling Pattern
        'For Each item As Transaction In theMarket.sellingOfferings.Where(Function(n) n.Resource.Name = ResourceToBuyName AndAlso n.AskingResource = theCompany.Name)
        '    If item.PricePerUnit > currentValue Then
        '        theMarket.CompleteTransaction(item, 1, theCompany)
        '    End If
        'Next

        'If theCompany.Shares > idealShares Then
        '    Dim numberInBatch As Integer = theCompany.Shares - idealShares
        '    If numberInBatch > 5 Then numberInBatch = 5
        '    Dim resourceToSell As New Resource() With {.Name = theCompany.Name, .Shares = numberInBatch}
        '    theMarket.Sell(currentValue, Resource.RESOURCE_COMMON, resourceToSell, theCompany)
        '    'theCompany.Resources.Add(New Resource() With {.Name = theCompany.Name, .Shares = numberInBatch})
        '    'theCompany.Shares -= numberInBatch
        'End If

    End Sub
End Class
