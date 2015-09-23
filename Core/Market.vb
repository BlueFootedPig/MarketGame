Public Interface IMarket
    Property SellingOfferings As List(Of Transaction)
    Property BuyingOfferings As List(Of Transaction)


    Sub Buy(pricePerUnit As Integer, item As IResource, theCompany As Company)
    Sub Sell(pricePerUnit As Integer, item As IResource, theCompany As Company)

End Interface

Public Class Market
    Implements IMarket

    Public Property SellingOfferings As New List(Of Transaction) Implements IMarket.SellingOfferings
    Public Property BuyingOfferings As New List(Of Transaction) Implements IMarket.BuyingOfferings

    Private lockingObject As New Object
    Private knownResources As IList(Of CraftResource)


    'Posts an bid up for sale
    Public Sub Buy(pricePerUnit As Integer, item As IResource, theCompany As Company) Implements IMarket.Buy
        If item Is Nothing Then Throw New ArgumentNullException("A Resource must be specified.")
        If item.Shares <= 0 OrElse String.IsNullOrEmpty(item.Name) Then Throw New ArgumentException("Item did not have a resource name or number of shares.")

        If theCompany Is Nothing Then Throw New ArgumentNullException("A company must be specified.")


        Dim nTransaction As New Transaction()
        nTransaction.Owner = theCompany
        nTransaction.Resource = item.CopyResource()
        nTransaction.PricePerUnit = pricePerUnit
        SyncLock (lockingObject)


            CompleteBuyOrder(nTransaction)

            If nTransaction.Resource.Shares > 0 Then
                BuyingOfferings.Add(nTransaction)
            End If
            cleanupTransactions()
        End SyncLock

    End Sub

    Private Sub cleanupTransactions()
        removeEmptyOfferings(SellingOfferings)
        removeEmptyOfferings(BuyingOfferings)

    End Sub

    Private Sub removeEmptyOfferings(listToCheck As IList(Of Transaction))

        Dim listToRemove As IList(Of Transaction) = listToCheck.Where(Function(n) n.Resource.Shares <= 0).ToList()
        If listToRemove.Count = 0 Then Exit Sub

        For Each item As Transaction In listToRemove
            listToCheck.Remove(item)
        Next

    End Sub

    'Posts an item for sale
    Public Sub Sell(pricePerUnit As Integer, item As IResource, theCompany As Company) Implements IMarket.Sell
        If item Is Nothing Then Throw New ArgumentNullException("A Resource must be specified.")
        If item.Shares <= 0 OrElse String.IsNullOrEmpty(item.Name) Then Throw New ArgumentException("Item did not have a resource name or number of shares.")

        If theCompany Is Nothing Then Throw New ArgumentNullException("A company must be specified.")

        theCompany.RemoveResource(item)

        SyncLock (lockingObject)


            Dim nTransaction As New Transaction()
            nTransaction.Owner = theCompany
            nTransaction.Resource = item.CopyResource()
            nTransaction.PricePerUnit = pricePerUnit

            CompleteSellOrder(nTransaction)

            If nTransaction.Resource.Shares > 0 Then
                SellingOfferings.Add(nTransaction)
            End If
            cleanupTransactions()
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
            Dim itemBuying As IResource = availableTransaction.Resource.CopyResource()
            itemBuying.Shares = amountBuying

            buying.Owner.RemoveResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = availableTransaction.PricePerUnit * amountBuying})
            buying.Owner.AddResource(itemBuying)

            seller.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = availableTransaction.PricePerUnit * amountBuying})

            availableTransaction.Resource.Shares -= amountBuying

        End While



    End Sub

End Class





