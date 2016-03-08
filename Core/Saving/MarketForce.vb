Public Class MarketForceState

    Private propertyDictionary As New Dictionary(Of String, Object)


    Public ReadOnly Strategies As List(Of StateObject)

    Public Sub New(marketForceAssets As IAssetManager)
        Assets = marketForceAssets
    End Sub

    Public Sub AddProperty(propertyName As String, value As Object)
        If propertyDictionary.ContainsKey(propertyName) Then
            propertyDictionary(propertyName) = value
        Else
            propertyDictionary.Add(propertyName, value)
        End If
    End Sub

    Public Function GetProperty(propertyName As String)
        If Not propertyDictionary.ContainsKey(propertyName) Then Throw New ArgumentException("propertyName was not found in this object.")

        Return propertyDictionary(propertyName)
    End Function

    Public ReadOnly ProducedResource As StateObject

    Public ReadOnly Assets As IAssetManager


End Class
