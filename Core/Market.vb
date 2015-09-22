Public Class Market
    Implements IMarket

    Public Property SellingOfferings As New List(Of Transaction) Implements IMarket.SellingOfferings
    Public Property BuyingOfferings As New List(Of Transaction) Implements IMarket.BuyingOfferings

    Private lockingObject As New Object
    Private knownResources As IList(Of CraftResource)


    'Posts an bid up for sale
    Public Sub Buy(pricePerUnit As Integer, item As CraftResource, theCompany As Company) Implements IMarket.Buy
        If item Is Nothing Then Throw New ArgumentNullException("A Resource must be specified.")
        If item.Shares <= 0 OrElse String.IsNullOrEmpty(item.Name) Then Throw New ArgumentException("Item did not have a resource name or number of shares.")

        If theCompany Is Nothing Then Throw New ArgumentNullException("A company must be specified.")


        Dim nTransaction As New Transaction()
        nTransaction.Owner = theCompany
        nTransaction.Resource = item
        nTransaction.PricePerUnit = pricePerUnit
        SyncLock (lockingObject)


            CompleteBuyOrder(nTransaction)

            If nTransaction.Resource.Shares > 0 Then
                BuyingOfferings.Add(nTransaction)
            End If
        End SyncLock

    End Sub

    'Posts an item for sale
    Public Sub Sell(pricePerUnit As Integer, item As CraftResource, theCompany As Company) Implements IMarket.Sell
        If item Is Nothing Then Throw New ArgumentNullException("A Resource must be specified.")
        If item.Shares <= 0 OrElse String.IsNullOrEmpty(item.Name) Then Throw New ArgumentException("Item did not have a resource name or number of shares.")

        If theCompany Is Nothing Then Throw New ArgumentNullException("A company must be specified.")

        theCompany.RemoveResource(item)

        SyncLock (lockingObject)


            Dim nTransaction As New Transaction()
            nTransaction.Owner = theCompany
            nTransaction.Resource = item
            nTransaction.PricePerUnit = pricePerUnit

            CompleteSellOrder(nTransaction)

            If nTransaction.Resource.Shares > 0 Then
                SellingOfferings.Add(nTransaction)
            End If
        End SyncLock
    End Sub

    Private Sub CompleteSellOrder(selling As Transaction)

        Dim availableTransaction As Transaction = BuyingOfferings.FirstOrDefault(Function(n) n.Resource.Name = selling.Resource.Name)
        While availableTransaction IsNot Nothing AndAlso selling.Resource.Shares > 0
            TransferPropertyBetweenCompanies(availableTransaction, selling)
        End While

    End Sub

    Private Sub TransferPropertyBetweenCompanies(ByRef buyingTransaction As Transaction, ByRef sellingTransaction As Transaction)
        Dim buyer As Company = buyingTransaction.Owner
        Dim amountBuying As Integer = sellingTransaction.Resource.Shares

        If amountBuying > buyingTransaction.Resource.Shares Then
            amountBuying = buyingTransaction.Resource.Shares
            BuyingOfferings.Remove(buyingTransaction)
        End If

        sellingTransaction.Resource.Shares -= amountBuying
        sellingTransaction.Owner.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = buyingTransaction.PricePerUnit * amountBuying})
        '  sellingTransaction.Owner.RemoveResource(New Resource() With {.Name = sellingTransaction.Resource.Name, .Shares = amountBuying})

        buyer.RemoveResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = buyingTransaction.PricePerUnit * amountBuying})
        buyer.AddResource(New CraftResource() With {.Name = sellingTransaction.Resource.Name, .Shares = amountBuying})

        buyingTransaction.Resource.Shares -= amountBuying
        Dim SellerResourceName As String = sellingTransaction.Resource.Name
        buyingTransaction = BuyingOfferings.FirstOrDefault(Function(n) n.Resource.Name = SellerResourceName)

    End Sub


    Private Sub CompleteBuyOrder(buying As Transaction)

        Dim availableTransaction As Transaction = SellingOfferings.FirstOrDefault(Function(n) n.Resource.Name = buying.Resource.Name)
        While availableTransaction IsNot Nothing AndAlso buying.Resource.Shares > 0
            Dim seller As Company = availableTransaction.Owner
            Dim amountBuying As Integer = buying.Resource.Shares

            If amountBuying > availableTransaction.Resource.Shares Then
                amountBuying = availableTransaction.Resource.Shares
                SellingOfferings.Remove(availableTransaction)
            End If

            buying.Resource.Shares -= amountBuying
            buying.Owner.RemoveResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = availableTransaction.PricePerUnit * amountBuying})
            buying.Owner.AddResource(New CraftResource() With {.Name = availableTransaction.Resource.Name, .Shares = amountBuying})

            seller.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = availableTransaction.PricePerUnit * amountBuying})
            seller.RemoveResource(New CraftResource() With {.Name = availableTransaction.Resource.Name, .Shares = amountBuying})

            availableTransaction.Resource.Shares -= amountBuying
            availableTransaction = SellingOfferings.FirstOrDefault(Function(n) n.Resource.Name = buying.Resource.Name)

        End While



    End Sub

End Class


Public Class Transaction
    Public Id As New Guid()
    Public Owner As Company
    Public PricePerUnit As Double
    Public Resource As CraftResource
End Class

Public Interface IMarket
    Property SellingOfferings As List(Of Transaction)
    Property BuyingOfferings As List(Of Transaction)


    Sub Buy(pricePerUnit As Integer, item As CraftResource, theCompany As Company)
    Sub Sell(pricePerUnit As Integer, item As CraftResource, theCompany As Company)

End Interface

