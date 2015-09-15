Imports EventBlocker

Public Class Engine
    Public Companies As New List(Of IMarketForce)
    Private market As IMarket

    Public Sub New(thisMarket As IMarket)
        If thisMarket Is Nothing Then Throw New ArgumentNullException("Market cannot be Null.")

        market = thisMarket
    End Sub

    Public Sub ExecuteComputerActions()

        For Each currentCompany As IMarketForce In Companies
            currentCompany.Produce()
            currentCompany.PerformAction(market)
        Next
    End Sub


    Sub Initialize()
        'Dim nCompany As New Company()
        'nCompany.Name = "The Reserve"
        'nCompany.ProfitPerUnit = 0
        'nCompany.Shares = 1000

        'nCompany.AddResource(New Resource() With {.Name = Resource.RESOURCE_LUMBER, .Shares = 300})
        'nCompany.gamingStrategy.Add(New StockBuyStrategy(90, 500, Resource.RESOURCE_LUMBER))
        'nCompany.gamingStrategy.Add(New StockSellingBasicStrategy(110, 500, Resource.RESOURCE_LUMBER))



        'Companies.Add(nCompany)


    End Sub




End Class
