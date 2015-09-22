Public Class StandardSpendingCampaign
    Implements ISpendingCampaign

    Public Sub RunCamapign(populationWallet As IDictionary(Of String, Double), market As IMarket) Implements ISpendingCampaign.RunCamapign
        If populationWallet Is Nothing Then Throw New ArgumentNullException("populationWallet", "'populationWallet' cannot be Null.")
        If market Is Nothing Then Throw New ArgumentNullException("market", "'market' cannot be Null.")


        'tolist is to avoid a problem when changing the values of a dictionary while iterating over it
        For Each tag As String In populationWallet.Keys.ToList()
            Dim runningTotal As Double = populationWallet(tag)

            Dim luxuryItemsForSale As IEnumerable(Of Transaction) = market.SellingOfferings.Where(Function(n) n.Resource.GetType() = GetType(LuxuryResource))
            Dim orderedItemsByValue As IEnumerable(Of Transaction) = luxuryItemsForSale.OrderBy(Function(n) getPerceivedValue(tag, n))

            For Each buyingThisItem As Transaction In orderedItemsByValue.ToList()
                If runningTotal < buyingThisItem.PricePerUnit Then Exit For

                runningTotal = buyShares(runningTotal, buyingThisItem, market)

            Next

            populationWallet(tag) = runningTotal

        Next

 

    End Sub

    Private Function buyShares(runningTotal As Double, buyingThisItem As Transaction, market As IMarket) As Integer
        If canBuyAllShares(runningTotal, buyingThisItem) Then
            market.Buy(buyingThisItem.PricePerUnit, buyingThisItem.Resource, Company.VoidCompany)
            runningTotal -= buyingThisItem.PricePerUnit * buyingThisItem.Resource.Shares
        Else
            Dim numberOfSharesToBuy As Integer = runningTotal / buyingThisItem.PricePerUnit

            market.Buy(buyingThisItem.PricePerUnit, New CraftResource() With {.Name = buyingThisItem.Resource.Name, .Shares = numberOfSharesToBuy}, Company.VoidCompany)
            runningTotal -= buyingThisItem.PricePerUnit * numberOfSharesToBuy

        End If
        Return runningTotal
    End Function

    Private Function getPerceivedValue(tag As String, item As Transaction) As Double
        Dim baseValue As Double = item.PricePerUnit
        Dim totalTags As Double = LuxuryResource.TotalTags

        If item.Resource.GetType() = GetType(LuxuryResource) Then
            Dim luxResource As LuxuryResource = item.Resource
            Dim factor = 1 - (0.02 * (totalTags - luxResource.PrefferedCustomers.Count))
            If luxResource.PrefferedCustomers.Contains(tag) Then
                factor = factor / 1.5
            End If
            baseValue = baseValue * factor
        End If

        Return baseValue / item.Resource.Level
    End Function

    Private Function canBuyAllShares(wallet As Double, buyingThisItem As Transaction) As Boolean
        If wallet > buyingThisItem.PricePerUnit * buyingThisItem.Resource.Shares Then Return True
        Return False
    End Function


End Class
