Imports EventBlocker
Imports Core

Public Class HomeScreen

    Dim market As IMarket = New Core.Market
    Dim gameEngine As New Engine(market)
    Dim user As New Player(New AssetManager())
    Dim clock As System.Timers.Timer
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        ' Add any initialization after the InitializeComponent() call.

        user.currentSkill = SkillChoice.Common

        'Setup Companies


        '   gameEngine.Companies.Add(user)
        clock = New Timers.Timer(1000)
        AddHandler clock.Elapsed, AddressOf UpdateInterface
        clock.Start()

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





        Dim company As New Company(New AssetManager())
        company.AddResource(New Resource() With {.Name = Resource.CREDIT, .Shares = 1000000})
        company.gamingStrategy.Add(New StockBuyStrategy(100, 100, "Lumber"))
        company.gamingStrategy.Add(New StockSellingBasicStrategy(200, 5, "Stool"))
        company.ProducedResource = New Resource() With {.Name = "Stool", .Shares = 1}
        company.RequiredResources.Add(New Resource() With {.Name = "Lumber", .Shares = 2})
        gameEngine.Companies.Add(company)

    End Sub

    Dim updatingObject As New Object

    Private Sub UpdateTransactionGrid()
        Dim marketDatatable As DataTable = MarketGridControl.DataSource
        marketDatatable.Clear()
        For Each trans As Transaction In market.SellingOfferings
            Dim nRow As DataRow = marketDatatable.NewRow
            nRow("Poster") = trans.Owner
            nRow("#") = trans.Resource.Shares
            nRow("Type") = "Buy"
            nRow("Per Unit") = trans.PricePerUnit
            nRow("Resource") = trans.Resource.Name

            marketDatatable.Rows.Add(nRow)
        Next

        For Each trans As Transaction In market.BuyingOfferings
            Dim nRow As DataRow = marketDatatable.NewRow
            nRow("Poster") = trans.Owner
            nRow("#") = trans.Resource.Shares
            nRow("Type") = "Sell"
            nRow("Per Unit") = trans.PricePerUnit
            nRow("Resource") = trans.Resource.Name

            marketDatatable.Rows.Add(nRow)
        Next
    End Sub


    Private Sub RareBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles RareBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Rare
        ResourceSelectionBarLinkContainerItem.Caption = Resource.RESOURCE_GOLD
        ResourceSelectionBarLinkContainerItem.LargeGlyph = RareBarButtonItem.Glyph
        RareBarButtonItem.Down = True
    End Sub

    Private Sub UncommonBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles UncommonBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Uncommon

        ResourceSelectionBarLinkContainerItem.Caption = Resource.RESOURCE_IRON
        ResourceSelectionBarLinkContainerItem.LargeGlyph = UncommonBarButtonItem.Glyph
        UncommonBarButtonItem.Down = True
    End Sub

    Private Sub CommonBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles CommonBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Common
        ResourceSelectionBarLinkContainerItem.Caption = Resource.RESOURCE_LUMBER
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


        Dim allAssests As IList(Of Resource) = user.GetAllAssets()

        Dim checkedResources As New List(Of Resource)
        Dim rowsToRemove As New List(Of DataRow)

        Dim resourceDataTable As DataTable = ResourceDataGrid.DataSource

        For Each row As DataRow In resourceDataTable.Rows
            Dim resourceName As String = row("Name")

            Dim currentResource As Resource = allAssests.FirstOrDefault(Function(n) n.Name = resourceName)

            If currentResource Is Nothing Then
                rowsToRemove.Add(row)
            Else
                row("#") = currentResource.Shares
            End If
            checkedResources.Add(currentResource)
        Next

        For Each item As Resource In allAssests
            If checkedResources.Any(Function(n) n.Name = item.Name) Then Continue For

            Dim nRow As DataRow = resourceDataTable.NewRow
            nRow("Name") = item.Name
            nRow("#") = item.Shares
            resourceDataTable.Rows.Add(nRow)
        Next

        For Each row As DataRow In rowsToRemove
            resourceDataTable.Rows.Remove(row)
        Next


        gameEngine.ExecuteComputerActions()

        UpdateTransactionGrid()

        token.Dispose()


    End Sub


    Private Sub ProduceLumberBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles ProduceLumberBarButtonItem.ItemClick
        user.AddResource(New Resource() With {.Name = "Lumber", .Shares = 1})
    End Sub

    Private Sub SellBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles SellBarButtonItem.ItemClick
        Dim indexes As Integer() = ResourceGridView.GetSelectedRows()
        If indexes.Count = 0 Then Exit Sub

        Dim firstIndex As Integer = indexes.First()

        Dim row As DataRowView = ResourceGridView.GetRow(firstIndex)
        Dim resourceToDealWith As New Resource() With {.Name = row("Name"), .Shares = 1}
        market.Sell(10, resourceToDealWith, user)

    End Sub
End Class
