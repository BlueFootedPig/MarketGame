Imports Core

Public Class HomeScreen
    Dim gameEngine As New Engine(New Core.Market)
    Dim user As New Player()
    Dim clock As New System.Timers.Timer(1000)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
        ' Add any initialization after the InitializeComponent() call.

        user.currentSkill = SkillChoice.Common

        'Setup Companies
        '  If Not gameEngine.Load() Then gameEngine.Initialize()

        gameEngine.Companies.Add(user)

        AddHandler clock.Elapsed, AddressOf UpdateEngine
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


        ' gameEngine.RequestPurchase(transRow("Seller"), transRow("Resource"), user)

        'nRow("Seller") = item.Seller
        'nRow("Will Buy") = item.AskingResource
        'nRow("#") = item.Resource.Shares
        'nRow("Per Unit") = item.PricePerUnit
        'nRow("Resource") = item.Resource.Name

    End Sub

    Private Sub UpdateEngine(sender As Object, e As Timers.ElapsedEventArgs)
        Throw New NotImplementedException
    End Sub


End Class
