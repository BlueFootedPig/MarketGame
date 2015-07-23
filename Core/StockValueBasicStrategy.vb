Public Interface Strategy
    Sub Execute(company As Company, theMarket As Market)
End Interface

Public Class StockBuyBackStrategy
    Implements Strategy
    Dim baselinePrice As Integer
    Dim idealShares As Integer
    Dim ResourceToBuyName As String

    Public Sub New(baseline As Integer, shares As Integer, resourceToBuy As String)
        baselinePrice = baseline
        idealShares = shares
        ResourceToBuyName = resourceToBuy
    End Sub

    Public Sub Execute(theCompany As Company, theMarket As Market) Implements Strategy.Execute
        Dim coeffecient As Double = idealShares / theCompany.Shares 'idealShares / ( idealShares - theCompany.Shares)
        Dim currentValue As Double = coeffecient * baselinePrice

        'Buying Pattern
        'For Each item As Transaction In theMarket.sellingOfferings.Where(Function(n) n.Resource.Name = ResourceToBuyName)
        '    If item.PricePerUnit < currentValue Then
        '        theMarket.Buy(item, theCompany)
        '    End If
        'Next


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

    Public Sub Execute(theCompany As Company, theMarket As Market) Implements Strategy.Execute
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
