Imports EventBlocker

Public Class Engine
    Public Companies As New List(Of IMarketForce)
    Public society As IWorldPopulationEngine
    Private market As IMarket

    Public Sub New(thisMarket As IMarket, populationEngine As IWorldPopulationEngine)
        If thisMarket Is Nothing Then Throw New ArgumentNullException("thismarket", "thisMarket cannot be Null.")
        If populationEngine Is Nothing Then Throw New ArgumentNullException("populationEngine", "'populationEngine cannot be Null.")

        market = thisMarket
        society = populationEngine

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
