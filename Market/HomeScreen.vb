Imports EventBlocker
Imports Core
Imports SaveToFile
Imports PersonPopulationCampaigns
Imports PopulationCampaigns
Imports Ninject


Public Class HomeScreen

    Private market As IMarket = New Core.Market
    ' Private gameEngine As New Engine(market, New WorldPopulationEngine())
    Private gameEngine As Engine
    Private user As New Player(New AssetManager())
    Private interfaceClock As System.Timers.Timer
    Private Const STANDARD_TICK As Integer = 5000

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        ' Add any initialization after the InitializeComponent() call.

        SetupObjectMap()
        gameEngine = kernal.Get(Of Engine)()
        market = gameEngine.Market

        user.currentSkill = SkillChoice.Common

        Dim systemSettings As New EngineSettings

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
        Dim company As New Company(New AssetManager(), New SaveCompanyToFile())
        company.Name = "Best Stools"
        company.Shares = 1000
        company.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 10000})
        company.gamingStrategy.Add(New StockBuyStrategy(100, 100, "Lumber"))
        Dim producedRes As IResource = New LuxuryResource() With {.Name = "Stool", .Shares = 1}
        company.gamingStrategy.Add(New StockSellingBasicStrategy(200, 0, producedRes))
        company.ProducedResource = New ResourceProduction(systemSettings) With {.ProducedResource = producedRes}
        company.ProducedResource.RequiredResources.Add(New CraftResource() With {.Name = "Lumber", .Shares = 2})
        gameEngine.Companies.Add(company)

        company = New Company(New AssetManager(), New SaveCompanyToFile())
        company.Name = "Lumber Jacks Lumber"
        company.Shares = 2000
        company.AddResource(New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = 1000})
        producedRes = New CraftResource() With {.Name = "Lumber", .Shares = 1}
        company.gamingStrategy.Add(New StockSellingBasicStrategy(50, 0, producedRes))
        company.ProducedResource = New ResourceProduction(systemSettings) With {.ProducedResource = producedRes}
        gameEngine.Companies.Add(company)

        'SetupPopulation()
        SetupBudgets()
        'setup population
        '20 poor


    End Sub

    Private Sub SetupPopulation()
        For counter As Integer = 0 To 20
            AddNewPerson(0)
        Next

        '10 middle class
        For counter As Integer = 0 To 10
            AddNewPerson(1)
        Next

        '5 rich
        For counter As Integer = 0 To 5
            AddNewPerson(2)
        Next
    End Sub

    Private Sub SetupBudgets()
        AddNewBudget("Poor", 5000)
        AddNewBudget("Middle", 300)
        AddNewBudget("Rich", 100)
    End Sub


    Dim updatingObject As New Object

    Dim listedResources As New Dictionary(Of String, Double)

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

        UpdateWorldStats()

        token.Dispose()


    End Sub

    Private Sub UpdateWorldStats()
        'RichBarStaticItem.Caption = "Rich: " & gameEngine.society.Population.Where(Function(n) n.Income = 2).Count
        'MiddleClassBarStaticItem.Caption = "MiddleClass: " & gameEngine.society.Population.Where(Function(n) n.Income = 1).Count
        'PoorBarStaticItem.Caption = "Poor: " & gameEngine.society.Population.Where(Function(n) n.Income = 0).Count
    End Sub

    Private Sub ExecuteCompany(sender As Object, e As EventArgs) Handles CompanysBarButtonItem.ItemClick

        gameEngine.ExecuteComputerActions()

    End Sub

    Private rand As New Random(0)
    Private Sub ExecutePopulation(sender As Object, e As EventArgs) Handles StandardPopulation.ItemClick

        gameEngine.society.RunPopulationCampaign(New WorldPopluationIncreaseCampaign(rand, 30))
        gameEngine.society.RunPopulationCampaign(New WorldPopluationDecreaseCampaign(rand, 20))

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

    Private Sub AddNewPerson(incomeLevel As Integer)
        Dim person As New Person(incomeLevel)
        gameEngine.society.Population.Add(person)
    End Sub

    Private Sub AddNewBudget(tag As String, basicBudget As Double)
        Dim budget As New Budget(tag, basicBudget)
        gameEngine.society.Population.Add(budget)
    End Sub


    Private Sub SaveButton_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveButton.ItemClick
        For Each force As IMarketForce In gameEngine.Companies
            force.Save()
        Next
    End Sub

    Private kernal As IKernel

    Private Sub SetupObjectMap()

        kernal = New StandardKernel()
        kernal.Bind(Of IMarket).To(Of Core.Market)()
        kernal.Bind(Of IWorldPopulationEngine).To(Of WorldPopulationEngine)()

    End Sub

End Class
