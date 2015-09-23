Imports EventBlocker
Imports Core

Public Class HomeScreen

    Private market As IMarket = New Core.Market
    Private gameEngine As New Engine(market, New WorldPopulationEngine())
    Private user As New Player(New AssetManager())
    Private interfaceClock As System.Timers.Timer
    ' Private computerClock As System.Timers.Timer
    '  Private popluationClock As System.Timers.Timer
    Private Const STANDARD_TICK As Integer = 5000

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        ' Add any initialization after the InitializeComponent() call.

        user.currentSkill = SkillChoice.Common

        'computerClock = New Timers.Timer(STANDARD_TICK / 2)
        'AddHandler computerClock.Elapsed, AddressOf ExecuteCompany
        'computerClock.Start()

        'popluationClock = New Timers.Timer(STANDARD_TICK)
        'AddHandler popluationClock.Elapsed, AddressOf ExecutePopulation
        'popluationClock.Start()


        interfaceClock = New Timers.Timer(1000)
        AddHandler interfaceClock.Elapsed, AddressOf UpdateInterface
        interfaceClock.Start()

        Dim resourceDatatable As New DataTable()
        resourceDatatable.Columns.Add("Name")
        resourceDatatable.Columns.Add("#", Type.GetType("System.Double"))


        ResourceDataGrid.DataSource = resourceDatatable

        Dim marketDatatable As New DataTable
        marketDatatable.Columns.Add("Poster")
        marketDatatable.Columns.Add("#", Type.GetType("System.Double"))
        marketDatatable.Columns.Add("Type")
        marketDatatable.Columns.Add("Per Unit", Type.GetType("System.Double"))
        marketDatatable.Columns.Add("Resource")
        MarketGridControl.DataSource = marketDatatable

        UpdateTransactionGrid()

        'Setup companies
        Dim company As New Company(New AssetManager())
        company.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 1000000})
        company.gamingStrategy.Add(New StockBuyStrategy(100, 100, "Lumber"))
        Dim producedRes As New LuxuryResource() With {.Name = "Stool", .Shares = 1}

        company.gamingStrategy.Add(New StockSellingBasicStrategy(200, 0, producedRes))
        company.ProducedResource = New ResourceProduction() With {.ProducedResource = producedRes}
        company.ProducedResource.RequiredResources.Add(New CraftResource() With {.Name = "Lumber", .Shares = 2})
        gameEngine.Companies.Add(company)


        'setup population
        Dim person As New Person(0)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)

        person = New Person(1)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)

        person = New Person(2)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)
        gameEngine.society.Population.Add(person)


    End Sub

    Dim updatingObject As New Object

    Private Sub UpdateTransactionGrid()
        Dim token As IBlock = New FunctionBlock(AddressOf UpdateTransactionGrid)
        If Not EventRegistry.TryGetLock(Me, Nothing, token) Then Exit Sub

        Dim marketDatatable As DataTable = MarketGridControl.DataSource
        marketDatatable.Clear()
        For Each trans As Transaction In market.SellingOfferings.ToList()
            Dim nRow As DataRow = marketDatatable.NewRow
            nRow("Poster") = trans.Owner
            nRow("#") = trans.Resource.Shares
            nRow("Type") = "Buy"
            nRow("Per Unit") = trans.PricePerUnit
            nRow("Resource") = trans.Resource.Name

            marketDatatable.Rows.Add(nRow)
        Next

        For Each trans As Transaction In market.BuyingOfferings.ToList()
            Dim nRow As DataRow = marketDatatable.NewRow
            nRow("Poster") = trans.Owner
            nRow("#") = trans.Resource.Shares
            nRow("Type") = "Sell"
            nRow("Per Unit") = trans.PricePerUnit
            nRow("Resource") = trans.Resource.Name

            marketDatatable.Rows.Add(nRow)
        Next

        token.Dispose()
    End Sub


    Private Sub RareBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles RareBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Rare
        ResourceSelectionBarLinkContainerItem.Caption = CraftResource.RESOURCE_GOLD
        ResourceSelectionBarLinkContainerItem.LargeGlyph = RareBarButtonItem.Glyph
        RareBarButtonItem.Down = True
    End Sub

    Private Sub UncommonBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles UncommonBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Uncommon

        ResourceSelectionBarLinkContainerItem.Caption = CraftResource.RESOURCE_IRON
        ResourceSelectionBarLinkContainerItem.LargeGlyph = UncommonBarButtonItem.Glyph
        UncommonBarButtonItem.Down = True
    End Sub

    Private Sub CommonBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles CommonBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Common
        ResourceSelectionBarLinkContainerItem.Caption = CraftResource.RESOURCE_LUMBER
        ResourceSelectionBarLinkContainerItem.LargeGlyph = CommonBarButtonItem.Glyph
        CommonBarButtonItem.Down = True

    End Sub

    Private Sub BuyBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BuyBarButtonItem.ItemClick
        If MarketGridView.SelectedRowsCount = 0 Then Exit Sub

        Dim rowIndex As Integer = MarketGridView.GetSelectedRows()(0)
        Dim transRow As DataRowView = MarketGridView.GetRow(rowIndex)

    End Sub

    Private Sub UpdateInterface(sender As Object, e As Timers.ElapsedEventArgs)

        Dim token As IBlock = New ClassBlock(updatingObject)
        If Not EventRegistry.TryGetLock(Me, updatingObject, token) Then Exit Sub


        Dim allAssests As IList(Of IResource) = user.GetAllAssets()

        Dim checkedResources As New List(Of IResource)
        Dim rowsToRemove As New List(Of DataRow)

        Dim resourceDataTable As DataTable = ResourceDataGrid.DataSource

        For Each row As DataRow In resourceDataTable.Rows
            Dim resourceName As String = row("Name")

            Dim currentResource As CraftResource = allAssests.FirstOrDefault(Function(n) n.Name = resourceName)

            If currentResource Is Nothing Then
                rowsToRemove.Add(row)
            Else
                row("#") = currentResource.Shares
            End If
            checkedResources.Add(currentResource)
        Next

        For Each item As CraftResource In allAssests
            If checkedResources.Any(Function(n) n.Name = item.Name) Then Continue For

            Dim nRow As DataRow = resourceDataTable.NewRow
            nRow("Name") = item.Name
            nRow("#") = item.Shares
            resourceDataTable.Rows.Add(nRow)
        Next

        For Each row As DataRow In rowsToRemove
            resourceDataTable.Rows.Remove(row)
        Next

        UpdateTransactionGrid()

        token.Dispose()


    End Sub

    Private Sub ExecuteCompany(sender As Object, e As EventArgs) Handles CompanysBarButtonItem.ItemClick

        gameEngine.ExecuteComputerActions()

    End Sub

    Private rand As New Random(0)
    Private Sub ExecutePopulation(sender As Object, e As EventArgs) Handles StandardPopulation.ItemClick

        gameEngine.society.RunPopulationCampaign(New WorldPopluationIncreaseCampaign(rand))
        gameEngine.society.RunPopulationCampaign(New WorldPopluationDecreaseCampaign(rand))

        gameEngine.society.ProduceMoney()

        gameEngine.society.RunSpendingCampaign(New StandardSpendingCampaign(), market)

    End Sub

    Private Sub ProduceLumberBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles ProduceLumberBarButtonItem.ItemClick
        user.AddResource(New CraftResource() With {.Name = "Lumber", .Shares = 1})
    End Sub

    Private Sub SellBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles SellBarButtonItem.ItemClick
        Dim indexes As Integer() = ResourceGridView.GetSelectedRows()
        If indexes.Count = 0 Then Exit Sub

        Dim firstIndex As Integer = indexes.First()

        Dim row As DataRowView = ResourceGridView.GetRow(firstIndex)
        Dim resourceToDealWith As New CraftResource() With {.Name = row("Name"), .Shares = 1}
        market.Sell(10, resourceToDealWith, user)

    End Sub

    
End Class
