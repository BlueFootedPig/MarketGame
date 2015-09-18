Public Class AssetManager
    Implements IAssetManager

    Private assests As New List(Of Resource)

    Public Sub AddAsset(resource As Resource) Implements IAssetManager.AddAsset
        Dim foundResource As Resource = assests.FirstOrDefault(Function(n) n.Name = resource.Name)

        If foundResource Is Nothing Then
            assests.Add(resource)
        Else
            foundResource.Shares += resource.Shares
        End If

    End Sub


    Public Sub RemoveAsset(resource As Resource) Implements IAssetManager.RemoveAsset
        Dim foundResource As Resource = assests.FirstOrDefault(Function(n) n.Name = resource.Name)

        If foundResource Is Nothing Then
            Throw New InvalidOperationException("Asset not found")
        Else
            foundResource.Shares -= resource.Shares
            If foundResource.Shares < 0 Then Throw New InvalidOperationException("Not enough resources to perform this action")
        End If
    End Sub

    Public Function HasEnough(item As Resource) As Boolean Implements IAssetManager.HasEnough
        Dim foundResource As Resource = assests.FirstOrDefault(Function(n) n.Name = item.Name)
        If foundResource Is Nothing Then
            Return False
        Else
            Return foundResource.Shares > item.Shares
        End If
        Return False
    End Function

    Function GetAllAssets() As IList(Of Resource) Implements IAssetManager.GetAllAssets
        Return assests
    End Function

    Function GetResource(resource As String) As Resource Implements IAssetManager.GetResource
        Return assests.FirstOrDefault(Function(n) n.Name = resource)
    End Function

End Class

