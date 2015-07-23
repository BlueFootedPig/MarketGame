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
        nTransaction.Owner = theCompany
        nTransaction.Resource = item
        nTransaction.PricePerUnit = pricePerUnit

        CompleteBuyOrder(nTransaction)

        If nTransaction.Resource.Shares > 0 Then
            BuyingOfferings.Add(nTransaction)
        End If


    End Sub

    'Posts an item for sale
    Sub Sell(pricePerUnit As Integer, item As Resource, theCompany As Company)
        Dim nTransaction As New Transaction()
        nTransaction.Owner = theCompany
        nTransaction.Resource = item
        nTransaction.PricePerUnit = pricePerUnit

        SellingOfferings.Add(nTransaction)
    End Sub

    Private Sub CompleteSellOrder(selling As Transaction)

    End Sub

    Sub CompleteBuyOrder(buying As Transaction)

        Dim availableTransaction As Transaction = SellingOfferings.FirstOrDefault(Function(n) n.Resource.Name = buying.Resource.Name)
        While availableTransaction IsNot Nothing AndAlso buying.Resource.Shares > 0
            Dim seller As Company = availableTransaction.Owner
            Dim amountBuying As Integer = buying.Resource.Shares

            If amountBuying > availableTransaction.Resource.Shares Then
                amountBuying = availableTransaction.Resource.Shares
                SellingOfferings.Remove(availableTransaction)
            End If

            buying.Resource.Shares -= amountBuying
            buying.Owner.Assests.RemoveAsset(New Resource() With {.Name = Resource.CREDIT, .Shares = availableTransaction.PricePerUnit * amountBuying})
            buying.Owner.Assests.AddAsset(New Resource() With {.Name = availableTransaction.Resource.Name, .Shares = amountBuying})

            seller.Assests.AddAsset(New Resource() With {.Name = Resource.CREDIT, .Shares = availableTransaction.PricePerUnit * amountBuying})
            availableTransaction.Resource.Shares -= amountBuying
            availableTransaction = SellingOfferings.FirstOrDefault(Function(n) n.Resource.Name = buying.Resource.Name)

        End While



    End Sub

End Class


Public Class Transaction
    Public Id As New Guid()
    Public Owner As Company
    Public PricePerUnit As Double
    Public Resource As Resource
End Class

