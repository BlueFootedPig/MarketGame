<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HomeScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HomeScreen))
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.ResourceSelectionBarLinkContainerItem = New DevExpress.XtraBars.BarLinkContainerItem()
        Me.CommonBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.UncommonBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.RareBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BuyBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.SellBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.ProduceLumberBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.StandardPopulation = New DevExpress.XtraBars.BarButtonItem()
        Me.CompanysBarButtonItem = New DevExpress.XtraBars.BarButtonItem()
        Me.PoorBarStaticItem = New DevExpress.XtraBars.BarStaticItem()
        Me.MiddleClassBarStaticItem = New DevExpress.XtraBars.BarStaticItem()
        Me.RichBarStaticItem = New DevExpress.XtraBars.BarStaticItem()
        Me.SaveButton = New DevExpress.XtraBars.BarButtonItem()
        Me.ControlRibbonPage = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.ProductionRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.MarketRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.ProduceRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.EngineRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.WorldStatusRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.SavingRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.ResourceDataGrid = New DevExpress.XtraGrid.GridControl()
        Me.ResourceGridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MarketGridControl = New DevExpress.XtraGrid.GridControl()
        Me.MarketGridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourceDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourceGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MarketGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarketGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.ResourceSelectionBarLinkContainerItem, Me.CommonBarButtonItem, Me.UncommonBarButtonItem, Me.RareBarButtonItem, Me.BuyBarButtonItem, Me.SellBarButtonItem, Me.BarButtonItem1, Me.BarButtonItem2, Me.ProduceLumberBarButtonItem, Me.StandardPopulation, Me.CompanysBarButtonItem, Me.PoorBarStaticItem, Me.MiddleClassBarStaticItem, Me.RichBarStaticItem, Me.SaveButton})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.MaxItemId = 17
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.ControlRibbonPage})
        Me.RibbonControl1.Size = New System.Drawing.Size(889, 142)
        '
        'ResourceSelectionBarLinkContainerItem
        '
        Me.ResourceSelectionBarLinkContainerItem.Caption = "Common"
        Me.ResourceSelectionBarLinkContainerItem.Id = 2
        Me.ResourceSelectionBarLinkContainerItem.LargeGlyph = CType(resources.GetObject("ResourceSelectionBarLinkContainerItem.LargeGlyph"), System.Drawing.Image)
        Me.ResourceSelectionBarLinkContainerItem.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.CommonBarButtonItem), New DevExpress.XtraBars.LinkPersistInfo(Me.UncommonBarButtonItem), New DevExpress.XtraBars.LinkPersistInfo(Me.RareBarButtonItem), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem2)})
        Me.ResourceSelectionBarLinkContainerItem.Name = "ResourceSelectionBarLinkContainerItem"
        '
        'CommonBarButtonItem
        '
        Me.CommonBarButtonItem.Caption = "Common"
        Me.CommonBarButtonItem.Glyph = CType(resources.GetObject("CommonBarButtonItem.Glyph"), System.Drawing.Image)
        Me.CommonBarButtonItem.Id = 3
        Me.CommonBarButtonItem.LargeGlyph = CType(resources.GetObject("CommonBarButtonItem.LargeGlyph"), System.Drawing.Image)
        Me.CommonBarButtonItem.Name = "CommonBarButtonItem"
        '
        'UncommonBarButtonItem
        '
        Me.UncommonBarButtonItem.Caption = "Uncommon"
        Me.UncommonBarButtonItem.Glyph = CType(resources.GetObject("UncommonBarButtonItem.Glyph"), System.Drawing.Image)
        Me.UncommonBarButtonItem.Id = 4
        Me.UncommonBarButtonItem.Name = "UncommonBarButtonItem"
        '
        'RareBarButtonItem
        '
        Me.RareBarButtonItem.Caption = "Rare"
        Me.RareBarButtonItem.Glyph = CType(resources.GetObject("RareBarButtonItem.Glyph"), System.Drawing.Image)
        Me.RareBarButtonItem.Id = 5
        Me.RareBarButtonItem.Name = "RareBarButtonItem"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Uber Rare"
        Me.BarButtonItem2.Id = 9
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BuyBarButtonItem
        '
        Me.BuyBarButtonItem.Caption = "Buy"
        Me.BuyBarButtonItem.Glyph = CType(resources.GetObject("BuyBarButtonItem.Glyph"), System.Drawing.Image)
        Me.BuyBarButtonItem.Id = 6
        Me.BuyBarButtonItem.LargeGlyph = CType(resources.GetObject("BuyBarButtonItem.LargeGlyph"), System.Drawing.Image)
        Me.BuyBarButtonItem.Name = "BuyBarButtonItem"
        '
        'SellBarButtonItem
        '
        Me.SellBarButtonItem.Caption = "Sell"
        Me.SellBarButtonItem.Id = 7
        Me.SellBarButtonItem.LargeGlyph = CType(resources.GetObject("SellBarButtonItem.LargeGlyph"), System.Drawing.Image)
        Me.SellBarButtonItem.Name = "SellBarButtonItem"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 8
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'ProduceLumberBarButtonItem
        '
        Me.ProduceLumberBarButtonItem.Caption = "Lumber"
        Me.ProduceLumberBarButtonItem.Id = 10
        Me.ProduceLumberBarButtonItem.Name = "ProduceLumberBarButtonItem"
        '
        'StandardPopulation
        '
        Me.StandardPopulation.Caption = "Population"
        Me.StandardPopulation.Id = 11
        Me.StandardPopulation.Name = "StandardPopulation"
        '
        'CompanysBarButtonItem
        '
        Me.CompanysBarButtonItem.Caption = "Company"
        Me.CompanysBarButtonItem.Id = 12
        Me.CompanysBarButtonItem.Name = "CompanysBarButtonItem"
        '
        'PoorBarStaticItem
        '
        Me.PoorBarStaticItem.Caption = "Poor"
        Me.PoorBarStaticItem.Id = 13
        Me.PoorBarStaticItem.Name = "PoorBarStaticItem"
        Me.PoorBarStaticItem.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'MiddleClassBarStaticItem
        '
        Me.MiddleClassBarStaticItem.Caption = "Middle"
        Me.MiddleClassBarStaticItem.Id = 14
        Me.MiddleClassBarStaticItem.Name = "MiddleClassBarStaticItem"
        Me.MiddleClassBarStaticItem.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'RichBarStaticItem
        '
        Me.RichBarStaticItem.Caption = "Rich"
        Me.RichBarStaticItem.Id = 15
        Me.RichBarStaticItem.Name = "RichBarStaticItem"
        Me.RichBarStaticItem.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'SaveButton
        '
        Me.SaveButton.Caption = "Save"
        Me.SaveButton.Id = 16
        Me.SaveButton.Name = "SaveButton"
        '
        'ControlRibbonPage
        '
        Me.ControlRibbonPage.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.ProductionRibbonPageGroup, Me.MarketRibbonPageGroup, Me.ProduceRibbonPageGroup, Me.EngineRibbonPageGroup, Me.WorldStatusRibbonPageGroup, Me.SavingRibbonPageGroup})
        Me.ControlRibbonPage.Name = "ControlRibbonPage"
        Me.ControlRibbonPage.Text = "Control"
        '
        'ProductionRibbonPageGroup
        '
        Me.ProductionRibbonPageGroup.AllowTextClipping = False
        Me.ProductionRibbonPageGroup.ItemLinks.Add(Me.ResourceSelectionBarLinkContainerItem)
        Me.ProductionRibbonPageGroup.Name = "ProductionRibbonPageGroup"
        Me.ProductionRibbonPageGroup.Text = "Production"
        '
        'MarketRibbonPageGroup
        '
        Me.MarketRibbonPageGroup.AllowTextClipping = False
        Me.MarketRibbonPageGroup.ItemLinks.Add(Me.BuyBarButtonItem)
        Me.MarketRibbonPageGroup.ItemLinks.Add(Me.SellBarButtonItem)
        Me.MarketRibbonPageGroup.Name = "MarketRibbonPageGroup"
        Me.MarketRibbonPageGroup.Text = "Market"
        '
        'ProduceRibbonPageGroup
        '
        Me.ProduceRibbonPageGroup.ItemLinks.Add(Me.ProduceLumberBarButtonItem)
        Me.ProduceRibbonPageGroup.Name = "ProduceRibbonPageGroup"
        Me.ProduceRibbonPageGroup.Text = "Produce"
        '
        'EngineRibbonPageGroup
        '
        Me.EngineRibbonPageGroup.ItemLinks.Add(Me.StandardPopulation)
        Me.EngineRibbonPageGroup.ItemLinks.Add(Me.CompanysBarButtonItem)
        Me.EngineRibbonPageGroup.Name = "EngineRibbonPageGroup"
        Me.EngineRibbonPageGroup.Text = "Engine"
        '
        'WorldStatusRibbonPageGroup
        '
        Me.WorldStatusRibbonPageGroup.AllowTextClipping = False
        Me.WorldStatusRibbonPageGroup.ItemLinks.Add(Me.PoorBarStaticItem)
        Me.WorldStatusRibbonPageGroup.ItemLinks.Add(Me.MiddleClassBarStaticItem)
        Me.WorldStatusRibbonPageGroup.ItemLinks.Add(Me.RichBarStaticItem)
        Me.WorldStatusRibbonPageGroup.Name = "WorldStatusRibbonPageGroup"
        Me.WorldStatusRibbonPageGroup.Text = "World Status"
        '
        'SavingRibbonPageGroup
        '
        Me.SavingRibbonPageGroup.AllowTextClipping = False
        Me.SavingRibbonPageGroup.ItemLinks.Add(Me.SaveButton)
        Me.SavingRibbonPageGroup.Name = "SavingRibbonPageGroup"
        Me.SavingRibbonPageGroup.Text = "Save / Load"
        '
        'ResourceDataGrid
        '
        Me.ResourceDataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResourceDataGrid.Location = New System.Drawing.Point(0, 0)
        Me.ResourceDataGrid.MainView = Me.ResourceGridView
        Me.ResourceDataGrid.MenuManager = Me.RibbonControl1
        Me.ResourceDataGrid.Name = "ResourceDataGrid"
        Me.ResourceDataGrid.Size = New System.Drawing.Size(889, 296)
        Me.ResourceDataGrid.TabIndex = 4
        Me.ResourceDataGrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ResourceGridView})
        '
        'ResourceGridView
        '
        Me.ResourceGridView.GridControl = Me.ResourceDataGrid
        Me.ResourceGridView.Name = "ResourceGridView"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 142)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ResourceDataGrid)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MarketGridControl)
        Me.SplitContainer1.Size = New System.Drawing.Size(889, 547)
        Me.SplitContainer1.SplitterDistance = 296
        Me.SplitContainer1.TabIndex = 5
        '
        'MarketGridControl
        '
        Me.MarketGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MarketGridControl.Location = New System.Drawing.Point(0, 0)
        Me.MarketGridControl.MainView = Me.MarketGridView
        Me.MarketGridControl.MenuManager = Me.RibbonControl1
        Me.MarketGridControl.Name = "MarketGridControl"
        Me.MarketGridControl.Size = New System.Drawing.Size(889, 247)
        Me.MarketGridControl.TabIndex = 5
        Me.MarketGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.MarketGridView})
        '
        'MarketGridView
        '
        Me.MarketGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus
        Me.MarketGridView.GridControl = Me.MarketGridControl
        Me.MarketGridView.Name = "MarketGridView"
        Me.MarketGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.MarketGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.MarketGridView.OptionsBehavior.Editable = False
        Me.MarketGridView.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.[Nothing]
        Me.MarketGridView.OptionsSelection.EnableAppearanceFocusedCell = False
        '
        'HomeScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(889, 689)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Name = "HomeScreen"
        Me.Text = "b"
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourceDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourceGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MarketGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarketGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents ControlRibbonPage As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents ProductionRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents ResourceSelectionBarLinkContainerItem As DevExpress.XtraBars.BarLinkContainerItem
    Friend WithEvents CommonBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents UncommonBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RareBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ResourceDataGrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents ResourceGridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MarketGridControl As DevExpress.XtraGrid.GridControl
    Friend WithEvents MarketGridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BuyBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SellBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MarketRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ProduceLumberBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ProduceRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents StandardPopulation As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CompanysBarButtonItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents EngineRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents PoorBarStaticItem As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents MiddleClassBarStaticItem As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RichBarStaticItem As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents WorldStatusRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents SaveButton As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SavingRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup

End Class
