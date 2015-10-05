Imports Core
Imports System.IO

Public Class SaveCompanyToFile
    Implements ISaveMarketForce

    Private path As String
    Private BASE_PATH As String = "./data/"

    Public Sub New(filePath As String)
        If String.IsNullOrEmpty(filePath) Then Throw New ArgumentException("filePath", "filePath cannot be null or empty.")
        path = BASE_PATH & filePath
    End Sub

    Public Sub Save(marketForce As IMarketForce) Implements ISaveMarketForce.Save
        If marketForce Is Nothing Then Throw New ArgumentNullException("marketForce", "marketForce cannot be null.")
        If marketForce.GetType() <> GetType(Company) Then Throw New ArgumentException("marketForce", "marketForce must be a Company.")

        Dim companyToSave As Company = marketForce

        Dim saveString As String = GenerateSaveString(companyToSave)

        Directory.CreateDirectory(BASE_PATH)

        Using fs As FileStream = File.Create(path)
            Using sw As New StreamWriter(fs)
                sw.Write(saveString)
                sw.Flush()
            End Using

        End Using

    End Sub

    Private Function GenerateSaveString(companyToSave As Company)
        Dim returnValue As String = String.Empty

        returnValue &= String.Join(",", New Object() {companyToSave.Name, _
                                                     companyToSave.Level, _
                                                      companyToSave.Shares})

        returnValue &= Environment.NewLine

        returnValue &= String.Join(",", New Object() {companyToSave.ProducedResource.LaborLevel, _
                                                      companyToSave.ProducedResource.ProducedResource.ToString()})

        returnValue &= Environment.NewLine

        For Each item As IResource In companyToSave.ProducedResource.RequiredResources
            returnValue &= String.Join(",", New Object() {item.Name, item.Level, item.Shares})
            returnValue &= Environment.NewLine
        Next

        returnValue &= "--EndRequired--"
        returnValue &= Environment.NewLine

        For Each item As IResource In companyToSave.GetAllAssets()
            returnValue &= String.Join(",", New Object() {item.GetType(), item.Name, item.Level, item.Shares})
            returnValue &= Environment.NewLine
        Next

        Return returnValue
    End Function

End Class
