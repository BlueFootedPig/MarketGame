Imports Core
Imports System.IO

Public Class SaveCompanyToFile
    Inherits IPersistMarketForce

    Private BASE_PATH As String = "./data/"

    Public Sub New()
        
    End Sub

    Public Overrides Sub Save(marketForce As IMarketForce)
        If marketForce Is Nothing Then Throw New ArgumentNullException("marketForce", "marketForce cannot be null.")
        If marketForce.GetType() <> GetType(Company) Then Throw New ArgumentException("marketForce", "marketForce must be a Company.")

        Dim companyToSave As Company = marketForce

        Dim saveString As String = GenerateSaveString(companyToSave)

        Directory.CreateDirectory(BASE_PATH)

        Using fs As FileStream = File.Create(BASE_PATH & companyToSave.Name)
            Using sw As New StreamWriter(fs)
                sw.Write(saveString)
            End Using

        End Using

    End Sub


    Private COMPANY_NAME_INDEX As Integer = 0
    Private COMPANY_LEVEL_INDEX As Integer = 1
    Private COMPANY_SHARE_INDEX As Integer = 2

    Private LABEL_LEVEL_INDEX As Integer = 0

    Private RESOURCE_NAME_INDEX As Integer = 1
    Private RESOURCE_LEVEL_INDEX As Integer = 2
    Private RESOURCE_SHARE_INDEX As Integer = 3

    Public Overrides Function LoadForce(nameOfForce As String, systemSettings As EngineSettings) As IMarketForce
        Dim newCompany As New Company(New AssetManager())

        Using fs As FileStream = File.OpenRead(BASE_PATH & nameOfForce)
            Using sr As New StreamReader(fs)

                Dim companyInfo As String() = sr.ReadLine().ToString().Split(",")
                If companyInfo.Length <> 3 Then Throw New InvalidOperationException("Company info contains the wrong number of elements.")
                newCompany.Name = companyInfo(COMPANY_NAME_INDEX)
                newCompany.Level = companyInfo(COMPANY_LEVEL_INDEX)
                newCompany.Shares = companyInfo(COMPANY_SHARE_INDEX)


                Dim productionInfo As String = sr.ReadLine()
                If productionInfo <> END_REQUIRED Then
                    Dim productionDetails As String() = productionInfo.Split(",")

                    Dim production As New ResourceProduction(systemSettings)
                    production.LaborLevel = productionDetails(LABEL_LEVEL_INDEX)

                    Dim requirementInfo As String = sr.ReadLine()
                    While requirementInfo <> END_REQUIRED
                        Dim requirementDetails As String() = requirementInfo.Split(",")

                        Dim newResource As IResource = Activator.CreateInstance(Type.GetType(requirementDetails(0).Replace("-", ",")))
                        newResource.Name = requirementDetails(RESOURCE_NAME_INDEX)
                        newResource.Level = Integer.Parse(requirementDetails(RESOURCE_LEVEL_INDEX))
                        newResource.Shares = Integer.Parse(requirementDetails(RESOURCE_SHARE_INDEX))
                        requirementInfo = sr.ReadLine()
                    End While

                End If

                While Not sr.EndOfStream
                    Dim assetLine As String = sr.ReadLine()
                    'Example line:
                    'Core.CraftResource,CraftResource,2,1337
                    Dim splitLine As String() = assetLine.Split(",")
                    Dim resourceToAdd As IResource = Activator.CreateInstance(Type.GetType(splitLine(0).Replace("-", ",")))
                    resourceToAdd.Name = splitLine(1)
                    resourceToAdd.Level = splitLine(2)
                    resourceToAdd.Shares = splitLine(3)
                    newCompany.AddResource(resourceToAdd)
                End While

            End Using
        End Using

        Return newCompany

    End Function

    Private END_REQUIRED As String = "--EndRequired--"

    Private Function GenerateSaveString(companyToSave As Company)
        Dim returnValue As String = String.Empty

        returnValue &= String.Join(",", New Object() {companyToSave.Name, _
                                                     companyToSave.Level, _
                                                      companyToSave.Shares})

        returnValue &= Environment.NewLine

        If companyToSave.ProducedResource IsNot Nothing Then

            returnValue &= String.Join(",", New Object() {companyToSave.ProducedResource.LaborLevel, _
                                                          companyToSave.ProducedResource.ProducedResource.ToString()})

            returnValue &= Environment.NewLine

            For Each item As IResource In companyToSave.ProducedResource.RequiredResources
                returnValue &= String.Join(",", New Object() {item.GetType().AssemblyQualifiedName.Replace(",", "-"), item.Name, item.Level, item.Shares})
                returnValue &= Environment.NewLine
            Next
        End If

        returnValue &= END_REQUIRED
        returnValue &= Environment.NewLine

        For Each item As IResource In companyToSave.GetAllAssets()
            returnValue &= String.Join(",", New Object() {item.GetType().AssemblyQualifiedName.Replace(",", "-"), item.Name, item.Level, item.Shares})
            returnValue &= Environment.NewLine
        Next

        Return returnValue
    End Function

End Class
