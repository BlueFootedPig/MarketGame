Public Class Market
    Public sellingOfferings As New List(Of Transaction)
    Friend TimeFrame As Double


    Private Function GetResourceValue(item As Resource) As Double
        Return 0
    End Function

    'Posts an bid up for sale
    Sub Buy(item As Transaction, theCompany As Company)

        'Dim requiredResources = theCompany.Assests.FirstOrDefault(Function(n) n.Name = item.AskingResource)
        'If requiredResources Is Nothing Then Exit Sub

        'If requiredResources.Shares > item.PricePerUnit * item.Resource.Shares Then

        'End If


    End Sub

    'Posts an item for sale
    Sub Sell(pricePerUnit As Integer, item As Resource, theCompany As Company)
        Dim nTransaction As New Transaction()
        nTransaction.Seller = theCompany.Name
        nTransaction.Resource = item
        nTransaction.PricePerUnit = pricePerUnit

        sellingOfferings.Add(nTransaction)
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

