Public Class Player
    Inherits Company
    Dim skillCommon As Double
    Dim skillUncommon As Double
    Dim skillRare As Double
    Public currentSkill As SkillChoice

    Public Sub New(manager As IAssetManager)
        MyBase.New(manager)
        skillCommon = 200
        skillUncommon = 100
        skillRare = 1000

    End Sub

    Public Overrides Sub PerformAction(market As IMarket)
        Dim producedResource As CraftResource = Nothing
        Select Case currentSkill
            Case SkillChoice.Common
                AddResource(New CraftResource() With {.Name = CraftResource.RESOURCE_LUMBER, .Shares = 1})

            Case SkillChoice.Uncommon
                AddResource(New CraftResource() With {.Name = CraftResource.RESOURCE_IRON, .Shares = 1})
            Case SkillChoice.Rare
                AddResource(New CraftResource() With {.Name = CraftResource.RESOURCE_GOLD, .Shares = 1})
        End Select


    End Sub



End Class

Public Enum SkillChoice
    Common
    Uncommon
    Rare
End Enum
