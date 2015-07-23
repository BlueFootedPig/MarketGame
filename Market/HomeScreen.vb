Imports Core

Public Class HomeScreen
    Dim gameEngine As New Engine(10)
    Dim user As New Player()
    Dim clock As New System.Timers.Timer(1000)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        ' Add any initialization after the InitializeComponent() call.

        user.currentSkill = SkillChoice.Common

        'Setup Companies
        If Not gameEngine.Load() Then gameEngine.Initialize()

        gameEngine.Companies.Add(user)

        AddHandler clock.Elapsed, AddressOf UpdateEngine
        clock.Start()

        Dim resourceDatatable As New DataTable()
        resourceDatatable.Columns.Add("Name")
        resourceDatatable.Columns.Add("#", Type.GetType("System.Double"))

        For Each item As Resource In user.Assests.GetAllAssets
            Dim nRow As DataRow = resourceDatatable.NewRow
            nRow("Name") = item.Name
            nRow("#") = Math.Floor(item.Shares)
            resourceDatatable.Rows.Add(nRow)
        Next

        ResourceDataGrid.DataSource = resourceDatatable

        Dim marketDatatable As New DataTable
        marketDatatable.Columns.Add("Seller")
        marketDatatable.Columns.Add("#", Type.GetType("System.Double"))
        marketDatatable.Columns.Add("Will Buy")
        marketDatatable.Columns.Add("Per Unit", Type.GetType("System.Double"))
        marketDatatable.Columns.Add("Resource")
        'nRow("Per Unit") = item.PricePerUnit
        'nRow("Resource") = item.Resource.Name

        For Each item As Transaction In gameEngine.GetMarketResources()
            Dim nRow As DataRow = resourceDatatable.NewRow
            nRow("Seller") = item.Resource.Name
            nRow("#") = Math.Floor(item.Resource.Shares)
            nRow("Per Unit") = item.PricePerUnit
            nRow("Resource") = item.Resource.Name
            resourceDatatable.Rows.Add(nRow)

            'nRow("Name") = item.Seller     nRow("Name") = item.Seller
            'nRow("Will Buy") = item.AskingResource
            'nRow("#") = item.Resource.Shares
            'nRow("Per Unit") = item.PricePerUnit
            'nRow("Resource") = item.Resource.Name
        Next


        MarketGridControl.DataSource = marketDatatable


    End Sub

    Dim updating As Boolean = False
    Private Sub UpdateEngine()
        If updating Then Exit Sub
        updating = True
        gameEngine.ExecuteComputerActions()

        Dim updateTable As DataTable = ResourceDataGrid.DataSource

        ' updateTable.Rows.Clear()
        For Each item As Resource In user.Assests.GetAllAssets

            Dim tmpView As New DataView(updateTable, "Name = '" & item.Name & "'", String.Empty, DataViewRowState.CurrentRows)
            If tmpView.Count = 0 Then
                Dim nRow As DataRow = Nothing

                nRow = updateTable.NewRow
                nRow("Name") = item.Name
                nRow("#") = Math.Floor(item.Shares)

                updateTable.Rows.Add(nRow)
            Else
                Dim nRow As DataRowView = tmpView(0)
                nRow("#") = item.Shares
            End If

        Next

        updateTable = MarketGridControl.DataSource

        For Each item As Transaction In gameEngine.GetMarketResources()
            Dim tmpView As New DataView(updateTable, "Resource = '" & item.Resource.Name & "' and Seller = '" & item.Seller & "'", String.Empty, DataViewRowState.CurrentRows)
            If tmpView.Count = 0 Then
                Dim nRow As DataRow = Nothing

                nRow = updateTable.NewRow
                nRow("Seller") = item.Seller

                nRow("#") = item.Resource.Shares
                nRow("Per Unit") = item.PricePerUnit
                nRow("Resource") = item.Resource.Name

                updateTable.Rows.Add(nRow)
            Else
                Dim nRow As DataRowView = tmpView(0)
                nRow("Seller") = item.Seller

                nRow("#") = item.Resource.Shares
                nRow("Per Unit") = item.PricePerUnit
                nRow("Resource") = item.Resource.Name

            End If
        Next
        updating = False
    End Sub



    Private Sub RareBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles RareBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Rare
        ResourceSelectionBarLinkContainerItem.Caption = Resource.RESOURCE_RARE
        ResourceSelectionBarLinkContainerItem.LargeGlyph = RareBarButtonItem.Glyph
        RareBarButtonItem.Down = True
    End Sub

    Private Sub UncommonBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles UncommonBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Uncommon

        ResourceSelectionBarLinkContainerItem.Caption = Resource.RESOURCE_UNCOMMON
        ResourceSelectionBarLinkContainerItem.LargeGlyph = UncommonBarButtonItem.Glyph
        UncommonBarButtonItem.Down = True
    End Sub

    Private Sub CommonBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles CommonBarButtonItem.ItemClick
        user.currentSkill = SkillChoice.Common
        ResourceSelectionBarLinkContainerItem.Caption = Resource.RESOURCE_COMMON
        ResourceSelectionBarLinkContainerItem.LargeGlyph = CommonBarButtonItem.Glyph
        CommonBarButtonItem.Down = True

    End Sub

    Private Sub BuyBarButtonItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BuyBarButtonItem.ItemClick
        If MarketGridView.SelectedRowsCount = 0 Then Exit Sub

        Dim rowIndex As Integer = MarketGridView.GetSelectedRows()(0)
        Dim transRow As DataRowView = MarketGridView.GetRow(rowIndex)


        gameEngine.RequestPurchase(transRow("Seller"), transRow("Resource"), user)

        'nRow("Seller") = item.Seller
        'nRow("Will Buy") = item.AskingResource
        'nRow("#") = item.Resource.Shares
        'nRow("Per Unit") = item.PricePerUnit
        'nRow("Resource") = item.Resource.Name

    End Sub


End Class
