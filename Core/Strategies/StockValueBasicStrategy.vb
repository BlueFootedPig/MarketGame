Public Interface IStrategy
    Sub Execute(company As Company, theMarket As IMarket)
    Function getStateObject() As StateObject
End Interface

Public Class StockBuyStrategy
    Implements IStrategy
    Private baselinePrice As Integer
    Private idealShares As Integer
    Private resourceToBuyName As String

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
        resourceToBuyName = resourceToBuy
    End Sub

    Public Function GetStateObject() As StateObject Implements IStrategy.getStateObject
        Dim nStateObject As New StateObject(Me)
        nStateObject.SetProperty("baselinePrice", baselinePrice)
        nStateObject.SetProperty("idealShares", idealShares)
        nStateObject.SetProperty("resourceToBuyName", resourceToBuyName)
        Return nStateObject
    End Function

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements IStrategy.Execute

        Dim currentResource As CraftResource = theCompany.GetAsset(resourceToBuyName)
        Dim numberOfShares As Integer = 0
        If currentResource IsNot Nothing Then
            numberOfShares = currentResource.Shares
        End If

        Dim listOfMyOfferings As IEnumerable(Of Transaction) = theMarket.BuyingOfferings.Where(Function(n) n.Owner.Name = theCompany.Name AndAlso n.Resource.Name = resourceToBuyName)
        If listOfMyOfferings.Count > 0 Then numberOfShares += listOfMyOfferings.Sum(Function(n) n.Resource.Shares)


        If numberOfShares < idealShares Then
            theMarket.Buy(baselinePrice, New CraftResource() With {.Name = resourceToBuyName, .Shares = idealShares - numberOfShares}, theCompany)
        End If

    End Sub
End Class

Public Class StockSellingBasicStrategy
    Implements IStrategy
    Private baselinePrice As Integer
    Private idealShares As Integer
    Private ResourceToSellName As IResource

    Public Sub New(baseline As Integer, shares As Integer, resourceToSell As IResource)
        ResourceToSellName = resourceToSell
        baselinePrice = baseline
        idealShares = shares
    End Sub

    Public Function GetStateObject() As StateObject Implements IStrategy.getStateObject
        Dim nStateObject As New StateObject(Me)
        nStateObject.SetProperty("baselinePrice", baselinePrice)
        nStateObject.SetProperty("idealShares", idealShares)
        nStateObject.SetProperty("ResourceToSellName", ResourceToSellName)
        Return nStateObject
    End Function

    Public Sub Execute(theCompany As Company, theMarket As IMarket) Implements IStrategy.Execute
        Dim currentResource As IResource = theCompany.GetAsset(ResourceToSellName.Name)
        Dim numberOfShares As Integer = 0
        If currentResource IsNot Nothing Then
            numberOfShares = currentResource.Shares
        End If

        If numberOfShares > idealShares Then
            Dim whatToSell As IResource = currentResource.CopyResource()
            whatToSell.Shares = numberOfShares - idealShares
            theMarket.Sell(baselinePrice, whatToSell, theCompany)
        End If

    End Sub
End Class
