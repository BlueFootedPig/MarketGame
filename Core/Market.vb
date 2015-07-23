Public Class Market
    Public Property SellingOfferings As New List(Of Transaction)
    Public Property BuyingOfferings As New List(Of Transaction)


    Private knownResources As IList(Of Resource)

    Private Function GetResourceValue(item As Resource) As Double
        Return knownResources.First(Function(n) n.Name = item.Name).MarketValue
    End Function

    'Posts an bid up for sale
    Sub Buy(pricePerUnit As Integer, item As Resource, theCompany As Company)

        Dim nTransaction As New Transaction()
        nTransaction.Seller = theCompany.Name
        nTransaction.Resource = item
        nTransaction.PricePerUnit = pricePerUnit

        BuyingOfferings.Add(nTransaction)

    End Sub

    'Posts an item for sale
    Sub Sell(pricePerUnit As Integer, item As Resource, theCompany As Company)
        Dim nTransaction As New Transaction()
        nTransaction.Seller = theCompany.Name
        nTransaction.Resource = item
        nTransaction.PricePerUnit = pricePerUnit

        SellingOfferings.Add(nTransaction)
    End Sub

    Sub CompleteTransaction(trans As Transaction, numberToBuy As Integer, theCompany As Company)

        theCompany.Assests.AddAsset(New Resource() With {.Name = trans.Resource.Name, .Shares = numberToBuy})
        trans.Resource.Shares -= numberToBuy

        If trans.Resource.Shares <= 0 Then
            sellingOfferings.Remove(trans)
        End If

    End Sub

End Class


Public Class Transaction
    Public Id As New Guid()
    Public Seller As String
    Public PricePerUnit As Double
    Public Resource As Resource
End Class

