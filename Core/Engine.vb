Public Class Engine
    Public Companies As New List(Of Company)
    Private market As IMarket

    Public Sub New(thisMarket As IMarket)
        If thisMarket Is Nothing Then Throw New ArgumentNullException("Market cannot be Null.")

        market = thisMarket
    End Sub

    Public Sub ExecuteComputerActions()
        For Each currentCompany As Company In companies
            currentCompany.PerformAction(market)
        Next

    End Sub

    Function Load() As Boolean
        Return False
        ' Throw New NotImplementedException
    End Function

    Sub Initialize()
        Dim nCompany As New Company()
        nCompany.Name = "The Reserve"
        nCompany.ProfitPerUnit = 0
        nCompany.Shares = 1000

        nCompany.AddResource(New Resource() With {.Name = Resource.RESOURCE_LUMBER, .Shares = 300})
        nCompany.gamingStrategy.Add(New StockBuyBackStrategy(90, 500, Resource.RESOURCE_LUMBER))
        nCompany.gamingStrategy.Add(New StockSellingBasicStrategy(110, 500, Resource.RESOURCE_LUMBER))



        Companies.Add(nCompany)

        'nCompany = New Company()
        'nCompany.Name = "Basic Commons"
        'nCompany.Shares = 10000
        'nCompany.RequiredResources.Add(New Resource() With {.Name = Resource.RESOURCE_COMMON, .Amount = 3})
        'nCompany.ProfitPerUnit = 0
        'Companies.Add(nCompany)

    End Sub

    Private Sub ReserveStrategy()

    End Sub

    Function GetMarketResources() As List(Of Transaction)
        Return market.sellingOfferings
    End Function

    Sub RequestPurchase(seller As String, resource As String, buyer As Company)
        ' If buyer.Resources.Any(Function(n) n.Name = )
        'Dim lowestPrice As Double = market.sellingOfferings.Where(Function(n) n.Seller = seller AndAlso n.Resource.Name = resource).Min(Function(n) n.PricePerUnit)
        'Dim findTransaction As Transaction = market.sellingOfferings.FirstOrDefault(Function(n) n.Seller = seller AndAlso n.Resource.Name = resource AndAlso n.PricePerUnit = lowestPrice)

        ''current exits function, will post item for buy
        'If findTransaction Is Nothing Then Exit Sub

        'findTransaction.Resource.Shares -= 1

        'buyer.Assests.AddAsset(New Resource() With {.Name = resource, .Shares = 1})

        'buyer.Assests.RemoveAsset(New Resource() With {.Name = "Credit", .Shares = findTransaction.PricePerUnit})

        'Dim sellerCompany As Company = Companies.First(Function(n) n.Name = findTransaction.Seller)

        'sellerCompany.Assests.AddAsset(New Resource() With {.Name = "Credit", .Shares = findTransaction.PricePerUnit})

    End Sub

End Class
