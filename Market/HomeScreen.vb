Imports Core

Public Class HomeScreen
    ' Dim gameEngine As New Engine(New Core.Market)
    Dim market As IMarket = New Core.Market
    Dim user As New Player()
    Dim clock As New System.Timers.Timer(1000)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        ' Add any initialization after the InitializeComponent() call.

        user.currentSkill = SkillChoice.Common

        'Setup Companies


        '   gameEngine.Companies.Add(user)

        AddHandler clock.Elapsed, AddressOf UpdateInterface
        clock.Start()

        Dim resourceDatatable As New DataTable()
        resourceDatatable.Columns.Add("Name")
        resourceDatatable.Columns.Add("#", Type.GetType("System.Double"))


        ResourceDataGrid.DataSource = resourceDatatable

        Dim marketDatatable As New DataTable
        marketDatatable.Columns.Add("Seller")
        marketDatatable.Columns.Add("#", Type.GetType("System.Double"))
        marketDatatable.Columns.Add("Will Buy")
        marketDatatable.Columns.Add("Per Unit", Type.GetType("System.Double"))
        marketDatatable.Columns.Add("Resource")


        MarketGridControl.DataSource = marketDatatable


    End Sub

    Dim updating As Boolean = False



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

    End Sub


    Private Sub ProduceLumberBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles ProduceLumberBarButtonItem.ItemClick
        user.AddResource(New Resource() With {.Name = "Lumber", .Shares = 1})
    End Sub

    Private Sub SellBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles SellBarButtonItem.ItemClick
        Dim indexes As Integer() = ResourceGridView.GetSelectedRows()
        If indexes.Count = 0 Then Exit Sub

        Dim firstIndex As Integer = indexes.First()

        Dim row As DataRowView = ResourceGridView.GetRow(firstIndex)

        market.Sell(10, New Resource() With {.Name = row("Name"), .Shares = 1}, user)

    End Sub
End Class
