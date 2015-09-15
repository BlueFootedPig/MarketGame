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

Public Interface IAssetManager
    Sub AddAsset(resource As Resource)
    Sub RemoveAsset(resource As Resource)
    Function HasEnough(item As Resource) As Boolean
    Function GetAllAssets() As IList(Of Resource)
    Function GetResource(resource As String) As Resource
End Interface

Public Class VoidAssetManager
    Implements IAssetManager


    Public Sub AddAsset(resource As Resource) Implements IAssetManager.AddAsset

    End Sub

    Public Function GetAllAssets() As IList(Of Resource) Implements IAssetManager.GetAllAssets
        Dim money As New Resource() With {.Name = Resource.CREDIT, .Shares = Integer.MaxValue}
        Dim returnValue As New List(Of Resource)
        returnValue.Add(money)
        Return returnValue

    End Function

    Public Function GetResource(wantedResource As String) As Resource Implements IAssetManager.GetResource
        If wantedResource = Resource.CREDIT Then Return New Resource() With {.Name = Resource.CREDIT, .Shares = Integer.MaxValue}
    End Function

    Public Function HasEnough(item As Resource) As Boolean Implements IAssetManager.HasEnough
        Return True
    End Function

    Public Sub RemoveAsset(resource As Resource) Implements IAssetManager.RemoveAsset

    End Sub
End Class
